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
using backend_core.Domain.Constants;

namespace backend_core.Application.Identity.Queries.Client.LoginFacebook
{

    public class LoginFacebookQueryHandler : IRequestHandler<LoginFacebookQuery, ApiResponse<AuthResultDTO>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly IJwtToken _jwtToken;
        private readonly IRefreshToken _refreshToken;
        private readonly IUserRepository _userRepository;
        private readonly IFacebookAuth _facebookAuth;
        private readonly IUnitOfWork _unitOfWork;
        public LoginFacebookQueryHandler(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signinManager,
            IJwtToken jwtToken,
            IUserRepository userRepository,
            IRefreshToken refreshToken,
            IFacebookAuth facebookAuth,
            IUnitOfWork unitOfWork)
        {
            _jwtToken = jwtToken;
            _userManager = userManager;
            _signinManager = signinManager;
            _userRepository = userRepository;
            _refreshToken = refreshToken;
            _facebookAuth = facebookAuth;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<AuthResultDTO>> Handle(LoginFacebookQuery query, CancellationToken cancellationToken)
        {
            var ValidateAccessToken = await _facebookAuth.ValidateFacebookAccessToken(query.accessToken);
            if (!ValidateAccessToken.Data!.IsValid)
            {
                throw new BadRequestException("Token is invalid");
            }
            var userInfo = await _facebookAuth.GetFacebookUserInfo(query.accessToken);

            var user = await _userManager.FindByEmailAsync(userInfo.Email!);
            if (user == null)
            {
                // Create User :
                var newUser = new AppUser
                {
                    Email = userInfo.Email,
                    UserName = Guid.NewGuid().ToString()
                };
                await _unitOfWork.StartTransactionAsync(cancellationToken);
                var createdUser = await _userManager.CreateAsync(newUser);
                if (createdUser.Succeeded)
                {
                    // Assign USER role for user
                    var roleResult = await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                    if (roleResult.Succeeded)
                    {
                        // Generate Access Token
                        var accessToken = await _jwtToken.GenerateToken(newUser);

                        // Generate Access Token
                        var refreshToken = _refreshToken.GenerateRefreshToken();

                        newUser.RefreshToken = refreshToken.Token;
                        newUser.RefreshTokenExpiry = refreshToken.ExpiryDate;
                        newUser.EmailConfirmed = true;

                        await _userManager.UpdateAsync(newUser);


                        await _unitOfWork.SubmitTransactionAsync(cancellationToken);

                        return new ApiResponse<AuthResultDTO>
                        {
                            IsSuccess = true,
                            Message = "You are logged in successfully",
                            StatusCode = 200,
                            Response = new AuthResultDTO(accessToken, refreshToken)
                        };
                    }
                }
                else
                {
                    throw new BadRequestException("Something went wrong");
                }
            }
            else
            {
                var signIn = await _signinManager.CanSignInAsync(user!);
                if (signIn)
                {
                    var accessToken = await _jwtToken.GenerateToken(user!);
                    var refreshToken = _refreshToken.GenerateRefreshToken();

                    user.RefreshToken = refreshToken.Token;
                    user.RefreshTokenExpiry = refreshToken.ExpiryDate;

                    await _userManager.UpdateAsync(user);

                    return new ApiResponse<AuthResultDTO>
                    {
                        IsSuccess = true,
                        Message = "You are logged in successfully",
                        StatusCode = 200,
                        Response = new AuthResultDTO(accessToken, refreshToken)
                    };
                }

            }

            throw new Exception("An error occured");
        }
    }
}