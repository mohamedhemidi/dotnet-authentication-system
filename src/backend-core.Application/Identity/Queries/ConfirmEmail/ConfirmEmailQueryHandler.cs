using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Application.Common.Exceptions;
using backend_core.Domain.Common;
using backend_core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend_core.Application.Identity.Queries.ConfirmEmail
{

    public class ConfirmEmailQueryHandler : IRequestHandler<ConfirmEmailQuery, ApiResponse<bool>>
    {
        private readonly UserManager<AppUser> _userManager;
        public ConfirmEmailQueryHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        public async Task<ApiResponse<bool>> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
        {

            // 1. Validate if User does Exist
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken: cancellationToken);

            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, request.Token);
                if (result.Succeeded)
                {
                    return new ApiResponse<bool>
                    {
                        IsSuccess = true,
                        Message = "Your Email is confirmed successfully",
                        StatusCode = 200,
                        Response = true
                    };
                }
            }

            return new ApiResponse<bool>
            {
                IsSuccess = false,
                Message = "User does not exists",
                StatusCode = 400,
                Response = false
            };

        }
    }
}