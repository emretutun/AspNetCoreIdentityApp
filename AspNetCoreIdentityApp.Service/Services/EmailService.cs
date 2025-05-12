﻿using AspNetCoreIdentityApp.Core.OptionsModel;
using AspNetCoreIdentityApp.Web.Services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace AspNetCoreIdentityApp.Service.Services
{
    public class EmailService:IEmailService
    {

        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }

        public async Task SendResetPasswordEmail(string resetPasswordEmailLink,string ToEmail)
        {
            var smptClient = new SmtpClient();

            smptClient.Host =_emailSettings.Host;
            smptClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smptClient.UseDefaultCredentials = false;
            smptClient.Port = 587;
            smptClient.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password);
            smptClient.EnableSsl = true;

            var mailMessage= new MailMessage();


            mailMessage.From = new MailAddress(_emailSettings.Email);
            mailMessage.To.Add(ToEmail);

            mailMessage.Subject = "Localhost | Şifre sıfırılama linki";
            mailMessage.Body = @$"
            
              <h4>Şifrenizi yenilemek için aşağıdaki linke tıklayınız.</h4> 
              <p><a href='{resetPasswordEmailLink}'>şifre yenileme link</a></P>";

            mailMessage.IsBodyHtml = true;

            await smptClient.SendMailAsync(mailMessage);

        }


    }
}
