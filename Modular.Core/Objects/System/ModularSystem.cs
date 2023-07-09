namespace Modular.Core
{
    public partial class ModularSystem
    {

        #region "  Constructors  "

        private ModularSystem()
        {
        }

        #endregion

        #region "  Variables  "

        private static ModularSystem _Context;

        private Account _Identity;

        #endregion

        #region "  Properties  "

        public static ModularSystem Context
        {
            get
            {
                return _Context ?? new ModularSystem();
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
        }

        #endregion

    }
}