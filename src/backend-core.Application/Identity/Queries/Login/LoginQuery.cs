using backend_core.Application.Identity.DTOs.Account;
using backend_core.Domain.Common;
using MediatR;

namespace backend_core.Application.Identity.Queries.Login;

public record LoginQuery(
    LoginDTO loginDTO
) : IRequest<ApiResponse<AccountResultDTO>>;
