using backend_core.Application.DTOs;
using backend_core.Application.DTOs.Account;
using MediatR;

namespace backend_core.Application;

public record RegisterCommand(
    RegisterDTO registerDTO
) : IRequest<AccountResultDTO>;
