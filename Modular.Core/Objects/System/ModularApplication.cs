using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Core
{
    public static class Application
    {

        #region "  Enums  "

        public enum ApplicationModeType
        {
            Unknown,
            Website,
            Desktop,
            Mobile
        }

        #endregion

        public static string Name
        {
            get
            {
                return AppConfig.GetValue("Application:Name").Trim();
            }
        }

        public static ApplicationModeType Mode
        {
            get
            {
                return AppConfig.GetValue("Application:Mode").ToUpper() switch
                {
                    "WEBSITE" => ApplicationModeType.Website,
                    "DESKTOP" => ApplicationModeType.Desktop,
                    "MOBILE" => ApplicationModeType.Mobile,
                    _ => ApplicationModeType.Unknown,
                };
            }
        }

        public static bool Maintenance
        {
            get
            {
                return AppConfig.GetValue("Application:Maintenance").ToUpper() == "TRUE";
            }
        }

    }
}
