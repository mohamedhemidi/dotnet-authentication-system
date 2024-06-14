using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Application.Common.Exceptions;
using backend_core.Application.Identity.Common.Interfaces;
using backend_core.Application.Identity.DTOs.Account;
using backend_core.Domain.Common;
using backend_core.Domain.Entities;
using backend_core.Domain.Models;
using backend_core.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend_core.Application.Identity.Client.Queries.RenewToken
{

    public class RenewTokenQueryHandler : IRequestHandler<RenewTokenQuery, ApiResponse<TokenType>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IJwtToken _jwtToken;
        public RenewTokenQueryHandler(UserManager<AppUser> userManager, IUserRepository userRepository, IJwtToken jwtToken)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _jwtToken = jwtToken;
        }

        public async Task<ApiResponse<TokenType>> Handle(RenewTokenQuery request, CancellationToken cancellationToken)
        {
            var response = await _jwtToken.RenewAccessToken(request.tokens);

            return new ApiResponse<TokenType>
            {
                IsSuccess = true,
                Message = "Your access token has been generated successfully",
                StatusCode = 200,
                Response = response
            };
        }
    }
}