using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using backend_core.Application.Identity.DTOs.Account;
using backend_core.Domain.Entities;
using backend_core.Domain.Models;

namespace backend_core.Application.Identity.Common.Interfaces
{
    public interface IJwtToken
    {
        Task<TokenType> GenerateToken(AppUser user);
        Task<TokenType> RenewAccessToken(AuthResultDTO result);
    }
}