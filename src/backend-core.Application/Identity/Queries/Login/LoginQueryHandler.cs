using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Application.Identity.Interfaces;
using backend_core.Domain.Entities;
using MediatR;
using backend_core.Domain.Repositories;
using backend_core.Application.Common.Exceptions;
using backend_core.Application.Modules.Client.Account.Queries.Login;
using backend_core.Application.Identity.Queries.Login;
using backend_core.Application.Identity.DTOs.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend_core.Application.Modules.Account.Queries.Login
{

    public class LoginQueryHandler : IRequestHandler<LoginQuery, AccountResultDTO>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signinManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public LoginQueryHandler(UserManager<User> userManager, SignInManager<User> signinManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
            _signinManager = signinManager;
        }

        public async Task<AccountResultDTO> Handle(LoginQuery query, CancellationToken cancellationToken)
        {

            // 1. Validate if User does Exist
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == query.loginDTO.Email);

            if (user == null)
            {
                throw new NotFoundException(nameof(user), query.loginDTO.Email);
            }
            //// 2. Validate if Password Is Correct
            var result = await _signinManager.CheckPasswordSignInAsync(user, query.loginDTO.Password, false);
            if (!result.Succeeded)
            {
                throw new BadRequestException("Credentials Does Not Match");
            }
            //// 3. Create JWT Token

            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AccountResultDTO(
                user.Id,
                user.UserName,
                user.Email,
                token
            );
        }
    }
}