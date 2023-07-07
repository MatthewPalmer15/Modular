using System.Configuration;

namespace Modular.Core
{
    public static class AppConfig
    {

        public static string GetValue(string Key)
        {
            string? ConfigValue = ConfigurationManager.AppSettings[Key];
            if (ConfigValue != null)
            {
                return ConfigValue;
            }
            else
            {
                return string.Empty;
            }
        }

    }
}
