using backend_core.Application.Services.Account;
using backend_core.Contracts.Account;
using Microsoft.AspNetCore.Mvc;

namespace backend_core.Api.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            var registerResult = _accountService.Register(request.Email, request.Username, request.Password);

            var response = new AccountResponse(
                registerResult.Id,
                registerResult.Email,
                registerResult.Username,
                registerResult.Token
            );
            return Ok(response);
        }
        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var loginResult = _accountService.Login(request.Email, request.Password);
            var response = new AccountResponse(
                loginResult.Id,
                loginResult.Email,
                loginResult.Username,
                loginResult.Token
            );
            return Ok(response);
        }
    }
}