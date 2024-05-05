using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_core.Application.Services.Account
{
    public record AccountResult(
        Guid Id,
        string Username,
        string Email,
        string Token
    );
}