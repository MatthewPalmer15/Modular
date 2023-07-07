using System.Configuration;

namespace Modular.Core
{
    public static class AppConfig
    {

        public static string GetValue(string Key)
        {
            return ConfigurationManager.AppSettings[Key] ?? string.Empty;
        }

    }
}
