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
using backend_core.Domain.Constants;

namespace backend_core.Application.Identity.Admin.Commands.AssignRoles;

public class AssignRolesCommandHandler : IRequestHandler<AssignRolesCommand, ApiResponse<List<string>>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IUserRepository _userRepository;

    public AssignRolesCommandHandler(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IUserRepository userRepository)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _userRepository = userRepository;
    }

    public async Task<ApiResponse<List<string>>> Handle(AssignRolesCommand command, CancellationToken cancellationToken)
    {
        var assignedRoles = new List<string>();

        // 1. Validate if User does Exist
        var user = await _userRepository.FindByEmailOrUsernameAsync(command.EmailOrUsername);
        if (user == null)
        {
            throw new NotFoundException(nameof(user), command.EmailOrUsername);
        }
        var userRoles = await _userManager.GetRolesAsync(user);
        var isSuperAdmin = userRoles.Contains(UserRoles.SuperAdmin);
        if (isSuperAdmin)
        {
            throw new BadRequestException("The provided email is for a Super Admin");
        }

        foreach (var role in command.roles)
        {
            if (role == UserRoles.SuperAdmin)
            {
                throw new BadRequestException($"You can't assign a {UserRoles.SuperAdmin} role");
            }

            if (await _roleManager.RoleExistsAsync(role))
            {

                if (!await _userManager.IsInRoleAsync(user, role))
                {
                    await _userManager.AddToRoleAsync(user, role);
                    assignedRoles.Add(role);

                    return new ApiResponse<List<string>>
                    {
                        IsSuccess = true,
                        Message = "Roles are assigned successfully",
                        StatusCode = 200,
                        Response = assignedRoles
                    };
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, role);
                    return new ApiResponse<List<string>>
                    {
                        IsSuccess = true,
                        Message = "Role has been removed for User",
                        StatusCode = 200,
                        Response = [role]
                    };
                }

            }
            else
            {
                throw new BadRequestException("Role does not exist");
            }
        }
        throw new Exception("An error occured");
    }
}
