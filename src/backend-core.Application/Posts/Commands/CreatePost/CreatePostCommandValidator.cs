using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace backend_core.Application.Posts.Commands.CreatePost
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator()
        {
            RuleFor(x => x.createPostDTO.Title).NotEmpty().WithMessage("{PropertyName} is required").NotNull();
            RuleFor(x => x.createPostDTO.Body).NotEmpty().WithMessage("{PropertyName} is required").NotNull();
        }
    }
}