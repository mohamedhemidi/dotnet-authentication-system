using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_core.Contracts.Account
{
    public record LoginRequest(
        string Email,
        string Password
    );
}