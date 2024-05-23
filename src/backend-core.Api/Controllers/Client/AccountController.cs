using Microsoft.AspNetCore.Mvc;
using MediatR;
using backend_core.Application;
using backend_core.Application.Modules.Account.Queries.Login;
using backend_core.Application.DTOs.Account;
using backend_core.Application.DTOs;
using backend_core.Application.Modules.Client.Account;
using backend_core.Application.Modules.Client.Account.Queries.Login;

namespace backend_core.Api.Controllers.Client
{
    [Route("api/account")]
    public class AccountController : BaseController
    {
        private readonly ISender _mediator;
        public AccountController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterDTO request)
        {
            var command = new RegisterCommand(request);
            AccountResultDTO registerResult = await _mediator.Send(command);

            return Ok(registerResult);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO request)
        {
            var query = new LoginQuery(request);
            var loginResult = await _mediator.Send(query);

            return Ok(loginResult);
        }
    }
}