using Mailjet.Client;
using Microsoft.AspNetCore.Identity.UI.Services;
using Newtonsoft.Json.Linq;
using ILogger = Serilog.ILogger;

namespace EShop.Presentation.Services;

public class EmailSender : IEmailSender
{
    private readonly ILogger _logger;
    private readonly string _publicKey;
    private readonly string _privateKey;
    private readonly string _emailFrom;

    public EmailSender(IConfiguration configuration,
        ILogger logger)
    {
        _logger = logger;
        _publicKey = configuration["MailJet:PublicKey"];
        _privateKey = configuration["MailJet:SecretKey"];
        _emailFrom = configuration["MailJet:Email"];
    }
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        if(string.IsNullOrEmpty(_publicKey) || string.IsNullOrEmpty(_privateKey))
        {
            throw new InvalidOperationException("Key is not configured");
        }
        await Execute(subject, htmlMessage, email);
    }
    private async Task Execute(string subject, string message, string toEmail)
    {
        MailjetClient client = new MailjetClient(_publicKey, _privateKey);
        MailjetRequest request = new MailjetRequest
            {
                Resource = Mailjet.Client.Resources.Send.Resource,
            }
            .Property(Mailjet.Client.Resources.Send.FromEmail, _emailFrom)
            .Property(Mailjet.Client.Resources.Send.FromName, "Eshop help")
            .Property(Mailjet.Client.Resources.Send.Subject, subject)
            .Property(Mailjet.Client.Resources.Send.TextPart, "Dear passenger, welcome to Mailjet! May the delivery force be with you!")
            .Property(Mailjet.Client.Resources.Send.HtmlPart, message)
            .Property(Mailjet.Client.Resources.Send.Recipients, new JArray {
                new JObject {
                    {"Email", toEmail}
                }
            });
        MailjetResponse response = await client.PostAsync(request);
                
        _logger.Information(response.IsSuccessStatusCode
            ? $"Email to {toEmail} queued successfully!"
            : $"Failure Email to {toEmail}");
    }
}