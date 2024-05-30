using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Internal;

namespace backend_core.Application.Identity.Validators
{
    public class AccountValidator : AbstractValidator<IAccountDTO>
    {
        public AccountValidator()
        {
            RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .NotNull()
            .EmailAddress();

            RuleFor(x => x.Password)
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
            .WithMessage("Your password must contain at least one non-alphanumeric character eg: (@ $ / !)");
        }
        private bool ContainNonAlphanumeric(string password)
        {
            return password.Any(ch => !char.IsLetterOrDigit(ch));
        }
    }
}