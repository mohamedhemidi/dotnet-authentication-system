using backend_core.Application.Identity.Interfaces;
using backend_core.Domain.Entities;
using MediatR;
using backend_core.Domain.Repositories;
using backend_core.Application.Common.Exceptions;
using backend_core.Application.Modules.Client.Account.Commands.Register;
using backend_core.Application.Identity.DTOs.Account;
using Microsoft.AspNetCore.Identity;
using backend_core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Routing;
using backend_core.Domain.Models;
using MimeKit;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using backend_core.Domain.Common;

namespace backend_core.Application.Modules.Client.Account;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ApiResponse<bool>>
{
    private readonly UserManager<AppUser> _userManager;

    public ResetPasswordCommandHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ApiResponse<bool>> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
    {
        // 1. Validate if User does Exist
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == command.Email, cancellationToken: cancellationToken);

        if (user != null)
        {
            var resetPasswordResult = await _userManager.ResetPasswordAsync(user, command.Token, command.resetPasswordDTO.Password);
            if (resetPasswordResult.Succeeded)
            {
                return new ApiResponse<bool>
                {
                    IsSuccess = true,
                    Message = "Your password has been changed successfully",
                    StatusCode = 200,
                    Response = true
                };
            }
            else
            {
                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = "An error occured while changing your password",
                    StatusCode = 400,
                    Response = false
                };
            }
        }
        return new ApiResponse<bool>
        {
            IsSuccess = false,
            Message = "User does not exist",
            StatusCode = 400,
            Response = false
        };
    }
}
