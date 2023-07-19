using Modular.Core.Configuration;

namespace Modular.Core.System.Licencing
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

        #endregion

    }
}
