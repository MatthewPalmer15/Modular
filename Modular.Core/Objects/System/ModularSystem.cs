namespace Modular.Core
{
    public class ModularSystem
    {

        #region "  Constructors  "

        private ModularSystem()
        {
        }

        #endregion

        #region "  Variables  "

        private static ModularSystem _Context;

        private Account _CurrentIdentity;

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
                if (_CurrentIdentity != null)
                {
                    return _CurrentIdentity;
                }
                else
                {
                    return Account.Create();
                }
            }
        }

        // TODO: Get organsation policy from user

        #endregion

    }
}