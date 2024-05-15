using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace backend_core.Application.Modules.Account.Queries.Login
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(x => x.loginDTO.Email).NotEmpty();
            RuleFor(x => x.loginDTO.Password).NotEmpty();
        }
    }
}