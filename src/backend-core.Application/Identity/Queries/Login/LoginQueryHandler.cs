using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Application.Identity.Interfaces;
using backend_core.Domain.Entities;
using MediatR;
using backend_core.Application.Contracts.Persistance;
using backend_core.Application.Common.Exceptions;
using backend_core.Application.Modules.Client.Account.Queries.Login;
using backend_core.Application.Identity.Queries.Login;
using backend_core.Application.Identity.DTOs.Account;

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
            // Data Validation :

            var validator = new LoginQueryValidator();
            var validationResult = await validator.ValidateAsync(query.loginDTO);
            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            // 1. Validate if User does Exist
            var user = await _userRepository.Get(x => x.Email == query.loginDTO.Email);
            if (user == null)
            {
                throw new NotFoundException(nameof(user), query.loginDTO.Email);
            }
            //// 2. Validate if Password Is Correct
            if (user.Password != query.loginDTO.Password)
            {
                throw new BadRequestException("Credentials Does Not Match");
            }
            //// 3. Create JWT Token

            var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Username, user.Email);
            return new AccountResultDTO(user.Id, user.Username, user.Email, token);
        }
    }
}