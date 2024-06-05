using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Domain.Entities;

namespace backend_core.Application.Posts.DTOs
{
    public class PostDTO
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Body { get; set; }
    }
}