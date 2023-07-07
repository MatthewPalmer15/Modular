namespace Modular.Core
{
    public class ModularSystem
    {

        #region "  Constructors  "

        private ModularSystem()
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

        private static ModularSystem? _Context;

        #endregion

        #region "  Properties  "

        public static ModularSystem Context
        {
            get
            {
                return _Context != null ? _Context : new ModularSystem();
            }
        }

        public IIdentity Identity { get; } = new IIdentity();

        public IApplication Application { get; } = new IApplication();

        #endregion

        #region "  Nested Classes  "

        public class IIdentity
        {
            internal IIdentity()
            {
            }

            private Account? _CurrentUser;

            public Account CurrentUser
            {
                get
                {
                    if (_CurrentUser != null)
                    {
                        return _CurrentUser;
                    }
                    else
                    {
                        return Account.Create();
                    }
                }
            }
        }

        public class IApplication
        {

            internal IApplication()
            {
            }

            public string Name
            {
                get
                {
                    return AppConfig.GetValue("Application:Name").Trim();
                }
            }

            public ApplicationModeType Mode
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

            public bool Maintenance
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
