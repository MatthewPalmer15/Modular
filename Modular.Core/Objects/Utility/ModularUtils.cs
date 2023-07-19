using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Modular.Core.Utility
{
    public static class ModularUtils
    {

        #region "  Remove Illegal Characters Methods  "

        public static string RemoveIllegalCharacters(string Text)
        {
            return RemoveIllegalCharacters(Text, false);
        }

        private static string RemoveIllegalCharacters(string Text, bool AllowUnderscores)
        {
            return RemoveIllegalCharacters(Text, AllowUnderscores, false);
        }

        public static string RemoveIllegalCharacters(string Text, bool AllowUnderscores, bool AllowDashes)
        {
            // Define the pattern of illegal characters using a regular expression
            string pattern = "[\\/:*?\"<>|]";

            // Remove illegal characters using regular expression substitution
            string result = Regex.Replace(Text, pattern, "");

            if (!AllowUnderscores) result = result.Replace("_", "");
            if (!AllowDashes) result = result.Replace("-", "");

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

        #region "  Data Types Conversion Methods  "

        public static Stream ConvertFileToStream(string FilePath)
        {
            return new FileStream(FilePath, FileMode.Open, FileAccess.Read);
        }

        public static void ConvertStreamToFile(Stream FileStream, string OutputPath)
        {
            using (FileStream NewFileStream = new FileStream(OutputPath, FileMode.Create, FileAccess.Write))
            {
                FileStream.CopyTo(NewFileStream);
            }
        }

        public static byte[] ConvertFileToBytes(string FilePath)
        {
            return File.ReadAllBytes(FilePath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FileBytes"></param>
        /// <param name="Filename"></param>
        /// <param name="FileExtension">File Format. Do not add '.' to this parameter.</param>
        /// <param name="FilePath"></param>
        public static void ConvertBytesToFile(byte[] FileBytes, string Filename,  string FileExtension,  string FilePath = "")
        {
            string OutputDestination;
            
            if (FileExtension.StartsWith('.')) FileExtension = FileExtension.Replace(".", "");
            
            
            if (string.IsNullOrEmpty(FilePath))
            {
                OutputDestination = $"{Environment.SpecialFolder.UserProfile}\\Downloads\\{Filename}.{FileExtension}";
            }
            else
            {
                if (FilePath.Trim().EndsWith('\\') || FilePath.Trim().EndsWith('/'))
                {
                    OutputDestination = $"{FilePath.Trim()}{Filename}.{FileExtension}";
                }
                else
                {
                    OutputDestination = $"{FilePath.Trim()}\\{Filename}.{FileExtension}";
                }
            }

            using (FileStream NewFileStream = new FileStream(OutputDestination, FileMode.Create, FileAccess.Write))
            {
                NewFileStream.Write(FileBytes, 0, FileBytes.Length);
            }
        }

        public static Stream ConvertBytesToStream(byte[] File)
        {             
            return new MemoryStream(File);
        }

        public static byte[] ConvertStreamToBytes(Stream FileStream)
        {
            using (MemoryStream Stream = new MemoryStream())
            {
                FileStream.CopyTo(Stream);
                return Stream.ToArray();
            }
        }

        #endregion

        public static bool PropertyHasAttribute(PropertyInfo Property, Type AttributeType)
        {
            return Property.GetCustomAttributes(AttributeType, false).Length > 0;
        }


        public static string GetDeviceSummary()
        {
            IDeviceInfo CurrentIdentityDevice = DeviceInfo.Current;
            return  $"Device Name: {CurrentIdentityDevice.Name}" + Environment.NewLine +
                    $"Device Type: {CurrentIdentityDevice.Manufacturer} {CurrentIdentityDevice.Model}" + Environment.NewLine +
                    $"Device OS: {CurrentIdentityDevice.Platform} v{CurrentIdentityDevice.VersionString}";
        }
    }
}