using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_core.Application.Identity.Admin.DTOs
{
    public class ListUsersDTO
    {
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? PhoneNumber { get; set; }
        public IList<string>? Roles { get; set; }
    }
}