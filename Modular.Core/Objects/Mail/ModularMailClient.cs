using Modular.Core.Audit;
using Modular.Core.Utility;
using Modular.Core.Configuration;
using System.Net;
using System.Net.Mail;

namespace Modular.Core.Mail
{
    public static class MailClient
    {

        #region "  Structures  "

        internal struct Credentials
        {
            public static string Username => AppConfig.GetValue("SMTPClientUsername");

            public static string Password => AppConfig.GetValue("SMTPClientPassword");

        }

        #endregion

        #region "  Properties  "

        /// <summary>
        /// Get the SMTP Client Credentials from the App.Config file. If the value is not defined, it will return the default credentials for SMTP Client (empty string)
        /// </summary>
        public static string Host
        {
            get
            {
                return AppConfig.GetValue("SMTPClientHost");
            }
        }

        /// <summary>
        /// Get the SMTP Client Port from the App.Config file. If the value is invalid, it will return the default port for SMTP Client (587).
        /// </summary>
        public static int Port
        {
            get
            {
                string ConfigValue = AppConfig.GetValue("SMTPClientHost");
                try
                {
                    return int.Parse(ConfigValue);
                }
                catch
                {
                    return 587; // Default Port for SMTP Client
                }
            }
        }

        /// <summary>
        /// Get the SMTP Client Delivery Method from the App.Config file. If the value is not defined, it will return the default delivery method for SMTP Client (Network).
        /// </summary>
        public static SmtpDeliveryMethod DeliveryMethod
        {
            get
            {
                string ConfigValue = AppConfig.GetValue("SMTPClientDeliveryMethod");
                switch (ConfigValue.ToUpper())
                {
                    case "SPECIFIEDPICKUPDIRECTORY":
                        return SmtpDeliveryMethod.SpecifiedPickupDirectory;

                    case "PICKUPDIRECTORYFROMIIS":
                        return SmtpDeliveryMethod.PickupDirectoryFromIis;

                    case "NETWORK":
                        return SmtpDeliveryMethod.Network;

                    default:
                        return SmtpDeliveryMethod.Network;

                }
            }
        }

        /// <summary>
        /// Get the SSL Enabled from the App.Config file. If the value is not defined, it will return the default value for SMTP Client (false).
        /// </summary>
        public static bool EnableSSL
        {
            get
            {
                return AppConfig.GetValue("SMTPClientEnableSSL").ToUpper() == "TRUE";
            }
        }

        public static SmtpClient SMTPClient
        {
            get
            {
                return new SmtpClient()
                {
                    Host = MailClient.Host,
                    Port = MailClient.Port,
                    Credentials = new NetworkCredential(MailClient.Credentials.Username, MailClient.Credentials.Password),
                    DeliveryMethod = MailClient.DeliveryMethod,
                    EnableSsl = MailClient.EnableSSL
                };
            }
        }

        #endregion

        #region "  Public Methods  "

        public static void SendEmail(MailMessage Email)
        {
            try
            {
                SMTPClient.Send(Email);
            }
            catch
            {
                throw new ModularException(ExceptionType.SMTPClientEmailSendError, "There was an issue sending an email.");
            }
        }

        public static void SendAuditedEmail(MailMessage Email)
        {
            try
            {
                SMTPClient.Send(Email);
                AuditLog.Create(ObjectTypes.ObjectType.Email, Guid.Empty, $"Email was successfully sent from {Email.From.Address} | Subject: {Email.Subject}.");
            }
            catch
            {
                AuditLog.Create(ObjectTypes.ObjectType.Email, Guid.Empty, $"Email failed to be sent from {Email.From.Address} | Subject: {Email.Subject}.");
                throw new ModularException(ExceptionType.SMTPClientEmailSendError, "There was an issue sending an email.");
            }
        }

        #endregion

    }
}
