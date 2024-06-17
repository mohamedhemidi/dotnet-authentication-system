using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_core.Domain.Models
{
    public class FacebookSettings
    {
        public required string AppId {get; set;}
        public required string AppSecret {get; set;}
    }
}