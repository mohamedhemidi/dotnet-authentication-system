using backend_core.Application.Identity.DTOs;
using backend_core.Application.Identity.DTOs.Account;
using MediatR;

namespace backend_core.Application.Modules.Client.Account;

public record RegisterCommand(
    RegisterDTO registerDTO
) : IRequest<AccountResultDTO>;
