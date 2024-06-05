using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace backend_core.Application.Identity.Queries.ConfirmEmail
{
    public record ConfirmEmailQuery(
        string Token,
        string Email
    ): IRequest<bool>;

}