using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using MS3_LMS.Models;
using MS3_LMS.Models.RequestModel; 

public class EmailService
{
    private readonly ILogger<EmailService> _logger;

    public EmailService(ILogger<EmailService> logger)
    {
        _logger = logger;
    }

    public async Task<bool> SendEmailAsync(string email, string subject, string body)
    {
        try
        {
            _logger.LogInformation("Attempting to send email to: {Email}", email);

            var smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential("sajilan96s@gmail.com", "rvxl ezvf jqvh fmcp"),
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("sajilan96s@gmail.com"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(email);

            await smtpClient.SendMailAsync(mailMessage);

            _logger.LogInformation("Email sent successfully.");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email.");
            return false;
        }
    }

}
