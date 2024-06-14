using System.Security.Cryptography;
using backend_core.Application.Identity.Common.Interfaces;
using backend_core.Domain.Models;
using Microsoft.Extensions.Options;

namespace backend_core.Application.Identity.Common.Services
{
    public class RefreshToken : IRefreshToken
    {
        private readonly JwtSettings _jwtSettings;
        public RefreshToken(IOptions<JwtSettings> jwtOptions)
        {
            _jwtSettings = jwtOptions.Value;
        }
        public TokenType GenerateRefreshToken()
        {
            var randomeNumber = new Byte[64];
            var range = RandomNumberGenerator.Create();
            range.GetBytes(randomeNumber);
            return new TokenType
            {
                Token = Convert.ToBase64String(randomeNumber),
                ExpiryDate = DateTime.UtcNow.AddDays(_jwtSettings.RefershTokenExpiryInDays)
            };
        }

    }
}