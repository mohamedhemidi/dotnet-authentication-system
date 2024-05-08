using backend_core.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using MediatR;
using backend_core.Application;
using backend_core.Application.Account.Queries.Login;
using backend_core.Application.Account.Common;

namespace backend_core.Api.Controllers
{
    [Route("api/account")]
    public class AccountController : ApiController
    {
        private readonly ISender _mediator;
        public AccountController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            var command = new RegisterCommand(request.Username, request.Email, request.Password);
            ErrorOr<AccountResult> registerResult = await _mediator.Send(command);



            return registerResult.Match(
                registerResult => Ok(MapAuthResult(registerResult)),
               errors => Problem(errors)
            );

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = new LoginQuery(request.Email, request.Password);
            var loginResult = await _mediator.Send(query);
            return loginResult.Match(
                loginResult => Ok(MapAuthResult(loginResult)),
               errors => Problem(errors)
            );
        }


        private static AccountResponse MapAuthResult(AccountResult authResult)
        {
            return new AccountResponse(
                            authResult.User.Id,
                            authResult.User.Username,
                            authResult.User.Email,
                            authResult.Token
                        );
        }
    }
}