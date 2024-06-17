using backend_core.Application.Identity.DTOs;
using backend_core.Application.Identity.DTOs.Account;
using backend_core.Domain.Common;
using MediatR;

namespace backend_core.Application.Identity.Queries.Client.LoginFacebook;

public record LoginFacebookQuery(
    string accessToken
) : IRequest<ApiResponse<AuthResultDTO>>;
