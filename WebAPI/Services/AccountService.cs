﻿using System;
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
    public class AccountService// : IAccountService
    {
/*        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IOptions<JwtOptionsDto> _jwtOptions;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AccountService(ILogger logger, IMapper mapper, IOptions<JwtOptionsDto> jwtOptions, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _logger = logger;
            _mapper = mapper;
            _jwtOptions = jwtOptions;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public void LoginUser(LoginUserDto dto)
        {
            var user = _userManager.FindByEmailAsync(dto.Email);
            //var userRoles =  _userManager.GetRolesAsync(user)
        }

        public void RegisterUser(RegisterUserDto dto)
        {
            //var user = 
        }*/
        
        
        
        
        /*private readonly DiplomaManagementDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public AccountService(DiplomaManagementDbContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }
        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                RoleId = dto.RoleId,
                RegistrationDate = dto.RegistrationDate

            };
            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = hashedPassword;
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        public string GenerateJwt(LoginUserDto dto)
        {
            var user = _context.Users
                .Include(u=>u.Role)
                .FirstOrDefault(u => u.Email == dto.Email);
            if (user == null)
            {
                throw new BadRequestException("Invalid email or password");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid email or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expireDate = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, 
                _authenticationSettings.JwtIssuer,
                claims, 
                expires: expireDate, 
                signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);

        }*/
    }
}