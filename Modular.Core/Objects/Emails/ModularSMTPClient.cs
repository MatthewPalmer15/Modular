using System.Net.Mail;

namespace Modular.Core
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

        #endregion



    }
}
