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

namespace backend_core.Application.Identity.Client.Queries.Login
{

    public class LoginQueryHandler : IRequestHandler<LoginQuery, ApiResponse<AccountResultDTO>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IEmailSender _emailSender;
        private readonly IUserRepository _userRepository;
        public LoginQueryHandler(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signinManager,
            IJwtTokenGenerator jwtTokenGenerator,
            IEmailSender emailSender,
            IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
            _signinManager = signinManager;
            _emailSender = emailSender;
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<AccountResultDTO>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {

            // 1. Validate if User does Exist
            var user = await _userRepository.FindByEmailOrUsernameAsync(query.loginDTO.EmailOrUsername);
            if (user == null)
            {
                throw new NotFoundException(nameof(user), query.loginDTO.EmailOrUsername);
            }

            ////(If user enabled 2FA) :
            if (user.TwoFactorEnabled)
            {
                await _signinManager.SignOutAsync();
                await _signinManager.PasswordSignInAsync(user, query.loginDTO.Password, false, true);

                var twoFactorsToken = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

                var confirmationOtpEmail = new EmailMessage()
                {
                    To = new List<MailboxAddress>() { new("", user.Email) },
                    Subject = "OTP confirmation",
                    Content = twoFactorsToken
                };
                await _emailSender.SendEmail(confirmationOtpEmail);

                return new ApiResponse<AccountResultDTO>
                {
                    IsSuccess = true,
                    Message = "An OTP message is sent to your email",
                    StatusCode = 200,
                    Response = new AccountResultDTO(
                                    user.Id,
                                    user.UserName!,
                                    user.Email!
                               )
                };
            }

            //// 2. Validate if Password Is Correct
            var result = await _signinManager.CheckPasswordSignInAsync(user, query.loginDTO.Password, false);
            if (!result.Succeeded)
            {
                throw new BadRequestException("Credentials Does Not Match");
            }
            //// 3. Create JWT Token and Assign Roles

            var userRoles = await _userManager.GetRolesAsync(user);

            var token = _jwtTokenGenerator.GenerateToken(user, userRoles);


            return new ApiResponse<AccountResultDTO>
            {
                IsSuccess = true,
                Message = "You are logged in successfully",
                StatusCode = 200,
                Response = new AccountResultDTO(
                                user.Id,
                                user.Email!,
                                user.UserName!,
                                token
                            )
            };
        }
    }
}