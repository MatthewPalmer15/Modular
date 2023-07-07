namespace Modular.Core
{

    public class AccountManager
    {

        #region "  Constructors  "

        private AccountManager()
        {
        }

        #endregion

        #region "  Variables  "

        private static AccountManager _Instance;

        private Account _CurrentUser;

        #endregion

        #region "  Properties  "

        public static AccountManager Instance
        {
            get
            {
                return _Instance ?? new AccountManager();
            }
        }

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

        #endregion

    }

}
