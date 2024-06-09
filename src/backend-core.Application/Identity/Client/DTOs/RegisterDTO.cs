using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_core.Application.Identity.DTOs;

public class RegisterDTO 
{
    public required string Email { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
}
