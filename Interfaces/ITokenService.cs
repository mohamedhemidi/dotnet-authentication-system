using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Models;

namespace backend_core.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}