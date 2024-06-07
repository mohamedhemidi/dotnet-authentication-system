using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_core.Application.Identity.DTOs
{
    public class ResetPasswordDTO
    {
        public required string Password { get; set; }
    }
}