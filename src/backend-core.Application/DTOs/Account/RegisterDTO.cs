using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Application.DTOs.Account.Validators;

namespace backend_core.Application.DTOs;

public class RegisterDTO : IAccountDTO
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}
