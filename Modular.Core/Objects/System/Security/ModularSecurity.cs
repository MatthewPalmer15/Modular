namespace Modular.Core
{
    public static class Security
    {

        #region "  Enums  "

        #region "  Base  "

        public enum Contact
        {
            View = 100000,
            Fetch = 100001,
            Insert = 100002,
            Update = 100003,
            Delete = 100004,
        }

        public enum ContactAccount
        {
            View = 100005,
            Fetch = 100006,
            Insert = 100007,
            Update = 100008,
            Delete = 100009,
        }

        public enum Log
        {
            View = 100015,
            Fetch = 100016,
            Insert = 100017,
            Update = 100018,
            Delete = 100019,
        }

        public enum Permission
        {
            View = 100020,
            Fetch = 100021,
            Insert = 100022,
            Update = 100023,
            Delete = 100024,
        }

        public enum Email
        {
            Send = 200000,
        }

        public enum Printer
        {
            Print = 200001,
        }

        #endregion

        #endregion

        public static bool CheckSecurity(Enum SecurityPermission)
        {
            Account objAccount = ModularUtils.GetCurrentUserObject();
            if (objAccount != null)
            {
                Role objRole = Role.Load(objAccount.RoleID);
                return objRole.Permissions.Where(RolePermission => RolePermission.Permission == SecurityPermission).First() != null;
            }
            else
            {
                return false;
            }
        }

    }
}