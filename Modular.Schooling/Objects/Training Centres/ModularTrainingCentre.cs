using Modular.Core;

namespace Modular.Schooling
{
    [Serializable]
    public class TrainingCentre : ModularBase
    {

        #region "  Constructors  "

        public TrainingCentre()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_TrainingCentre";

        #endregion

        #region "  Enums  "

        public enum StatusType
        {
            Unknown = 0,
            Active = 1,
            Inactive = 2,
            Deleted = 3
        }

        #endregion

        #region "  Variables  "

        private string _Name = string.Empty;

        private string _Description = string.Empty;

        private Guid _OwnerID;

        private StatusType _Status;

        private string _AddressLine1 = string.Empty;

        private string _AddressLine2 = string.Empty;

        private string _AddressLine3 = string.Empty;

        private string _AddressCity = string.Empty;

        private string _AddressCounty = string.Empty;

        private Guid _AddressCountryID;

        private string _AddressPostcode = string.Empty;

        private string _Email = string.Empty;

        private string _Phone = string.Empty;

        private string _FacebookLink = string.Empty;

        private string _InstagramLink = string.Empty;

        private string _TwitterLink = string.Empty;

        private string _LinkedInLink = string.Empty;

        private string _WebsiteLink = string.Empty;

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

        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        public Guid OwnerID
        {
            get
            {
                return _OwnerID;
            }
            set
            {
                if (_OwnerID != value)
                {
                    _OwnerID = value;
                    OnPropertyChanged("OwnerID");
                }
            }
        }

        public StatusType Status
        {
            get
            {
                return _Status;
            }
            set
            {
                if (_Status != value)
                {
                    _Status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        public string AddressLine1
        {
            get
            {
                return _AddressLine1;
            }
            set
            {
                if (_AddressLine1 != value)
                {
                    _AddressLine1 = value;
                    OnPropertyChanged("AddressLine1");
                }
            }
        }

        public string AddressLine2
        {
            get
            {
                return _AddressLine2;
            }
            set
            {
                if (_AddressLine2 != value)
                {
                    _AddressLine2 = value;
                    OnPropertyChanged("AddressLine2");
                }
            }
        }

        public string AddressLine3
        {
            get
            {
                return _AddressLine3;
            }
            set
            {
                if (_AddressLine3 != value)
                {
                    _AddressLine3 = value;
                    OnPropertyChanged("AddressLine3");
                }
            }
        }

        public string AddressCity
        {
            get
            {
                return _AddressCity;
            }
            set
            {
                if (_AddressCity != value)
                {
                    _AddressCity = value;
                    OnPropertyChanged("AddressCity");
                }
            }
        }

        public string AddressCounty
        {
            get
            {
                return _AddressCounty;
            }
            set
            {
                if (_AddressCounty != value)
                {
                    _AddressCounty = value;
                    OnPropertyChanged("AddressCounty");
                }
            }
        }

        public Guid AddressCountryID
        {
            get
            {
                return _AddressCountryID;
            }
            set
            {
                if (_AddressCountryID != value)
                {
                    _AddressCountryID = value;
                    OnPropertyChanged("AddressCountryID");
                }
            }
        }

        public string AddressPostcode
        {
            get
            {
                return _AddressPostcode;
            }
            set
            {
                if (_AddressPostcode != value)
                {
                    _AddressPostcode = value;
                    OnPropertyChanged("AddressPostcode");
                }
            }
        }

        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                if (_Email != value)
                {
                    _Email = value;
                    OnPropertyChanged("Email");
                }
            }
        }

        public string Phone
        {
            get
            {
                return _Phone;
            }
            set
            {
                if (_Phone != value)
                {
                    _Phone = value;
                    OnPropertyChanged("Phone");
                }
            }
        }

        public string FacebookLink
        {
            get
            {
                return _FacebookLink;
            }
            set
            {
                if (_FacebookLink != value)
                {
                    _FacebookLink = value;
                    OnPropertyChanged("FacebookLink");
                }
            }
        }

        public string InstagramLink
        {
            get
            {
                return _InstagramLink;
            }
            set
            {
                if (_InstagramLink != value)
                {
                    _InstagramLink = value;
                    OnPropertyChanged("InstagramLink");
                }
            }
        }

        public string TwitterLink
        {
            get
            {
                return _TwitterLink;
            }
            set
            {
                if (_TwitterLink != value)
                {
                    _TwitterLink = value;
                    OnPropertyChanged("TwitterLink");
                }
            }
        }

        public string LinkedInLink
        {
            get
            {
                return _LinkedInLink;
            }
            set
            {
                if (_LinkedInLink != value)
                {
                    _LinkedInLink = value;
                    OnPropertyChanged("LinkedInLink");
                }
            }
        }

        public string WebsiteLink
        {
            get
            {
                return _WebsiteLink;
            }
            set
            {
                if (_WebsiteLink != value)
                {
                    _WebsiteLink = value;
                    OnPropertyChanged("WebsiteLink");
                }
            }
        }

        public bool IsAccredited
        {
            get
            {
                return false;
                //return Accreditation.Accreditation.LoadAll(ID).Where(x => x.Status == Accreditation.Accreditation.StatusType.Verified).Count() > 0;
            }
        }

        public Accreditation.Accreditation LatestAccreditation
        {
            get
            {
                return new Accreditation.Accreditation();
            }
        }

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Creates a new instance with default values
        /// </summary>
        /// <returns>A new instance</returns>
        public static new TrainingCentre Create()
        {
            TrainingCentre obj = new TrainingCentre();
            obj.SetDefaultValues();
            return obj;
        }

        public static new TrainingCentre Load(Guid ID)
        {
            TrainingCentre obj = new TrainingCentre();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Name;
        }

        public override TrainingCentre Clone()
        {
            return TrainingCentre.Load(ID);
        }

        #endregion

    }
}