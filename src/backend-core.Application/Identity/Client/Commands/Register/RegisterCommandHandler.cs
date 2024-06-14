using backend_core.Application.Identity.Common.Interfaces;
using backend_core.Domain.Entities;
using MediatR;
using backend_core.Domain.Repositories;
using backend_core.Application.Common.Exceptions;
using backend_core.Application.Identity.DTOs.Account;
using Microsoft.AspNetCore.Identity;
using backend_core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Routing;
using backend_core.Domain.Models;
using MimeKit;
using Microsoft.AspNetCore.WebUtilities;
using backend_core.Domain.Common;
using backend_core.Domain.Constants;

namespace backend_core.Application.Identity.Client.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ApiResponse<AuthResultDTO>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IEmailSender _emailSender;
    private readonly IJwtToken _jwtToken;

    private readonly IUnitOfWork _unitOfWork;

    public RegisterCommandHandler(
        UserManager<AppUser> userManager,
        IUnitOfWork unitOfWork,
        IEmailSender emailSender,
        IJwtToken jwtToken
        )
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _emailSender = emailSender;
        _jwtToken = jwtToken;
    }
    public async Task<ApiResponse<AuthResultDTO>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        // Check if User Already Exists:
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == command.registerDTO.Email);

        if (user != null)
        {
            throw new BadRequestException($"An account with the email : {command.registerDTO.Email} already exists");
        }
        // Create User :
        var newUser = new AppUser
        {
            Email = command.registerDTO.Email,
            UserName = command.registerDTO.UserName,
        };

        await _unitOfWork.StartTransactionAsync(cancellationToken);

        var createdUser = await _userManager.CreateAsync(newUser, command.registerDTO.Password);

        // Create Role for User:

        if (createdUser.Succeeded)
        {
            var roleResult = await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            if (roleResult.Succeeded)
            {
                var accessToken = await _jwtToken.GenerateToken(newUser);

                // Create And Send Token To Verify By Email

                var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

                var uriBuilder = new UriBuilder(command.Uri);
                var queryParams = new Dictionary<string, string>
                {
                    { "token", emailToken },
                    { "email", newUser.Email }
                };
                uriBuilder.Query = QueryHelpers.AddQueryString(string.Empty, queryParams!).TrimStart('?');

                var confirmationLink = uriBuilder.ToString();

                var confirmationEmail = new EmailMessage()
                {
                    To = new List<MailboxAddress>() { new MailboxAddress("", newUser.Email) },
                    Subject = "Confirm your email",
                    Content = confirmationLink
                };
                await _emailSender.SendEmail(confirmationEmail);

                await _unitOfWork.SubmitTransactionAsync(cancellationToken);

                return new ApiResponse<AuthResultDTO>
                {
                    IsSuccess = true,
                    Message = "Registered successfully, Please check your email to confirm",
                    StatusCode = 200,
                    Response = new AuthResultDTO
                    (
                       accessToken
                    )
                };

            }
            else
            {
                await _unitOfWork.RevertTransactionAsync(cancellationToken);
                throw new BadRequestException(createdUser.Errors.ToString()!);
            }
        }
        else
        {
            await _unitOfWork.RevertTransactionAsync(cancellationToken);
            throw new BadRequestException("An error occured!");
        }
    }
}
