using backend_core.Application.Common.Interfaces.Authentication;
using backend_core.Domain.Entities;
using MediatR;
using backend_core.Application.Contracts.Persistance;
using backend_core.Application.DTOs.Account;
using backend_core.Application.Modules.Account.Commands.Register;
using backend_core.Application.Common.Exceptions;

namespace backend_core.Application.Modules.Account;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AccountResultDTO>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
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
