namespace Modular.Core
{

    //[Serializable]
    //public class ModularPrincipal : IPrincipal
    //{
    //    #region "  Constructors  "
    //
    //    private ModularPrincipal(string Username, string Password)
    //    {
    //        AppDomain CurrentDomain = Thread.GetDomain();
    //        CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.UnauthenticatedPrincipal);
    //
    //        IPrincipal? OldPrincipal = Thread.CurrentPrincipal;
    //        Thread.CurrentPrincipal = this;
    //
    //        try
    //        {
    //            if (OldPrincipal is not ModularPrincipal)
    //            {
    //                CurrentDomain.SetThreadPrincipal(this);
    //            }
    //        }
    //        catch
    //        {
    //            // Failed but its ok.
    //        }
    //
    //        if (string.IsNullOrEmpty(Username))
    //        {
    //            _Account = Account.Create();
    //            _Account.Username = "Guest";
    //        }
    //        else
    //        {
    //            _Account = Account.Load(Username, Account.LoginMethodType.Username);
    //        }
    //    }
    //
    //    #endregion
    //
    //    #region "  Variables  "
    //
    //  private Account _Account;
    //
    //  #endregion
    //
    //    #region "  Properties  "
    //
    //  public IIdentity Identity
    //  {
    //      get
    //      {
    //          return _Account;
    //      }
    //  }
    //
    //  #endregion
    //
    //    public void InitialiseBlankUser()
    //    {
    //        ModularPrincipal Principal = new ModularPrincipal(string.Empty, string.Empty);
    //    }
    //
    //    public void Login(string Username, string Password)
    //    {
    //        ModularPrincipal Principal = new ModularPrincipal(Username, Password);
    //    }
    //
    //    public void Logout()
    //    {
    //        try
    //        {
    //            if (_Account != null)
    //            {
    //                if (_Account.IsModified)
    //                {
    //                    _Account.Save();
    //                }
    //                _Account = Account.Create();
    //            }
    //        }
    //        catch
    //        {
    //            // LOGIC
    //        }
    //    }
    //
    //    bool IPrincipal.IsInRole(string RoleName)
    //    {
    //        throw new ModularException(ExceptionType.NotImplemented, "This feature is still under development.");
    //    }
    //}
}

