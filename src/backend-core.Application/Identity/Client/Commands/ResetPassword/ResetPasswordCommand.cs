using backend_core.Application.Identity.DTOs;
using backend_core.Application.Identity.DTOs.Account;
using backend_core.Domain.Common;
using MediatR;

namespace backend_core.Application.Identity.Client.Commands.ResetPassword;

public record ResetPasswordCommand(
    ResetPasswordDTO resetPasswordDTO,
    string Token,
    string Email
) : IRequest<ApiResponse<bool>>;
