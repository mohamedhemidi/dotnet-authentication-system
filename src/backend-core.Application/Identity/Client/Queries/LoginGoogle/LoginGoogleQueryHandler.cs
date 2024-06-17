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
using Google.Apis.Auth;
using Microsoft.Extensions.Options;

namespace backend_core.Application.Identity.Queries.Client.LoginGoogle
{

    public class LoginGoogleQueryHandler : IRequestHandler<LoginGoogleQuery, ApiResponse<AuthResultDTO>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly IJwtToken _jwtToken;
        private readonly IRefreshToken _refreshToken;
        private readonly IUnitOfWork _unitOfWork;
        private readonly GoogleSettings _googleSettings;
        public LoginGoogleQueryHandler(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signinManager,
            IJwtToken jwtToken,
            IRefreshToken refreshToken,
            IUnitOfWork unitOfWork,
            IOptions<GoogleSettings> googleSettings)
        {
            _jwtToken = jwtToken;
            _userManager = userManager;
            _signinManager = signinManager;
            _refreshToken = refreshToken;
            _unitOfWork = unitOfWork;
            _googleSettings = googleSettings.Value;
        }

        public async Task<ApiResponse<AuthResultDTO>> Handle(LoginGoogleQuery query, CancellationToken cancellationToken)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new List<string> { _googleSettings.ClientId }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(query.credentials, settings);

            var user = await _userManager.FindByEmailAsync(payload.Email!);

            if (user == null)
            {
                // Create User :
                var newUser = new AppUser
                {
                    Email = payload.Email,
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