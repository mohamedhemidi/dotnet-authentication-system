using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Application.Common.Exceptions;
using backend_core.Domain.Common;
using backend_core.Domain.Entities;
using backend_core.Domain.Interfaces;
using backend_core.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace backend_core.Application.Identity.Queries.ConfirmEmail
{

    public class ForgotPasswordQueryHandler : IRequestHandler<ForgotPasswordQuery, ApiResponse<bool>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IEmailBodyBuilder _emailBodyBuilder;
        public ForgotPasswordQueryHandler(UserManager<AppUser> userManager, IEmailSender emailSender, IEmailBodyBuilder emailBodyBuilder)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _emailBodyBuilder = emailBodyBuilder;
        }


        public async Task<ApiResponse<bool>> Handle(ForgotPasswordQuery query, CancellationToken cancellationToken)
        {

            // 1. Validate if User does Exist
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == query.Email, cancellationToken: cancellationToken);

            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var uriBuilder = new UriBuilder(query.Uri);
                var queryParams = new Dictionary<string, string>
                {
                    { "token", token },
                    { "email", user.Email! }
                };

                uriBuilder.Query = QueryHelpers.AddQueryString(string.Empty, queryParams!).TrimStart('?');

                var resetPasswordLink = uriBuilder.ToString();

                var passwordRestEmail = new EmailMessage()
                {
                    To = new List<MailboxAddress>() { new MailboxAddress("", user.Email) },
                    Subject = "Reset your password",
                    Content = await _emailBodyBuilder.GetRestPasswordEmailBodyAsync(resetPasswordLink)
                };
                await _emailSender.SendEmail(passwordRestEmail);

                return new ApiResponse<bool>
                {
                    IsSuccess = true,
                    Message = "Please check your email to reset your password ",
                    StatusCode = 200,
                    Response = true
                };

            }
            return new ApiResponse<bool>
            {
                IsSuccess = false,
                Message = "User does not exists",
                StatusCode = 400,
                Response = false
            };

        }
    }
}