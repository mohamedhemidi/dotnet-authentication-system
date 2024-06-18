using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Application.Identity.Common.Interfaces;
using backend_core.Domain.Entities;
using MediatR;
using backend_core.Domain.Repositories;
using backend_core.Application.Common.Exceptions;
using backend_core.Application.Identity.DTOs.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using backend_core.Domain.Models;
using MimeKit;
using backend_core.Domain.Interfaces;
using backend_core.Application.Identity.DTOs;
using backend_core.Domain.Common;
using backend_core.Application.Identity.Client.DTOs;
using Microsoft.AspNetCore.Http;

namespace backend_core.Application.Identity.Client.Queries.GetUserProfile
{

    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, ApiResponse<UserProfile>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserRepository _userRepository;
        public GetUserProfileQueryHandler(
            UserManager<AppUser> userManager,
            IUserRepository userRepository
            )
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<UserProfile>> Handle(GetUserProfileQuery query, CancellationToken cancellationToken)
        {
            var userId = _userManager.GetUserId(query.context)!;
            var user = await _userManager.FindByIdAsync(userId);

            return new ApiResponse<UserProfile>
            {
                IsSuccess = true,
                Message = "Profile fetched successfully",
                StatusCode = 200,
                Response = new UserProfile
                {
                    UserId = userId,
                    FirstName = user!.FirstName,
                    LastName = user.LastName,
                    UserName = user!.UserName!,
                    PhoneNumber = user!.PhoneNumber!,
                    Email = user!.Email!,
                    TwoFactorLoginEnabled = user.TwoFactorEnabled,
                    AccountCreatedAt = user.CreatedAt
                }
            };


            // throw new NotImplementedException();
        }
    }
}