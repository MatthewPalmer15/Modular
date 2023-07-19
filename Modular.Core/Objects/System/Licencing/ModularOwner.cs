namespace Modular.Core.Owner
{
    public class Owner : ModularBase
    {

        #region "  Constructors  "

        public Owner()
        {
        }

        #endregion

        #region "  Variables  "

        private string _Name = string.Empty;

        //private Image _Logo;

        #endregion

        #region "  Properties  "

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        //public Image Logo
        //{
        //    get
        //    {
        //        return _Logo;
        //    }
        //    set
        //    {
        //        if (_Logo != value)
        //        {
        //            _Logo = value;
        //            OnPropertyChanged("Logo");
        //        }
        //    }
        //}

        #endregion

        #region "  Methods  "

        #endregion

    }
}
