﻿using backend_core.Application.Identity.Client.DTOs;
using backend_core.Application.Identity.DTOs;
using backend_core.Application.Identity.DTOs.Account;
using backend_core.Domain.Common;
using MediatR;

namespace backend_core.Application.Identity.Client.Commands.UpdateUserProfile;

public record UpdateUserProfileCommand(
    string id,
    UpdateUserProfileDTO updateUserProfileDTO
) : IRequest<ApiResponse<bool>>;
