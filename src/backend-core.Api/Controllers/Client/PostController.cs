using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Application.Posts.Commands.CreatePost;
using backend_core.Application.Posts.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace backend_core.Api.Controllers.Client
{
    [Route("api/post")]
    public class PostController : ControllerBase
    {
        private readonly ISender _mediator;
        public PostController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
         public async Task<IActionResult> Create([FromBody]CreatePostDTO request)
        {
            var command = new CreatePostCommand(request);
            PostDTO postResult = await _mediator.Send(command);

            return Ok(postResult);

        }

    }
}