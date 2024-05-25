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
            RuleFor(x => x.Email).NotEmpty().WithMessage("{PropertyName} is required").NotNull().EmailAddress();
            RuleFor(x => x.Password).MinimumLength(6);
        }
    }
}