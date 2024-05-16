using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Application.DTOs;
using FluentValidation;

namespace backend_core.Application.Modules.Account.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterDTO>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("{PropertyName} is required").NotNull().EmailAddress();
            RuleFor(x => x.Username).NotNull().NotEmpty().MaximumLength(64);
            RuleFor(x => x.Password).MinimumLength(6);
        }
    }
}