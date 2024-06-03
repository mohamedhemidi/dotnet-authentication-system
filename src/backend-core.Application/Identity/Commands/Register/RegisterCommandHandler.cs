using backend_core.Application.Identity.Interfaces;
using backend_core.Domain.Entities;
using MediatR;
using backend_core.Domain.Repositories;
using backend_core.Application.Common.Exceptions;
using backend_core.Application.Modules.Client.Account.Commands.Register;
using backend_core.Application.Identity.DTOs.Account;
using Microsoft.AspNetCore.Identity;
using backend_core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Routing;
using backend_core.Domain.Models;
using MimeKit;

namespace backend_core.Application.Modules.Client.Account;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AccountResultDTO>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IEmailSender _emailSender;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<AppUser> _userManager;

    public RegisterCommandHandler(
        UserManager<AppUser> userManager,
        IJwtTokenGenerator jwtTokenGenerator,
        IEmailSender emailSender, 
        IUnitOfWork unitOfWork)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _emailSender = emailSender;
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }
    public async Task<AccountResultDTO> Handle(RegisterCommand command, CancellationToken cancellationToken)
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
                var token = _jwtTokenGenerator.GenerateToken(newUser);
                await _unitOfWork.SubmitTransactionAsync(cancellationToken);

                // Create Token To Verify By Email

                var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                string confirmationLink = $"http://localhost:5172/api/account/confirm-email?token={emailToken}&email={newUser.Email}";
                var EmailRecipents = new List<MailboxAddress>();
                var userEmail = new MailboxAddress("", newUser.Email);
                EmailRecipents.Add(userEmail);
                var confirmationEmail = new EmailMessage()
                { To = EmailRecipents, Subject = "Confirm your email", Content = confirmationLink };
                _emailSender.SendEmail(confirmationEmail);

                return
                    new AccountResultDTO
                    (
                       newUser.Id,
                       newUser.Email,
                       newUser.UserName,
                       token
                    )
                ;
            }
            else
            {
                await _unitOfWork.RevertTransactionAsync(cancellationToken);
                throw new BadRequestException(createdUser.Errors.ToString());
            }
        }
        else
        {
            await _unitOfWork.RevertTransactionAsync(cancellationToken);
            throw new BadRequestException("An error occured!");
        }
    }
}
