using Modular.Core.Entity;

namespace Modular.Core.System
{
    public partial class SystemCore
    {

        #region "  Constructors  "

        private SystemCore()
        {
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

    }
}