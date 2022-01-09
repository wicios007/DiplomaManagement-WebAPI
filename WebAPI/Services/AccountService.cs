using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Entities;
using WebAPI.Exceptions;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class LoginReturn
    {
        public string Access_token { get; set; }
        public string Expires_in { get; set; }
        public string User_role { get; set; }
        public int Id { get; set; }
    }
    public class AccountService : IAccountService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly JwtOptionsDto _jwtOptions;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IUserContextService _userContextService;
        private readonly DiplomaManagementDbContext _dbContext;

        public AccountService(ILogger<AccountService> logger, IMapper mapper, IOptions<JwtOptionsDto> jwtOptions, SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<Role> roleManager, IEmailSenderService emailSenderService, IUserContextService userContextService, DiplomaManagementDbContext dbContext)
        {
            _logger = logger;
            _mapper = mapper;
            _jwtOptions = jwtOptions?.Value;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _emailSenderService = emailSenderService;
            _userContextService = userContextService;
            _dbContext = dbContext;
        }

        public async Task<int> RegisterUser(RegisterUserDto dto)
        {

            if (dto == null)
            {
                _logger.LogInformation($"Model is invalid");
                throw new BadRequestException("Invalid data");
            }


            switch (dto.RoleId)
            {
                case (int)RoleValue.User:
                    {
                        var user = new User()
                        {
                            UserName = dto.Email,
                            Email = dto.Email,
                            FirstName = dto.FirstName,
                            LastName = dto.LastName,
                            RegistrationDate = DateTime.Now
                        };
                        var result = await _userManager.CreateAsync(user, dto.Password);

                        if (result.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(user, "User");
                            _logger.LogInformation("New user created");
                            return user.Id;
                        }
                        _logger.LogInformation("Error creating new user");
                        throw new BadRequestException("User not created");
                    }
                case (int)RoleValue.Promoter:
                    {
                        var promoter = new Promoter()
                        {
                            UserName = dto.Email,
                            Email = dto.Email,
                            FirstName = dto.FirstName,
                            LastName = dto.LastName,
                            RegistrationDate = DateTime.Now,
                            Title = dto.PromoterTitle,
                            DepartmentId = dto.DepartmentId
                        };
                        var result = await _userManager.CreateAsync(promoter, dto.Password);

                        if (result.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(promoter, "Promoter");
                            _logger.LogInformation("New promoter created");
                            return promoter.Id;
                        }
                        else
                        {
                            _logger.LogInformation("Error creating new promoter");
                            throw new BadRequestException("Promotor not created");
                        }

                    }
                case (int)RoleValue.Student:
                    {
                        var student = new Student()
                        {
                            UserName = dto.Email,
                            Email = dto.Email,
                            FirstName = dto.FirstName,
                            LastName = dto.LastName,
                            RegistrationDate = DateTime.Now,
                            IndexNumber = dto.StudentIndexNumber,
                            DepartmentId = dto.DepartmentId
                        };
                        var result = await _userManager.CreateAsync(student, dto.Password);
                        if (result.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(student, "Student");
                            _logger.LogInformation("New student created");
                            return student.Id;
                        }
                        else
                        {
                            _logger.LogInformation("Error creating new student");
                            throw new BadRequestException("Student not created");
                        }
                    }
                case (int)RoleValue.Admin:
                    {
                        var admin = new Admin()
                        {
                            UserName = dto.Email,
                            Email = dto.Email,
                            FirstName = dto.FirstName,
                            LastName = dto.LastName,
                            RegistrationDate = DateTime.Now
                        };
                        var result = await _userManager.CreateAsync(admin, dto.Password);
                        if (result.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(admin, "Admin");
                            _logger.LogInformation("New admin created");
                            return admin.Id;
                        }
                        else
                        {
                            _logger.LogInformation("Error creating new admin");
                            throw new BadRequestException("Admin not created");
                        }
                    }
            }
            return 0;

        }

        public async Task<LoginReturn> Login(LoginUserDto dto)
        {


            if (dto is null)
            {
                _logger.LogInformation($"Model is invalid");
                throw new BadRequestException("Data object is invalid");
            }

            var result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, true, false);
            if (!result.Succeeded)
            {
                _logger.LogInformation($"Invalid email {dto.Email} or password {dto.Password}");
                throw new BadRequestException("Niepoprawny login lub hasło");
            }

            var user = await _userManager.FindByEmailAsync(dto.Email);
            var userRoles = await _userManager.GetRolesAsync(user);

            var userId = user.Id;
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, dto.Email),
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Nbf,
                        new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                    new Claim(JwtRegisteredClaimNames.Exp,
                        ((long) ((DateTime.Now.AddMinutes(_jwtOptions.TokenExpirationMinutes) -
                                  new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds)).ToString()),
                };
            claims.AddRange(userRoles.Select(ur => new Claim(ClaimTypes.Role, ur)));

            var role = userRoles.Select(ur => new Claim(ClaimTypes.Role, ur)).Select(u => u.Value);

            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
                    SecurityAlgorithms.HmacSha256)), new JwtPayload(claims));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);
            var response = new LoginReturn()
            {
                Access_token = encodedJwt,
                Expires_in = token.ValidTo.ToString("yyyy-MM-ddTHH:mm:ss"),
                User_role = role.First(),
                Id = userId
            };
            return response;

        }

        async public Task<List<UserDto>> GetUsers()
        {
            var users = await _userManager
                .Users
                .Include(c => c.Student)
                .Include(c => c.Promoter) //TODO: 
                .ToListAsync();

            var result = _mapper.Map<List<UserDto>>(users);
            return result;
        }
        async public Task<List<UserDto>> GetUsersFromDepartment(int departmentId, int roleValue)
        {
            var users = await _dbContext.Users.FromSqlRaw("SELECT * FROM AspNetUsers WHERE UserType=@roleValue AND DepartmentId=@departmentId", new SqlParameter("@roleValue", roleValue), new SqlParameter("@departmentId", departmentId)).ToListAsync();

            var result = _mapper.Map<List<UserDto>>(users);
            return result;
        }
        async public Task<List<UserDto>> GetUsersByRole(int roleValue)
        {
            if (new[] { 1, 2, 3 }.Contains(roleValue))
            {
                var usersWithRoles = await _dbContext.Users.FromSqlRaw("SELECT * FROM AspNetUsers WHERE UserType=@roleValue", new SqlParameter("@roleValue", roleValue)).ToListAsync();
                var result = _mapper.Map<List<UserDto>>(usersWithRoles);
                return result;
            }
            else
            {
                var allUsers = await _dbContext.Users.ToListAsync();
                var result = _mapper.Map<List<UserDto>>(allUsers);
                return result;
            }
        }
        async public Task<UserDto> GetCurrentUser()
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(c => c.Id == _userContextService.GetUserId);
            if (user is null)
            {
                throw new NotFoundException("User not found");
            }
            var role = await _userManager.GetRolesAsync(user);
            var result = _mapper.Map<UserDto>(user);
            result.RoleName = role[0];
            return result;
        }
        public async Task<UserDto> GetUserById(int id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(c => c.Id == id);
            var role = await _userManager.GetRolesAsync(user);
            var result = _mapper.Map<UserDto>(user);
            result.RoleName = role[0];
            return result;
        }
        public async Task<List<Role>> GetRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles;
        }

    }
}