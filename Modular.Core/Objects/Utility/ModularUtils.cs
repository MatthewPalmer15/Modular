using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Modular.Core
{
    public static class ModularUtils
    {

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