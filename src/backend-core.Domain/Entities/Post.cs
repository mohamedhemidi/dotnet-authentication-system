using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Domain.Common;

namespace backend_core.Domain.Entities
{
    public class Post : BaseDomainEntity
    {
        public required string Title { get; set; }
        public required string Body { get; set; }
    }
}