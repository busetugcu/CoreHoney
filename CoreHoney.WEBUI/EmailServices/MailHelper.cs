using System.Net.Mail;
using System.Net;

namespace CoreHoney.WEBUI.EmailServices
{
    public class MailHelper
    {
        public static bool SendMail(string body, string to, string subject, bool isHtml = true)
        {
            return SendMail(body, new List<string> { to }, subject, isHtml);
        }

        public static bool SendMail(string body, List<string> to, string subject, bool isHtml = true)
        {
            bool result = false;

            try
            {
                var message = new MailMessage();
                message.From = new MailAddress("buse.matematik@hotmail.com");

                to.ForEach(x =>
                {
                    message.To.Add(new MailAddress(x));
                });

                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = isHtml;

                using (var smtp = new SmtpClient("smtp-mail.outlook.com", 587))
                {
                    smtp.EnableSsl = true;
                    smtp.Credentials =
                        new NetworkCredential("buse.matematik@hotmail.com", "Matematik.1907");
                    smtp.Send(message);
                    result = true;
                }
            }
            catch (Exception)
            {
                // LOG
            }

            return result;
        }

        


    }
}
