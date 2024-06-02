using backend_core.Application.Identity.Interfaces;
using backend_core.Domain.Entities;
using MediatR;
using backend_core.Domain.Repositories;
using backend_core.Application.Common.Exceptions;
using backend_core.Application.Modules.Client.Account.Commands.Register;
using backend_core.Application.Identity.DTOs.Account;
using Microsoft.AspNetCore.Identity;
using backend_core.Domain.Interfaces;

namespace backend_core.Application.Modules.Client.Account;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AccountResultDTO>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IEmailSender _emailSender;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public RegisterCommandHandler(
        UserManager<User> userManager, 
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

        // Check if User Exists:

        // Create user :
        var newUser = new User
        {
            Email = command.registerDTO.Email,
            UserName = command.registerDTO.UserName,
        };

        await _unitOfWork.StartTransactionAsync(cancellationToken);

        var createdUser = await _userManager.CreateAsync(newUser, command.registerDTO.Password);

        if (createdUser.Succeeded)
        {
            var roleResult = await _userManager.AddToRoleAsync(newUser, "User");
            await _unitOfWork.SubmitTransactionAsync(cancellationToken);
            if (roleResult.Succeeded)
            {
                var token = _jwtTokenGenerator.GenerateToken(newUser);
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
                // throw new BadRequestException(roleResult.Errors);
                await _unitOfWork.RevertTransactionAsync(cancellationToken);
                throw new BadRequestException(createdUser.Errors.ToString());
            }
        }
        else
        {
            // await _unitOfWork.RevertTransactionAsync(cancellationToken);
            throw new BadRequestException("An error occured!");
        }
    }
}
