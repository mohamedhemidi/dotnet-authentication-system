using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Application.Identity.DTOs;
using FluentValidation;

namespace backend_core.Application.Identity.Client.Commands.UpdateUserProfile
{
    public class UpdateUserProfileCommandValidator : AbstractValidator<UpdateUserProfileCommand>
    {
        public UpdateUserProfileCommandValidator()
        {
            RuleFor(x => x.updateUserProfileDTO.UserName)
                .Matches("^[a-zA-Z]+$")
            .WithMessage("Username must contain only letters without any whitespace, digits, or non-alphanumeric characters.")
            .OverridePropertyName("UserName");

            RuleFor(x => x.updateUserProfileDTO.FirstName)
            .Length(3, 50).WithMessage("First name must be between 3 and 50 characters.")
            .Matches(@"^[a-zA-Z]+$").WithMessage("First name must contain only letters.")
            .Unless(x => string.IsNullOrEmpty(x.updateUserProfileDTO.FirstName));

            RuleFor(x => x.updateUserProfileDTO.LastName)
            .Length(3, 50).WithMessage("Last name must be between 3 and 50 characters.")
            .Matches(@"^[a-zA-Z]+$").WithMessage("Last name must contain only letters.")
            .Unless(x => string.IsNullOrEmpty(x.updateUserProfileDTO.LastName)); ;

            RuleFor(x => x.updateUserProfileDTO.PhoneNumber)
            .Matches(@"^\+?\d+$").WithMessage("Phone number must contain only digits and an optional leading plus sign.")
            .Length(10, 15).WithMessage("Phone number must be between 10 and 15 characters.")
            .Unless(x => string.IsNullOrEmpty(x.updateUserProfileDTO.PhoneNumber)); ;

        }
    }
}