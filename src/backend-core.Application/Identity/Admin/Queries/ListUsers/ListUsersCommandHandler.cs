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
using backend_core.Application.Identity.Admin.DTOs;

namespace backend_core.Application.Identity.Admin.Commands.ListUsers;

public class ListUsersCommandHandler : IRequestHandler<ListUsersCommand, ApiResponse<List<ListUsersDTO>>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ListUsersCommandHandler(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse<List<ListUsersDTO>>> Handle(ListUsersCommand command, CancellationToken cancellationToken)
    {
        var List = new List<ListUsersDTO>();
        var Users = await _unitOfWork.GetRepository<AppUser>().GetAll();
        foreach (var user in Users)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            List.Add(new ListUsersDTO
            {
                Email = user.Email,
                Username = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Roles = userRoles,
            });
        }

        return new ApiResponse<List<ListUsersDTO>>
        {
            IsSuccess = true,
            Message = "Users list fetched successfully",
            StatusCode = 200,
            Response = List
        };
    }
}
