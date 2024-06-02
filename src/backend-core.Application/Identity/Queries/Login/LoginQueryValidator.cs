using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Application.Identity.DTOs.Account;
using backend_core.Application.Identity.Queries.Login;
using FluentValidation;

namespace backend_core.Application.Modules.Client.Account.Queries.Login
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
        }

    }
}