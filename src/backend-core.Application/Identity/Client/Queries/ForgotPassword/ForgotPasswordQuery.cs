using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Domain.Common;
using MediatR;

namespace backend_core.Application.Identity.Client.Queries.ForgotPassword
{
    public record ForgotPasswordQuery(
        string EmailOrUsername,
        string Uri
    ): IRequest<ApiResponse<bool>>;

}