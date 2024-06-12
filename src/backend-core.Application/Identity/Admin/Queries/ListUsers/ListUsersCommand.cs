using backend_core.Application.Identity.Admin.DTOs;
using backend_core.Application.Identity.DTOs;
using backend_core.Application.Identity.DTOs.Account;
using backend_core.Domain.Common;
using backend_core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace backend_core.Application.Identity.Admin.Commands.ListUsers;

public record ListUsersCommand(

) : IRequest<ApiResponse<List<ListUsersDTO>>>;
