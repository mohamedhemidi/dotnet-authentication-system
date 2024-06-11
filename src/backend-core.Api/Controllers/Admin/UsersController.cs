using backend_core.Application.Identity.Admin.Commands.AssignRoles;
using backend_core.Application.Identity.Admin.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_core.Api.Controllers.Admin
{

    [Route("api/admin/users")]
    public class UsersController : ControllerBase
    {
        private readonly ISender _mediator;
        public UsersController(ISender mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Roles = "Super Admin,Admin")]
        [HttpGet("list")]
        public IActionResult ListUsers()
        {
            var users = new List<string> { "user1", "user2", "user3948" };

            return Ok(users);
        }

        [Authorize(Roles = "Super Admin")]
        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleDTO request)
        {
            var command = new AssignRolesCommand(request.EmailOrUsername, request.Roles);
            var assignRoleResult = await _mediator.Send(command);
            return Ok(assignRoleResult);
        }
    }
}