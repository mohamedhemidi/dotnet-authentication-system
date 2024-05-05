using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Application.Common.Interfaces.Authentication;

namespace backend_core.Application.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AccountService(IJwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public AccountResult Register(string Username, string Email, string Password)
        {
            // Check if User Exists:
            // Create user (Generate unique ID):
            // Create JWT Token:

            Guid userId = Guid.NewGuid();

            var token = _jwtTokenGenerator.GenerateToken(userId, Username, Email);


            return new AccountResult(userId, Username, Email, token);
        }
        public AccountResult Login(string Email, string Password)
        {
            return new AccountResult(Guid.NewGuid(), "Username", Email, "token");
        }


    }
}