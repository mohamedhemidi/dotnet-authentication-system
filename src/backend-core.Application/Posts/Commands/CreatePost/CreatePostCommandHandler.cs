using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Application.Common.Exceptions;
using backend_core.Domain.Repositories;
using backend_core.Application.Identity;
using backend_core.Application.Posts.DTOs;
using backend_core.Domain.Entities;
using MediatR;

namespace backend_core.Application.Posts.Commands.CreatePost
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, PostDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CurrentUser _currentUser;

        public CreatePostCommandHandler(IUnitOfWork unitOfWork, CurrentUser currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }
        public async Task<PostDTO> Handle(CreatePostCommand command, CancellationToken cancellationToken)
        {
            // Data Validation :

            var user = _currentUser.UserId;

            var validator = new CreatePostCommandValidator();
            var validationResult = await validator.ValidateAsync(command);
            if (validationResult.IsValid == false)
                throw new FluentValidationException(validationResult);

            // Create new Post :

            var newPost = new Post
            {
                Title = command.createPostDTO.Title,
                Body = command.createPostDTO.Body
            };

            await _unitOfWork.GetRepository<Post>().Create(newPost);

            await _unitOfWork.Save();

            return new PostDTO
            {
                post = newPost
            };
        }
    }
}