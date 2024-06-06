using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Domain.Models;

namespace backend_core.Domain.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmail(EmailMessage message);
    }
}