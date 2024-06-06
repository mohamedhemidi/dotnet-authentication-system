using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using backend_core.Domain.Entities;

namespace backend_core.Application.Identity.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(AppUser user, IList<string> roles);
    }
}