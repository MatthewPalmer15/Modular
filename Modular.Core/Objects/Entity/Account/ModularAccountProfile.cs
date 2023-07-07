namespace Modular.Core
{
    public class Profile : ModularBase
    {

        #region "  Constructors  "

        public Profile()
        {
        }

        #endregion

        #region "  Variables  "

        private Guid _AccountID;

        private string _Name = string.Empty;

        private string _Bio = string.Empty;

        private string _Colour = string.Empty;

        private byte[] _ProfilePicture;

        private byte[] _BackgroundPicture;

        #endregion

        #region "  Properties  "

        public Guid AccountID
        {
            get
            {
                return _AccountID;
            }
            set
            {
                if (_AccountID != value)
                {
                    _AccountID = value;
                    OnPropertyChanged("AccountID");
                }
            }
        }

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

        public string Bio
        {
            get
            {
                return _Bio;
            }
            set
            {
                if (_Bio != value)
                {
                    _Bio = value;
                    OnPropertyChanged("Bio");
                }
            }
        }

        public string Colour
        {
            get
            {
                return _Colour;
            }
            set
            {
                if (_Colour != value)
                {
                    _Colour = value;
                    OnPropertyChanged("Colour");
                }
            }
        }

        public byte[] ProfilePicture
        {
            get
            {
                return _ProfilePicture;
            }
            set
            {
                if (_ProfilePicture != value)
                {
                    _ProfilePicture = value;
                    OnPropertyChanged("ProfilePicture");
                }
            }
        }

        public byte[] BackgroundPicture
        {
            get
            {
                return _BackgroundPicture;
            }
            set
            {
                if (_BackgroundPicture != value)
                {
                    _BackgroundPicture = value;
                    OnPropertyChanged("BackgroundPicture");
                }
            }
        }

        #endregion

    }
}
