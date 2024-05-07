using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;

namespace backend_core.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class User
        {
            public static Error DuplicateEmail => Error.Validation(
                code: "User.DuplicateEmail", 
                description: "Email is already in use."
                );
        }
    }
}