using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Application.Account.Common;
using backend_core.Application.Common.Interfaces.Authentication;
using backend_core.Application.Common.Interfaces.Persistence;
using backend_core.Domain.Common.Errors;
using backend_core.Domain.Entities;
using ErrorOr;
using MediatR;

namespace backend_core.Application.Account.Queries.Login
{

    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AccountResult>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AccountResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            // 1. Validate if User does Exist
            if (_userRepository.GetUserByEmail(query.Email) is not User user)
            {
                return Errors.Authentication.InvalidCredentials;
            }
            // 2. Validate if Password Is Correct
            if (user.Password != query.Password)
            {
                return Errors.Authentication.InvalidCredentials;
            }
            // 3. Create JWT Token

            var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Username, user.Email);
            return new AccountResult(user, token);
        }
    }
}