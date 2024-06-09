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

namespace backend_core.Application.Identity.Client.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ApiResponse<AccountResultDTO>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IEmailSender _emailSender;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    private readonly IUnitOfWork _unitOfWork;

    public RegisterCommandHandler(
        UserManager<AppUser> userManager,
        IUnitOfWork unitOfWork,
        IEmailSender emailSender,
        IJwtTokenGenerator jwtTokenGenerator
        )
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _emailSender = emailSender;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    public async Task<ApiResponse<AccountResultDTO>> Handle(RegisterCommand command, CancellationToken cancellationToken)
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
            var roleResult = await _userManager.AddToRoleAsync(newUser, "User");

            if (roleResult.Succeeded)
            {
                var token = _jwtTokenGenerator.GenerateToken(newUser, ["User"]);

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

                return new ApiResponse<AccountResultDTO>
                {
                    IsSuccess = true,
                    Message = "Registered successfully, Please check your email to confirm",
                    StatusCode = 200,
                    Response = new AccountResultDTO
                    (
                       newUser.Id,
                       newUser.Email,
                       newUser.UserName,
                       token
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
