using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_core.Application.Posts.DTOs
{
    public class CreatePostDTO
    {
        public string Title { get; set; }
        public string Body { get; set; }
    }
}