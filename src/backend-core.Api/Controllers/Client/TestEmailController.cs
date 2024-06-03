using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using backend_core.Domain.Interfaces;
using backend_core.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace backend_core.Api.Controllers.Client
{
    [Route("api/email")]
    public class TestEmailController : ControllerBase
    {
        private readonly IEmailSender _emailSender;

        public TestEmailController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpPost("testsend")]
        public IActionResult Send()
        {
            var EmailRecipents = new List<MailboxAddress>();

            // foreach(var recipent in request)
            // {
            //     EmailRecipents.Add(recipent);
            // }

            var email1 = new MailboxAddress("name","mail@mail.com");

            EmailRecipents.Add(email1);

            var message = new EmailMessage()
            {
                To = EmailRecipents,
                Content = "test",
                Subject = "test"
            };
            _emailSender.SendEmail(message);
            return Ok("Email Sent Successfully");

        }


    }
}