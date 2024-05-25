using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Application.Posts.DTOs;
using MediatR;

namespace backend_core.Application.Posts.Commands.CreatePost
{
    public record CreatePostCommand(
        CreatePostDTO createPostDTO
    ) : IRequest<PostDTO>;

}