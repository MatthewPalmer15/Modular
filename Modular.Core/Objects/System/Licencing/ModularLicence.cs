using Modular.Core.Configuration;
using Modular.Core.Exporter;

namespace Modular.Core.Licencing
{
    public static class Licence
    {

        #region "  Properties  "

        public static string LicenceKey
        {
            get
            {
                return AppConfig.GetValue("Modular_LicenceKey");
            }
        }

        public static bool IsValid
        {
            get
            {
                return false;
            }
        }

        #endregion

    }
}
