using backend_core.Application.Identity.Common.Interfaces;
using backend_core.Domain.Entities;
using MediatR;
using backend_core.Domain.Repositories;
using backend_core.Application.Common.Exceptions;
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

namespace backend_core.Application.Identity.Client.Commands.UpdateUserProfile;

public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, ApiResponse<bool>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUserRepository _userRepository;

    public UpdateUserProfileCommandHandler(UserManager<AppUser> userManager, IUserRepository userRepository)
    {
        _userManager = userManager;
        _userRepository = userRepository;
    }

    public async Task<ApiResponse<bool>> Handle(UpdateUserProfileCommand command, CancellationToken cancellationToken)
    {
        var currentUserId = _userManager.GetUserId(command.context)!;

        // Check if User Already Exists:

        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == currentUserId, cancellationToken: cancellationToken);

        if (user == null)
        {
            throw new NotFoundException(nameof(user), currentUserId);
        }

        if (command.updateUserProfileDTO.FirstName != null)
        {
            user.FirstName = command.updateUserProfileDTO.FirstName == string.Empty ? null : command.updateUserProfileDTO.FirstName;
        }
        if (command.updateUserProfileDTO.LastName != null)
        {
            user.LastName = command.updateUserProfileDTO.LastName == string.Empty ? null : command.updateUserProfileDTO.LastName;
        }
        if (command.updateUserProfileDTO.UserName != null)
        {
            // Check if username is available
            var existingUserName = await _userManager.FindByNameAsync(command.updateUserProfileDTO.UserName);
            if (existingUserName != null)
            {
                throw new BadRequestException("Username already exists");
            }
            user.UserName = command.updateUserProfileDTO.UserName;
        }
        if (command.updateUserProfileDTO.PhoneNumber != null)
        {
            user.PhoneNumber = command.updateUserProfileDTO.PhoneNumber == string.Empty ? null : command.updateUserProfileDTO.PhoneNumber;
        }
        if (command.updateUserProfileDTO.TwoFactorLoginEnabled != null)
        {
            user.TwoFactorEnabled = (bool)command.updateUserProfileDTO.TwoFactorLoginEnabled;
        }

        await _userManager.UpdateAsync(user);
        return new ApiResponse<bool>
        {
            IsSuccess = true,
            Message = "Your profile has been changed successfully",
            StatusCode = 200,
            Response = true
        };
    }
}
