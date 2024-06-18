using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using backend_core.Application.Identity.Client.Commands.UpdateUserProfile;
using backend_core.Application.Identity.Client.DTOs;
using backend_core.Application.Identity.Client.Queries.GetUserProfile;
using backend_core.Application.Identity.Common.Services;
using backend_core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace backend_core.Api.Controllers.Client
{
    [Authorize]
    [Route("api/profile")]
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ISender _mediator;
        public ProfileController(ISender mediator, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _mediator = mediator;
        }



        // Get Current User
        [HttpGet("")]
        public async Task<IActionResult> GetProfile()
        {
            var query = new GetUserProfileQuery(HttpContext.User);
            var profileResult = await _mediator.Send(query);
            return Ok(profileResult);
        }
        // Update Profile
        [HttpGet("update")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserProfileDTO request)
        {
            var command = new UpdateUserProfileCommand(HttpContext.User, request);
            var updateResult = await _mediator.Send(command);
            return Ok(updateResult);
        }
    }
}