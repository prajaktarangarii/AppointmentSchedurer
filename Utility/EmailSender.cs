using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Newtonsoft.Json.Linq;
using Mailjet.Client;
using Mailjet.Client.Resources;
namespace AppointmentScheduler.Utility
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailjetClient client = new MailjetClient("571302511d59912a9fb0fdd02a0d15cb\r\n\r\n", "9e6bad89730a31ddfe86cceea8efcbb7")
            {
            };
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
               .Property(Send.FromEmail, "prajaktarangari03@gmail.com")
               .Property(Send.FromName, "Appointment Scheduler")
               .Property(Send.Subject, subject)
               .Property(Send.HtmlPart, htmlMessage)
               .Property(Send.Recipients, new JArray {
                new JObject {
                 {"Email", email}
                 }
                   });
            MailjetResponse response = await client.PostAsync(request);
            }
    }
}
