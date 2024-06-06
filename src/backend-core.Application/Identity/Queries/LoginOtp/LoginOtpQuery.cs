using backend_core.Application.Identity.DTOs;
using backend_core.Application.Identity.DTOs.Account;
using MediatR;

namespace backend_core.Application.Identity.Queries.Login;

public record LoginOtpQuery(
    LoginTwoFactorsDTO loginTwoFactorsDTO
) : IRequest<AccountResultDTO>;
