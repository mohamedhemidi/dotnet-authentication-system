using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Application.Identity.DTOs;
using FluentValidation;

namespace backend_core.Application.Identity.Client.Commands.ResetPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.resetPasswordDTO.Password)
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
        }

         private bool ContainNonAlphanumeric(string password)
        {
            return password.Any(ch => !char.IsLetterOrDigit(ch));
        }
    }

}