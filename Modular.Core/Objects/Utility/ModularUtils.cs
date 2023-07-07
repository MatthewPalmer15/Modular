using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Modular.Core
{
    public static class ModularUtils
    {

        #region "  Contact/Account Methods  "

        public static Guid GetCurrentUserID()
        {
            if (Thread.CurrentPrincipal != null)
            {
                Account? objLoggedInUser = Thread.CurrentPrincipal.Identity as Account;

                if (objLoggedInUser != null)
                {
                    return objLoggedInUser.ID;
                }
                else
                {
                    return Guid.Empty;
                }
            }
            else
            {
                throw new ModularException(ExceptionType.InvalidOperation, "Principal has not been setup.");
            }
        }

        public static Account GetCurrentUserObject()
        {
            if (Thread.CurrentPrincipal != null)
            {
                Account? objLoggedInUser = Thread.CurrentPrincipal.Identity as Account;

                if (objLoggedInUser != null)
                {
                    return objLoggedInUser;
                }
                else
                {
                    return Account.Create();
                }
            }
            else
            {
                throw new ModularException(ExceptionType.InvalidOperation, "Principal has not been setup.");
            }
        }

        #endregion

        #region "  "

        public static string GetOSVersion()
        {
            return Environment.OSVersion.ToString();
        }

        public static string GetMachineName()
        {
            return Environment.MachineName;
        }

        public static string GetLocalIPAddress()
        {
            var Host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var IP in Host.AddressList)
            {
                if (IP.AddressFamily == AddressFamily.InterNetwork)
                {
                    return IP.ToString();
                }
            }

            throw new ModularException(ExceptionType.IPAddressError, "No network adapters with an IPv4 address in the system!");

        }

        public static async Task<string> GetExternalIPAddress()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(AppConfig.GetValue("GetExternalIPWebsite"));  // "https://api.ipify.org"
                    response.EnsureSuccessStatusCode();
                    string ipAddress = await response.Content.ReadAsStringAsync();
                    return ipAddress;
                }
            }
            catch
            {
                // Handle any exceptions that occur during the request
                return string.Empty;
            }
        }

        #endregion

        #region "  Remove Illegal Characters  "

        public static string RemoveIllegalCharacters(string Text)
        {
            return RemoveIllegalCharacters(Text, false);
        }

        public static string RemoveIllegalCharacters(string Text, bool AllowUnderscores)
        {
            // Define the pattern of illegal characters using a regular expression
            string pattern = "[\\/:*?\"<>|]";

            // Remove illegal characters using regular expression substitution
            string result = Regex.Replace(Text, pattern, "");

            if (!AllowUnderscores) result = result.Replace("_", "");

            return result;
        }

        public static string RemoveIllegalCharactersXML(string Text)
        {
            StringBuilder StringBuilder = new StringBuilder(Text);

            StringBuilder.Replace("&", "&amp;");
            StringBuilder.Replace(">", "&gt;");
            StringBuilder.Replace("<", "&lt;");
            StringBuilder.Replace("'", "&apos;");
            StringBuilder.Replace("\"", "&quot;");

            return StringBuilder.ToString();
        }

        #endregion

        public static bool PropertyHasAttribute(PropertyInfo Property, Type AttributeType)
        {
            return Property.GetCustomAttributes(AttributeType, false).Length > 0;
        }

    }
}