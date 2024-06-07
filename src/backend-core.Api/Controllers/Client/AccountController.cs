using Microsoft.AspNetCore.Mvc;
using MediatR;
using backend_core.Application;
using backend_core.Application.Modules.Account.Queries.Login;
using backend_core.Application.Modules.Client.Account;
using backend_core.Application.Modules.Client.Account.Queries.Login;
using backend_core.Application.Identity.DTOs;
using backend_core.Application.Identity.DTOs.Account;
using backend_core.Application.Identity.Queries.Login;
using backend_core.Application.Identity.Queries.ConfirmEmail;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace backend_core.Api.Controllers.Client
{
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly ISender _mediator;
        public AccountController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO request)
        {
            var emailConfirmUri = Url.Action(
                    action: nameof(ConfirmEmail),
                    controller: "account",
                    values: null,
                    protocol: Request.Scheme,
                    host: Request.Host.ToUriComponent()
                );

            var command = new RegisterCommand(request, emailConfirmUri!);
            AccountResultDTO registerResult = await _mediator.Send(command);

            return Ok(registerResult);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO request)
        {
            var query = new LoginQuery(request);
            var loginResult = await _mediator.Send(query);

            return Ok(loginResult);
        }
        [HttpPost("login-2fa")]
        public async Task<IActionResult> LoginWithOTP([FromBody] LoginTwoFactorsDTO request)
        {
            var query = new LoginOtpQuery(request);
            var loginResult = await _mediator.Send(query);

            return Ok(loginResult);
        }
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var query = new ConfirmEmailQuery(token, email);
            var confirmationResult = await _mediator.Send(query);
            return Ok(confirmationResult);
        }
        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO request)
        {
            var resetPasswordUri = Url.Action(
                    action: nameof(ResetPassword),
                    controller: "account",
                    values: null,
                    protocol: Request.Scheme,
                    host: Request.Host.ToUriComponent()
                );
            var command = new ForgotPasswordQuery(request.Email, resetPasswordUri!);
            var confirmationResult = await _mediator.Send(command);
            return Ok(confirmationResult);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(string Token, string Email, [FromBody] ResetPasswordDTO request)
        {
            var command = new ResetPasswordCommand(request, Token, Email);
            var resetPasswordResult = await _mediator.Send(command);
            return Ok(resetPasswordResult);
        }
    }
}