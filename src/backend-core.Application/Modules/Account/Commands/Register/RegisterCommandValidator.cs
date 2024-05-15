using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace backend_core.Application.Modules.Account.Commands.Register
{
    public class RegisterCommandValidator: AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.registerDTO.Email).NotEmpty();
            RuleFor(x => x.registerDTO.Username).NotEmpty();
            RuleFor(x => x.registerDTO.Password).NotEmpty();
        }
    }
}