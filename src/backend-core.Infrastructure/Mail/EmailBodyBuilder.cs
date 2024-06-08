using System.Reflection;
using backend_core.Domain.Interfaces;
using Microsoft.AspNetCore.Hosting;


namespace backend_core.Infrastructure.Mail
{
    public class EmailBodyBuilder : IEmailBodyBuilder
    {

        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly string basePath;
        public EmailBodyBuilder(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            basePath = Path.Combine(_hostingEnvironment.ContentRootPath, "..", "backend-core.Infrastructure");
        }
        public async Task<string> GetRestPasswordEmailBodyAsync(string actionUrl)
        {
            var templatePath = Path.Combine(basePath, "Mail/Templates/ResetPasswordEmail.html");
            var templateContent = await File.ReadAllTextAsync(templatePath);
            var emailBody = templateContent.Replace("{{ActionUrl}}", actionUrl);
            return emailBody;
        }
    }
}