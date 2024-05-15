using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Domain.Entities;

namespace backend_core.Application.DTOs;

public record AccountResultDTO(
    Guid Id,
    string Email,
    string Username,
    string Token
);
