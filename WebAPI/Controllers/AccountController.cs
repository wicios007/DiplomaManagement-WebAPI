using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NLog;
using WebAPI.Entities;
using WebAPI.Interfaces;
using WebAPI.Middleware;
using WebAPI.Models;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace WebAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly JwtOptionsDto _jwtOptions;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public AccountController(ILogger<AccountController> logger, IMapper mapper, IOptions<JwtOptionsDto> jwtOptions, SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _logger = logger;
            _mapper = mapper;
            _jwtOptions = jwtOptions?.Value;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUserDto dto)
        {
            try
            {
                if (dto == null || ModelState == null || !ModelState.IsValid)
                {
                    _logger.LogInformation($"Model is invalid");
                    return BadRequest("Invalid data");
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
                                return Created("User created", null);
                            }

                            return BadRequest();
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
                                return Created("Promoter created", null);
                            }
                            else
                            {
                                _logger.LogInformation("Error creating new promoter");
                                return BadRequest("Promoter not created");
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
                                return Created("Student created", null);
                            }
                            else
                            {
                                _logger.LogInformation("Error creating new student");
                                return BadRequest("Student not created");
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
                                return Created("Admin created", null);
                            }
                            else
                            {
                                _logger.LogInformation("Error creating new admin");
                                return BadRequest("Admin not created");
                            }
                        }

                }
                return BadRequest();

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest($"Error occurred");
            }

        }
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginUserDto dto)
        {
            try
            {
                if (dto is null || ModelState is null || !ModelState.IsValid)
                {
                    _logger.LogInformation($"Model is invalid");
                    return BadRequest("Invalid credentials or model");
                }

                var result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, true, false);
                if (!result.Succeeded)
                {
                    _logger.LogInformation($"Invalid email {dto.Email} or password {dto.Password}");
                    return BadRequest("Niepoprawne dane logowania");
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
                var response = new
                {
                    access_token = encodedJwt,
                    expires_in = token.ValidTo.ToString("yyyy-MM-ddTHH:mm:ss"),
                    user_role = role.First()
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest("Error occurred");
            }
        }
        [HttpGet("users")]
        public List<UserDto> GetUsers(){
            var users = _userManager
                .Users
                .ToList();
            
            var result = _mapper.Map<List<UserDto>>(users);            
            return result;
        }

        [HttpGet("roles")]
        public List<Role> GetRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return roles;
        }
    }
}