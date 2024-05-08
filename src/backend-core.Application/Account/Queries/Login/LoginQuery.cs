using backend_core.Application.Account.Common;
using ErrorOr;
using MediatR;

namespace backend_core.Application.Account.Queries.Login;

public record LoginQuery(
    string Email,
    string Password
) : IRequest<ErrorOr<AccountResult>>;
