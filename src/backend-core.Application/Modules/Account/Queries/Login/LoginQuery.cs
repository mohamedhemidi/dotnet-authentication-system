using backend_core.Application.DTOs;
using MediatR;

namespace backend_core.Application.Modules.Account.Queries.Login;

public record LoginQuery(
    LoginDTO loginDTO
) : IRequest<AccountResultDTO>;
