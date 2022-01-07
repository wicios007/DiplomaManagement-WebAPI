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
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NLog;
using WebAPI.Entities;
using WebAPI.Exceptions;
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
        private readonly IEmailSenderService _emailSenderService;
        private readonly IAccountService _accountService;

        public AccountController(ILogger<AccountController> logger, IEmailSenderService emailSenderService, IAccountService accountService)
        {
            _logger = logger;
            _emailSenderService = emailSenderService;
            _accountService = accountService;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUserDto dto)
        {
            if(dto is null || ModelState == null || !ModelState.IsValid)
            {
                _logger.LogInformation($"Model is invalid");
                return BadRequest("Invalid data");
            }
            var newUserId = await _accountService.RegisterUser(dto);
            return Created($"api/account/users/{newUserId}", null);
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginUserDto dto)
        {
            if (dto is null || ModelState is null || !ModelState.IsValid)
            {
                _logger.LogInformation($"Model is invalid");
                return BadRequest("Invalid credentials or model");
            }
            var response = await _accountService.Login(dto);
            return Ok(response);
        }
        [HttpPost("send")]
        public async Task<ActionResult> SendEmail([FromBody] SendEmailDto dto)
        {
            if(ModelState == null || !ModelState.IsValid)
            {
                _logger.LogInformation($"Model or DTO is invalid");
                return BadRequest("Model error");
            }
            await _emailSenderService.SendEmailAsync(dto.DestId, dto.Subject, dto.Content);
            
            return Ok();
        }

        [HttpGet("users")]
        async public Task<ActionResult<List<UserDto>>> GetUsers(){
            var result = await _accountService.GetUsers();
            return Ok(result);
        }
        [HttpGet("users/department/{departmentId}")]
        async public Task<ActionResult<List<UserDto>>> GetUsersFromDepartment(int departmentId, int roleValue)
        {
            var result = await _accountService.GetUsersFromDepartment(departmentId, roleValue);
            return Ok(result);
        }

        [HttpGet("usersByRole")]
        async public Task<ActionResult<List<UserDto>>> GetUsersByRole(int roleValue)
        {
            var result = await _accountService.GetUsersByRole(roleValue);
            return Ok(result);
        }

        [HttpGet("users/current")]
        async public Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var result = await _accountService.GetCurrentUser();
            return Ok(result);
        }

        [HttpGet("users/{id}")]
        async public Task<ActionResult<UserDto>> GetUserById(int id)
        {
            var result = await _accountService.GetUserById(id);
            return Ok(result);
        }

        [HttpGet("roles")]
        async public Task<ActionResult<List<Role>>> GetRoles()
        {
            var result = await _accountService.GetRoles();
            return Ok(result);
        }
    }
}