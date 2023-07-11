using System.Net;
using System.Net.Mail;

namespace Modular.Core
{
    public static class Email
    {

        #region "  Methods  "

        public static void SendEmail(MailMessage Email, bool Audit = false)
        {
            try
            {
                SmtpClient SMTPClient = new SmtpClient()
                {
                    Host = MailClient.Host,
                    Port = MailClient.Port,
                    Credentials = new NetworkCredential(MailClient.Credentials.Username, MailClient.Credentials.Password),
                    DeliveryMethod = MailClient.DeliveryMethod,
                    EnableSsl = MailClient.EnableSSL
                };

                SMTPClient.Send(Email);

                if (Audit)
                {
                    //  TODO: Create Audit Log.
                }

            }
            catch
            {
                throw new ModularException(ExceptionType.SMTPClientEmailSendError, "There was an issue sending an email.");
            }
        }

        #endregion

    }
}
