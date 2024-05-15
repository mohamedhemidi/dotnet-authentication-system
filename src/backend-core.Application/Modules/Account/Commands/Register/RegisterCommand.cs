using backend_core.Application.DTOs;
using MediatR;

namespace backend_core.Application;

public record RegisterCommand(
    RegisterDTO registerDTO
) : IRequest<AccountResultDTO>;
