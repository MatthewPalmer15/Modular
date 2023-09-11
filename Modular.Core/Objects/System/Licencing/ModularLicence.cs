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
                DataExporterItem Item = DataExporter.GetScript("Modular_LicenceKey.sql");

                return AppConfig.GetValue("Modular_LicenceKey");
            }
        }

        #endregion

    }
}
