using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Application.Common.Interfaces.Authentication;
using backend_core.Application.Common.Interfaces.Persistence;
using backend_core.Domain.Entities;

namespace backend_core.Application.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AccountService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }
        public AccountResult Register(string username, string email, string password)
        {
            // Check if User Exists:
            if (_userRepository.GetUserByEmail(email) is not null)
            {
                throw new Exception("User with give email already exists");
            }
            // Create user (Generate unique ID):
            var newUser = new User
            {
                Username = username,
                Email = email,
                Password = password
            };

            _userRepository.Add(newUser);
            // Create JWT Token:

            var token = _jwtTokenGenerator.GenerateToken(newUser.Id, newUser.Username, newUser.Email);


            return new AccountResult(newUser.Id, newUser.Username, newUser.Email, token);
        }
        public AccountResult Login(string email, string password)
        {
            // 1. Validate if User does Exist
            if (_userRepository.GetUserByEmail(email) is not User user)
            {
                throw new Exception("Provided Email does not exist in our records");
            }
            // 2. Validate if Password Is Correct
            if (user.Password != password)
            {
                throw new Exception("Invalide Credentials");
            }
            // 3. Create JWT Token

            var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Username, user.Email);
            return new AccountResult(user.Id, user.Username, user.Email, token);
        }


    }
}