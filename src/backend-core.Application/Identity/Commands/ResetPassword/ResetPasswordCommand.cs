using backend_core.Application.Identity.DTOs;
using backend_core.Application.Identity.DTOs.Account;
using MediatR;

namespace backend_core.Application.Modules.Client.Account;

public record ResetPasswordCommand(
    ResetPasswordDTO resetPasswordDTO,
    string Token,
    string Email
) : IRequest<bool>;
