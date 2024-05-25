using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Application.Posts.DTOs;
using FluentValidation;

namespace backend_core.Application.Posts.Queries.GetPosts
{
    public class GetPostsQueryValidator : AbstractValidator<GetPostsQuery>
    {
        public GetPostsQueryValidator()
        {
            
        }
    }
}