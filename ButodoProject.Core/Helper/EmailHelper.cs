using ButodoProject.Core.Service.Dto;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ButodoProject.Core.Helper
{
    public  static class EmailHelper
    {
        public static async Task SendEmailAsync(MailRequestDto mailRequest)
        {
            try
            {
                var senderName = "";
                var email = "";
                var password = "";
                var loginInfo = new NetworkCredential(email, password);
                var msg = new MailMessage();
                var smtpClient = new SmtpClient("smtp.yandex.com", 587);

                msg.To.Add(mailRequest.ToEmail);

                msg.From = new MailAddress(email, senderName);

                msg.Subject = mailRequest.Subject;
                msg.Body = mailRequest.Body;
                //if (!string.IsNullOrEmpty(replyTo))
                //    msg.ReplyToList.Add(new MailAddress(replyTo));

                msg.IsBodyHtml = true;

                //if (stream != null)
                //{
                //    var file = new Attachment(stream, fileName);
                //    msg.Attachments.Add(file);
                //}

                smtpClient.UseDefaultCredentials = false;
                //if (!string.IsNullOrEmpty(GetSettings("smtp-issecure")))
                smtpClient.EnableSsl = true;

                smtpClient.Credentials = loginInfo;
                await smtpClient.SendMailAsync(msg);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
