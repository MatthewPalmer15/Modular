using Modular.Core.Entity;
using Modular.Core.Databases;
using Modular.Core.Configuration;

namespace Modular.Core
{
    public class SystemCore
    {

        #region "  Constructors  "

        private SystemCore()
        {
        }

        #endregion

        #region "  Enums  "

        public enum ApplicationModeType
        {
            Unknown,
            Website,
            Desktop,
            Mobile
        }

        #endregion

        #region "  Variables  "

        private static readonly SystemCore _Context = new SystemCore();

        private Account _Identity;

        #endregion

        #region "  Properties  "

        public static SystemCore Context
        {
            get
            {
                return _Context;
            }
        }

        public Account Identity
        {
            get
            {
                if (_Identity != null)
                {
                    return _Identity;
                }
                else
                {
                    return Account.Create();
                }
            }
            set
            {
                _Identity = value;
            }
        }

        #endregion

        #region "  Public Methods  "

        public static void Initialize()
        {
            // CREATE DATABASE
            if (Database.CheckDatabaseConnection())
            {

            }
            else
            {
                throw new ModularException(ExceptionType.DatabaseConnectionError, "Unable to connect to database.");
            }
        }

        #endregion

        #region "  Application  "

        public static class Application
        {

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

        #endregion

    }
}