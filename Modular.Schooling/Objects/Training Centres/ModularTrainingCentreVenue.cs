using Modular.Core;

namespace Modular.Schooling
{
    [Serializable]
    public class Venue : ModularBase
    {

        #region "  Constructors  "

        public Venue()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Venue";

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

        private Guid _TrainingCentreID;

        private string _Name = string.Empty;

        private string _Description = string.Empty;

        private StatusType _Status;

        private string _AddressLine1 = string.Empty;

        private string _AddressLine2 = string.Empty;

        private string _AddressLine3 = string.Empty;

        private string _AddressCity = string.Empty;

        private string _AddressCounty = string.Empty;

        private Guid _AddressCountryID;

        private string _AddressPostcode = string.Empty;

        #endregion

        #region "  Properties  "

        public Guid TrainingCentreID
        {
            get
            {
                return _TrainingCentreID;
            }
            set
            {
                if (_TrainingCentreID != value)
                {
                    _TrainingCentreID = value;
                    OnPropertyChanged("TrainingCentreID");
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

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Creates a new instance with default values
        /// </summary>
        /// <returns>A new instance</returns>
        public static new Venue Create()
        {
            Venue obj = new Venue();
            obj.SetDefaultValues();
            return obj;
        }

        public static new Venue Load(Guid ID)
        {
            Venue obj = new Venue();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Name;
        }

        public override Venue Clone()
        {
            return Venue.Load(ID);
        }

        #endregion

    }
}