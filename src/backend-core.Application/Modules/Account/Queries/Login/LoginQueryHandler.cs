using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Application.Common.Interfaces.Authentication;
using backend_core.Domain.Common.Errors;
using backend_core.Domain.Entities;
using MediatR;
using backend_core.Application.Contracts.Persistance;
using backend_core.Application.DTOs;

namespace backend_core.Application.Modules.Account.Queries.Login
{

    public class LoginQueryHandler : IRequestHandler<LoginQuery, AccountResultDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<AccountResultDTO> Handle(LoginQuery query, CancellationToken cancellationToken)
        {

            // 1. Validate if User does Exist
            var user = await _userRepository.Get(x => x.Email == query.loginDTO.Email);
            if (user == null)
            {
                throw new Exception("User Not Found");
            }
            //// 2. Validate if Password Is Correct
            if (user.Password != query.loginDTO.Password)
            {
                throw new Exception("Credentials Does Not Match");
            }
            //// 3. Create JWT Token

            var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Username, user.Email);
            return new AccountResultDTO(user.Id, user.Username, user.Email, token);
        }
    }
}