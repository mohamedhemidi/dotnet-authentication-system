using backend_core.Application.Account.Common;
using backend_core.Application.Common.Interfaces.Authentication;
using backend_core.Application.Common.Interfaces.Persistence;
using backend_core.Domain.Common.Errors;
using backend_core.Domain.Entities;
using ErrorOr;
using MediatR;

namespace backend_core.Application;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AccountResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    public async Task<ErrorOr<AccountResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        // Check if User Exists:
        if (_userRepository.GetUserByEmail(command.Email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }
        // Create user (Generate unique ID):
        var newUser = new User
        {
            Username = command.Username,
            Email = command.Email,
            Password = command.Password
        };

        _userRepository.Add(newUser);
        // Create JWT Token:

        var token = _jwtTokenGenerator.GenerateToken(newUser.Id, newUser.Username, newUser.Email);

        return new AccountResult(newUser, token);
    }
}
