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
using backend_core.Domain.Common;

namespace backend_core.Application.Identity.Queries.Client.LoginOtp
{

    public class LoginOtpQueryHandler : IRequestHandler<LoginOtpQuery, ApiResponse<AuthResultDTO>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly IJwtToken _jwtToken;
        private readonly IRefreshToken _refreshToken;
        private readonly IUserRepository _userRepository;
        public LoginOtpQueryHandler(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signinManager,
            IJwtToken jwtToken,
            IUserRepository userRepository,
            IRefreshToken refreshToken
            )
        {
            _jwtToken = jwtToken;
            _userManager = userManager;
            _signinManager = signinManager;
            _userRepository = userRepository;
            _refreshToken = refreshToken;
        }

        public async Task<ApiResponse<AuthResultDTO>> Handle(LoginOtpQuery query, CancellationToken cancellationToken)
        {
            //// 1. Check if user exists:

            var user = await _userRepository.FindByEmailOrUsernameAsync(query.loginTwoFactorsDTO.EmailOrUsername);
            if (user == null)
            {
                throw new NotFoundException(nameof(user), query.loginTwoFactorsDTO.EmailOrUsername);
            }

            //// 2. Validate OTP Code
            var signin = await _signinManager.TwoFactorSignInAsync("Email", query.loginTwoFactorsDTO.Code, false, false);
            if (signin.Succeeded)
            {
                //// 3. Create JWT Token and Assign Roles

                var accessToken = await _jwtToken.GenerateToken(user!);

                //// 4. Generate Refresh Token :
                var refreshToken = _refreshToken.GenerateRefreshToken();
                user.RefreshToken = refreshToken.Token;
                user.RefreshTokenExpiry = refreshToken.ExpiryDate;

                await _userManager.UpdateAsync(user);

                return new ApiResponse<AuthResultDTO>
                {
                    IsSuccess = true,
                    Message = "You are logged in successfully",
                    StatusCode = 200,
                    Response = new AuthResultDTO(
                            accessToken,
                            refreshToken
                    )
                };
            }
            else
            {
                throw new BadRequestException("Invalid Code");
            }
        }
    }
}