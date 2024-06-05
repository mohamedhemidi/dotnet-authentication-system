using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;

namespace backend_core.Domain.Models
{
    public class EmailMessage
    {
        public required List<MailboxAddress> To { get; set; }
        public required string Subject { get; set; }
        public required string Content { get; set; }

    }
}