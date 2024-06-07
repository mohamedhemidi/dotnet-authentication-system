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

namespace backend_core.Application.Modules.Client.Account;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, bool>
{
    private readonly UserManager<AppUser> _userManager;

    public ResetPasswordCommandHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
    {
        // 1. Validate if User does Exist
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == command.Email, cancellationToken: cancellationToken);

        if (user != null)
        {
            var resetPasswordResult = await _userManager.ResetPasswordAsync(user, command.Token, command.resetPasswordDTO.Password);
            if (resetPasswordResult.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
}
