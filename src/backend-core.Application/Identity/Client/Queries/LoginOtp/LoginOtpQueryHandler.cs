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

    public class LoginOtpQueryHandler : IRequestHandler<LoginOtpQuery, ApiResponse<AccountResultDTO>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        public LoginOtpQueryHandler(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signinManager,
            IJwtTokenGenerator jwtTokenGenerator,
            IUserRepository userRepository
            )
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
            _signinManager = signinManager;
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<AccountResultDTO>> Handle(LoginOtpQuery query, CancellationToken cancellationToken)
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

                var userRoles = await _userManager.GetRolesAsync(user!);

                var token = _jwtTokenGenerator.GenerateToken(user!, userRoles);

                return new ApiResponse<AccountResultDTO>
                {
                    IsSuccess = true,
                    Message = "You are logged in successfully",
                    StatusCode = 200,
                    Response = new AccountResultDTO(
                                user!.Id,
                                user.UserName!,
                                user.Email!,
                                token
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