using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_core.Application.Identity.DTOs
{
    public class LoginTwoFactorsDTO
    {
        public required string Code { get; set; }
        public required string EmailOrUsername { get; set; }
        // public required string Email { get; set; }
    }
}