using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_core.Application.Identity.Client.DTOs
{
    public class LoginGoogleDTO
    {
        public required string credentials { get; set; }
    }
}