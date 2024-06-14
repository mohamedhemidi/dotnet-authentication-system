using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Domain.Models;

namespace backend_core.Application.Identity.Common.Interfaces
{
    public interface IRefreshToken
    {
        TokenType GenerateRefreshToken();
    }
}