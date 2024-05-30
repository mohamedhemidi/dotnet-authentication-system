using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace backend_core.Application.Common.Exceptions
{
    public class FluentValidationException : ApplicationException
    {
        public FluentValidationException(ValidationResult result)
        {
            throw new ValidationException(result.Errors);
        }
    }
}