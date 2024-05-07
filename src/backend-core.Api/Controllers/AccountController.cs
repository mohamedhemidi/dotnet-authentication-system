using backend_core.Application.Services.Account;
using backend_core.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;

namespace backend_core.Api.Controllers
{
    [Route("api/account")]
    public class AccountController : ApiController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            ErrorOr<AccountResult> registerResult = _accountService.Register(request.Username, request.Email, request.Password);

            return registerResult.Match(
                registerResult => Ok(MapAuthResult(registerResult)),
               errors => Problem(errors)
            );

        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var loginResult = _accountService.Login(request.Email, request.Password);
            return loginResult.Match(
                loginResult => Ok(MapAuthResult(loginResult)),
               errors => Problem(errors)
            );
        }


        private static AccountResponse MapAuthResult(AccountResult registerResult)
        {
            return new AccountResponse(
                            registerResult.Id,
                            registerResult.Username,
                            registerResult.Email,
                            registerResult.Token
                        );
        }
    }
}