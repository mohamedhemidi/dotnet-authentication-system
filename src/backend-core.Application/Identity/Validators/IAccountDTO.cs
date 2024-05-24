using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_core.Application.Identity.Validators
{
    public interface IAccountDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    };

}