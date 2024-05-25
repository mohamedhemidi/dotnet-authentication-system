using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace backend_core.Application.Identity
{
    public class CurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId => Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue("UserId") ?? default(Guid).ToString());

        public string IdentityId => _httpContextAccessor.HttpContext?.User.FindFirstValue("IdentityId") ?? string.Empty;
    }
}