using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Application.Identity.DTOs;
using FluentValidation;

namespace backend_core.Application.Modules.Client.Account.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            // Include(new AccountValidator());
            RuleFor(x => x.registerDTO.Email)
           .NotEmpty()
           .WithMessage("{PropertyName} is required")
           .NotNull()
           .EmailAddress()
           .OverridePropertyName("Email");

            RuleFor(x => x.registerDTO.Password)
            .NotEmpty()
            .WithMessage("Your password cannot be empty")
            .MinimumLength(8)
            .WithMessage("Your password length must be at least 8.")
            .MaximumLength(16)
            .WithMessage("Your password length must not exceed 16.")
            .Matches(@"[A-Z]+")
            .WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+")
            .WithMessage("Your password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+")
            .WithMessage("Your password must contain at least one number.")
            .Must(ContainNonAlphanumeric)
            .WithMessage("Your password must contain at least one non-alphanumeric character eg: (@ $ / !)")
            .OverridePropertyName("Password");

            RuleFor(x => x.registerDTO.UserName)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .NotNull()
            .Matches("^[a-zA-Z]+$")
            .WithMessage("Username must contain only letters without any whitespace, digits, or non-alphanumeric characters.")
            .OverridePropertyName("UserName");
        }
        private bool ContainNonAlphanumeric(string password)
        {
            return password.Any(ch => !char.IsLetterOrDigit(ch));
        }
    }

}