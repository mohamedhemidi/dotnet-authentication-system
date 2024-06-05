using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_core.Infrastructure.Mail.Models
{
    public class EmailSettings
    {
        public required string SenderName { get; set; }
        public required string From { get; set; }
        public required string SmtpServer { get; set; }
        public int Port { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}