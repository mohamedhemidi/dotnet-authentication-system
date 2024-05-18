using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Internal;

namespace backend_core.Application.DTOs.Account.Validators
{
    public class IAccountValidator : AbstractValidator<IAccountDTO>
    {
        public IAccountValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("{PropertyName} is required").NotNull().EmailAddress();
            RuleFor(x => x.Password).MinimumLength(6);
        }
    }
}