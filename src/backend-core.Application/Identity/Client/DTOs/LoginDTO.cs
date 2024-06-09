using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_core.Application.Identity.DTOs.Account;

public class LoginDTO 
{
    // public required string Email { get; set; }
    public required string EmailOrUsername { get; set; }
    public required string Password { get; set; }
}
