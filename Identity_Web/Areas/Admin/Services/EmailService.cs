﻿using System.Net;
using System.Net.Mail;
using System.Text;

namespace Identity_Web.Areas.Admin.Services
{
    public class EmailService
    {
        public Task Excute(string UserEmail,string Body,string Subject)
        {
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 1000000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("ramin.mohamadyan20@gmail.com", "bdcc imwx wcck hejy");
            MailMessage message = new MailMessage("ramin.mohamadyan20@gmail.com",UserEmail,Subject,Body);
            message.IsBodyHtml = true;
            message.BodyEncoding=UTF8Encoding.UTF8;
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
            client.Send(message);

            return Task.CompletedTask;
        }
    }
}
