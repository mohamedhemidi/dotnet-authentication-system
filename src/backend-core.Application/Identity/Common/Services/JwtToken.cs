using System.Security.Claims;
using System.Text;
using backend_core.Application.Identity.Common.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using backend_core.Domain.Entities;
using backend_core.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using backend_core.Domain.Models;
using backend_core.Application.Identity.DTOs.Account;
using Microsoft.AspNetCore.Identity;

namespace backend_core.Application.Identity.Common.Services
{
    public class JwtToken : IJwtToken
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<AppUser> _userManager;

        public JwtToken(IOptions<JwtSettings> jwtOptions, UserManager<AppUser> userManager)
        {
            _jwtSettings = jwtOptions.Value;
            _userManager = userManager;
        }
        public async Task<TokenType> GenerateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName!)
            };
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                claims: claims,
                signingCredentials: creds
            );

            return new TokenType
            {
                Token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor),
                ExpiryDate = tokenDescriptor.ValidTo
            };
        }

        public async Task<TokenType> RenewAccessToken(AuthResultDTO result)
        {
            var accessToken = result.AccessToken!.Token;
            var refreshToken = result.RefreshToken!.Token;

            var principal = GetClaimsPrincipal(accessToken);
            var user = await _userManager.FindByNameAsync(principal!.Identity!.Name!);
            if (refreshToken != user!.RefreshToken && result.RefreshToken.ExpiryDate <= DateTime.Now)
            {
                return new TokenType { };
            }

            var response = await GenerateToken(user);

            return response;
        }

        private ClaimsPrincipal GetClaimsPrincipal(string accessToken)
        {
            var TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false,
                NameClaimType = "name",
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtSettings.Secret)
                )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(accessToken, TokenValidationParameters, out SecurityToken securityToken);

            return principal;
        }
    }
}