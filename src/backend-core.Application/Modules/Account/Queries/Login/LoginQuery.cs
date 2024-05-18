using backend_core.Application.DTOs.Account;
using MediatR;

namespace backend_core.Application.Modules.Account.Queries.Login;

public record LoginQuery(
    LoginDTO loginDTO
) : IRequest<AccountResultDTO>;
