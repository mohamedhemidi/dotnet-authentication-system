using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_core.Application.Identity.Client.DTOs
{
    public class UserProfile
    {
        public required string UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public required bool TwoFactorLoginEnabled { get; set; }
        public required DateTime AccountCreatedAt { get; set; }
    }
}