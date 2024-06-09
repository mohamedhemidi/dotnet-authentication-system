using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Application.Identity.DTOs.Account;
using FluentValidation;

namespace backend_core.Application.Identity.Queries.Client.LoginOtp
{
    public class LoginOtpQueryValidator : AbstractValidator<LoginOtpQuery>
    {
        public LoginOtpQueryValidator()
        {
        }

    }
}