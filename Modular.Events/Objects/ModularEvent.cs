using Modular.Core;

namespace Modular.Events
{
    [Serializable]
    public class Event : ModularBase
    {

        #region "  Constructors  "

        public Event()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Event";

        #endregion

        #region "  Variables  "

        private string _Name = string.Empty;

        private string _Description = string.Empty;

        private DateTime _StartDate;

        private DateTime _EndDate;

        private string _AddressLine1 = string.Empty;

        private string _AddressLine2 = string.Empty;

        private string _AddressLine3 = string.Empty;

        private string _AddressCity = string.Empty;

        private string _AddressCounty = string.Empty;

        private Guid _AddressCountryID;

        private string _AddressPostcode = string.Empty;

        private bool _IsPublic;

        private bool _IsRemote;

        private int _MaxAttendees;

        private decimal _PriceExcVAT;

        private decimal _PriceVAT;

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

        public DateTime StartDate
        {
            get
            {
                return _StartDate;
            }
            set
            {
                if (_StartDate != value)
                {
                    _StartDate = value;
                    OnPropertyChanged("StartDate");
                }
            }
        }

        public DateTime EndDate
        {
            get
            {
                return _EndDate;
            }
            set
            {
                if (_EndDate != value)
                {
                    _EndDate = value;
                    OnPropertyChanged("EndDate");
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

        public bool IsPublic
        {
            get
            {
                return _IsPublic;
            }
            set
            {
                if (_IsPublic != value)
                {
                    _IsPublic = value;
                    OnPropertyChanged("IsPublic");
                }
            }
        }

        public bool IsRemote
        {
            get
            {
                return _IsRemote;
            }
            set
            {
                if (_IsRemote != value)
                {
                    _IsRemote = value;
                    OnPropertyChanged("IsRemote");
                }
            }
        }

        public int MaxAttendees
        {
            get
            {
                return _MaxAttendees;
            }
            set
            {
                if (_MaxAttendees != value)
                {
                    _MaxAttendees = value;
                    OnPropertyChanged("MaxAttendees");
                }
            }
        }

        public decimal PriceExcVAT
        {
            get
            {
                return _PriceExcVAT;
            }
            set
            {
                if (_PriceExcVAT != value)
                {
                    _PriceExcVAT = value;
                    OnPropertyChanged("PriceExcVAT");
                }
            }
        }

        public decimal PriceVAT
        {
            get
            {
                return _PriceVAT;
            }
            set
            {
                if (_PriceVAT != value)
                {
                    _PriceVAT = value;
                    OnPropertyChanged("PriceVAT");
                }
            }
        }

        public decimal PriceIncVAT
        {
            get
            {
                return _PriceExcVAT + _PriceVAT;
            }
        }

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Creates a new instance with default values
        /// </summary>
        /// <returns>A new instance</returns>
        public static new Event Create()
        {
            Event obj = new Event();
            obj.SetDefaultValues();
            return obj;
        }

        public static new Event Load(Guid ID)
        {
            Event obj = new Event();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Name;
        }

        public override Event Clone()
        {
            return Event.Load(ID);
        }

        #endregion

    }
}