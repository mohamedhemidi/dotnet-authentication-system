using backend_core.Application.Account.Common;
using ErrorOr;
using MediatR;

namespace backend_core.Application;

public record RegisterCommand(
    string Username, 
    string Email, 
    string Password
) : IRequest<ErrorOr<AccountResult>> ;
