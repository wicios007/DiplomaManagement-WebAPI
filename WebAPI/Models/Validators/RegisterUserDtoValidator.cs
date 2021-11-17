using System.Linq;
using FluentValidation;
using WebAPI.Entities;

namespace WebAPI.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(DiplomaManagementDbContext dbContext)
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(u => u.Password)
                .NotEmpty()
                .MinimumLength(8);

            RuleFor(u => u.ConfirmPassword)
                .Equal(u => u.Password);
            
/*
            RuleFor(u => u.RoleId)
                .IsInEnum();*/

            RuleFor(u => u.Email)
                .Custom((value, context) =>
                {
                    bool emailUsed = dbContext.Users.Any(u => u.Email == value);
                    if (emailUsed)
                    {
                        context.AddFailure("Email", "This email is used");
                    }
                });
        }
    }
}