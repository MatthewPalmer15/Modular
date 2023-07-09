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

        private static readonly ModularSystem _Context = new ModularSystem();

        private Account _Identity;

        #endregion

        #region "  Properties  "

        public static ModularSystem Context
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

    }
}