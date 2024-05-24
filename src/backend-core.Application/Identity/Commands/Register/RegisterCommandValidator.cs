using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Application.Identity.DTOs;
using backend_core.Application.Identity.Validators;
using FluentValidation;

namespace backend_core.Application.Modules.Client.Account.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterDTO>
    {
        public RegisterCommandValidator()
        {
            Include(new IAccountValidator());
            RuleFor(x => x.Username).NotEmpty().WithMessage("{PropertyName} is required").NotNull();
        }
    }
}