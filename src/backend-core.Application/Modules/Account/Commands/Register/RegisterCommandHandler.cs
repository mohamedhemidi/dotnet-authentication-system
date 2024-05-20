using backend_core.Application.Common.Interfaces.Authentication;
using backend_core.Domain.Entities;
using MediatR;
using backend_core.Application.Contracts.Persistance;
using backend_core.Application.DTOs.Account;
using backend_core.Application.Modules.Account.Commands.Register;
using backend_core.Application.Common.Exceptions;
using backend_core.Application.Models;
using backend_core.Application.Contracts.Infrastructure;

namespace backend_core.Application.Modules.Account;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AccountResultDTO>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IEmailSender _emailSender;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository, IEmailSender emailSender)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _emailSender = emailSender;
    }
    public async Task<AccountResultDTO> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        // Data Validation :

        var validator = new RegisterCommandValidator();
        var validationResult = await validator.ValidateAsync(command.registerDTO);
        if (validationResult.IsValid == false)
            throw new ValidationException(validationResult);

        // Check if User Exists:

        // Create user (Generate unique ID):
        var newUser = new User
        {
            Email = command.registerDTO.Email,
            Username = command.registerDTO.Username,
            Password = command.registerDTO.Password
        };

        await _userRepository.Create(newUser);
        // Create JWT Token:

        var token = _jwtTokenGenerator.GenerateToken(newUser.Id, newUser.Username, newUser.Email);

        var email = new Email
        {
            To = "mail@mail.com",
            Body = $"Account with email {newUser.Email} is successfully created on data {newUser.Created_at:D}!",
            Subject = "Account created"
        };
        try
        {
            await _emailSender.SendEmail(email);
        }
        catch (System.Exception)
        {

            throw;
        }

        return new AccountResultDTO(newUser.Id, newUser.Email, newUser.Username, token);
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
