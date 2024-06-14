using backend_core.Domain.Models;

namespace backend_core.Application.Identity.DTOs.Account;

public record AuthResultDTO(
    TokenType? AccessToken = null,
    TokenType? RefreshToken = null
);
