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
        [HttpGet("assign-role/{id:int}")]
        public IActionResult AssignRole([FromRoute] int id)
        {
            return Ok(id);
        }
    }
}