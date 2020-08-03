using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SimpleFramework.Utils.Email
{
    public class EmailHelper
    {
       
        //todo: in progress :)


        public bool SendMail(EmailInfo emailInfo)
        {
            var message = new MailMessage
            {
                IsBodyHtml = emailInfo.UseHtml,
                From = new MailAddress(emailInfo.From, emailInfo.FromName),
            };
            
            foreach (var repicient in emailInfo.Recipients)
            {
                message.To.Add(new MailAddress(repicient));
            }

            foreach (var recipient in emailInfo.RecipientsCc)
            {
                message.To.Add(new MailAddress(recipient));
            }

            message.Subject = emailInfo.Subject;
            message.Body = emailInfo.Body;
            
            using (var client = new SmtpClient(emailInfo.Host, emailInfo.Port))
            {
                client.Credentials = new NetworkCredential(emailInfo.CredentialUser, emailInfo.CredentialPassword);
                client.EnableSsl = emailInfo.UseSsl;
                try
                {
                    client.Send(message);
                    return true;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    


        public async Task<bool> SendMailAsync()
        {
            return await Task.FromResult<bool>(true);
        }

        // Read Email 


        // ETC.

        private void Validation(EmailInfo emailInfo)
        {
            var errors = new List<Exception>();

            if(!new EmailAddressAttribute().IsValid(emailInfo.From))
            {

            }


            throw new AggregateException(errors);
        }

    }
}
