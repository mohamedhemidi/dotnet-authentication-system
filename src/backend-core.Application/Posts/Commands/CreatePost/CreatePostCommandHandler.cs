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
using backend_core.Domain.Common;

namespace backend_core.Application.Posts.Commands.CreatePost
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, ApiResponse<PostDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePostCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<PostDTO>> Handle(CreatePostCommand command, CancellationToken cancellationToken)
        {
            // Create new Post :

            var newPost = new Post
            {
                Title = command.createPostDTO.Title,
                Body = command.createPostDTO.Body
            };

            await _unitOfWork.GetRepository<Post>().Create(newPost);

            await _unitOfWork.Save();

            return new ApiResponse<PostDTO>
            {
                IsSuccess = true,
                Message = "Post added successfully",
                StatusCode = 200,
                Response = new PostDTO
                {
                    Id = newPost.Id,
                    Title = newPost.Title,
                    Body = newPost.Body,
                }
            };
        }
    }
}