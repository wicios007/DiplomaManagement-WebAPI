using System;
using System.ComponentModel.DataAnnotations;
using WebAPI.Entities;

namespace WebAPI.Models
{
    public class RegisterUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public string IndexNumber { get; set; }
        public int RoleId { get; set; }

    }
}