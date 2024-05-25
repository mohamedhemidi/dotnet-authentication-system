using backend_core.Application.Identity.Interfaces;
using backend_core.Domain.Entities;
using MediatR;
using backend_core.Application.Contracts.Persistance;
using backend_core.Application.Common.Exceptions;
using backend_core.Application.Models;
using backend_core.Application.Contracts.Infrastructure;
using backend_core.Application.Modules.Client.Account.Commands.Register;
using backend_core.Application.Identity.DTOs.Account;
using Microsoft.AspNetCore.Identity;

namespace backend_core.Application.Modules.Client.Account;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AccountResultDTO>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IEmailSender _emailSender;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signinManager;

    public RegisterCommandHandler(UserManager<User> userManager, SignInManager<User> signinManager, IJwtTokenGenerator jwtTokenGenerator, IEmailSender emailSender)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _emailSender = emailSender;
        _userManager = userManager;
        _signinManager = signinManager;
    }
    public async Task<AccountResultDTO> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        // Data Validation :

        var validator = new RegisterCommandValidator();
        var validationResult = await validator.ValidateAsync(command.registerDTO);
        if (validationResult.IsValid == false)
            throw new ValidationException(validationResult);

        // Check if User Exists:

        // Create user :
        var newUser = new User
        {
            Email = command.registerDTO.Email,
            UserName = command.registerDTO.UserName,
        };

        var createdUser = await _userManager.CreateAsync(newUser, command.registerDTO.Password);

        if (createdUser.Succeeded)
        {
            var roleResult = await _userManager.AddToRoleAsync(newUser, "User");
            if (roleResult.Succeeded)
            {
                var token = _jwtTokenGenerator.GenerateToken(newUser);
                return
                    new AccountResultDTO
                    (
                       newUser.Id,
                       newUser.UserName,
                       newUser.Email,
                       token
                    )
                ;
            }
            else
            {
                // throw new BadRequestException(roleResult.Errors);
                throw new BadRequestException("RoleResult.Errors");
            }
        }
        else
        {
            throw new BadRequestException("An error Occured");
        }

        // var email = new Email
        // {
        //     To = "mohamed.hemidi@hotmail.com",
        //     Body = $"Account with email {newUser.Email} is successfully created on data!",
        //     Subject = "Account created"
        // };
        // try
        // {
        //     await _emailSender.SendEmail(email);
        // }
        // catch (System.Exception)
        // {

        //     throw;
        // }

        // return MapAuthResult(newUser, token);
    }
    // private static AccountResultDTO MapAuthResult(User authResult, string Token)
    // {
    //     return new AccountResultDTO(
    //                     authResult.Id,
    //                     authResult.Username,
    //                     authResult.Email,
    //                     Token
    //                 );
    // }
}
