using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_core.Application.Identity.Admin.DTOs
{
    public class AssignRoleDTO
    {
        public required string EmailOrUsername { get; set; }
        public required List<string> Roles { get; set; }
    }
}