using System.Security.Claims;
using backend_core.Application.Identity.Client.DTOs;
using backend_core.Application.Identity.DTOs.Account;
using backend_core.Domain.Common;
using backend_core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace backend_core.Application.Identity.Client.Queries.GetUserProfile;

public record GetUserProfileQuery(
    ClaimsPrincipal context
) : IRequest<ApiResponse<UserProfile>>;
