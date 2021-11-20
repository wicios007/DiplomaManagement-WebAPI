using System;
using System.ComponentModel.DataAnnotations;
using WebAPI.Entities;

namespace WebAPI.Models
{
    public class RegisterUserDto
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [StringLength(100,ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name="Password")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and password confirmation doesn't match")]
        public string ConfirmPassword { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        public int RoleId { get; set; }
        [Display(Name = "Promoter titles")]
        public string PromoterTitle { get; set; }
        [Display(Name = "Student index number")]
        public string StudentIndexNumber { get; set; }

    }
}