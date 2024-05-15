using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_core.Application.DTOs;

public record LoginDTO(
    string Email,
    string Password
);
