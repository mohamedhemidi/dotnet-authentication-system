using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Application.Identity.DTOs.Account;
using backend_core.Domain.Common;
using backend_core.Domain.Models;
using MediatR;

namespace backend_core.Application.Identity.Client.Queries.RenewToken
{
    public record RenewTokenQuery(
        AuthResultDTO tokens
    ) : IRequest<ApiResponse<TokenType>>;

}