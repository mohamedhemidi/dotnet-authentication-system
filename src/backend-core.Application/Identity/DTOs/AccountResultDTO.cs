using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Domain.Entities;

namespace backend_core.Application.Identity.DTOs.Account;

public record AccountResultDTO(
    string Id,
    string Email,
    string Username,
    string Token
);
