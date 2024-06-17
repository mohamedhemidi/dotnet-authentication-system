using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_core.Domain.Models
{
    public class GoogleSettings
    {
        public required string ClientId { get; set; }
        public required string ClientSecret { get; set; }
    }
}