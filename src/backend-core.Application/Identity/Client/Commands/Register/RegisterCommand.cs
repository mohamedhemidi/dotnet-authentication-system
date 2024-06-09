using backend_core.Application.Identity.DTOs;
using backend_core.Application.Identity.DTOs.Account;
using backend_core.Domain.Common;
using MediatR;

namespace backend_core.Application.Identity.Client.Commands.Register;

public record RegisterCommand(
    RegisterDTO registerDTO,
    string Uri
) : IRequest<ApiResponse<AccountResultDTO>>;
