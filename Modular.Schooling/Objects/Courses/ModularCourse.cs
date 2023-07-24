using Modular.Core;
using Modular.Core.Entity;

namespace Modular.Schooling
{
    [Serializable]
    public class Course : ModularBase
    {

        #region "  Constructors  "

        public Course()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Course";

        #endregion

        #region "  Enums  "

        public enum CourseType
        {
            Unknown = 0,
            OnSite = 1,
            Online = 2,
            Both = 3
        }

        #endregion

        #region "  Variables  "

        private DateTime _ReleaseDate;

        private string _CourseNumber = string.Empty;

        private string _Name = string.Empty;

        private string _Description = string.Empty;

        private DateTime _StartDate;

        private DateTime _EndDate;

        private Guid _TrainingCentreID;

        private Guid _VenueID;

        private CourseType _CourseType;

        private Guid _CategoryID;

        private decimal _PriceExcVAT;

        private decimal _PriceVAT;

        private int _Capacity;

        private int _UserCount;

        private List<CourseModule> _Modules = new List<CourseModule>();

        #endregion

        #region "  Properties  "

        public DateTime ReleaseDate
        {
            get
            {
                return _ReleaseDate;
            }
            set
            {
                if (_ReleaseDate != value)
                {
                    _ReleaseDate = value;
                    OnPropertyChanged("ReleaseDate");
                }
            }
        }

        public string CourseNumber
        {
            get
            {
                return _CourseNumber;
            }
            set
            {
                if (_CourseNumber != value)
                {
                    _CourseNumber = value;
                    OnPropertyChanged("CourseNumber");
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

        public Guid TrainingCentreID
        {
            get
            {
                return _TrainingCentreID;
            }
            private set
            {
                if (_TrainingCentreID != value)
                {
                    _TrainingCentreID = value;
                    OnPropertyChanged("TrainingCentreID");
                }
            }
        }

        public TrainingCentre TrainingCentre
        {
            get
            {
                return TrainingCentre.Load(TrainingCentreID);
            }
        }

        public Guid VenueID
        {
            get
            {
                return _VenueID;
            }
            set
            {
                if (_VenueID != value)
                {
                    _VenueID = value;
                    OnPropertyChanged("VenueID");
                }
            }
        }

        public Venue Venue
        {
            get
            {
                return Venue.Load(VenueID);
            }
        }

        public CourseType Type
        {
            get
            {
                return _CourseType;
            }
            set
            {
                if (_CourseType != value)
                {
                    _CourseType = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        public Guid CategoryID
        {
            get
            {
                return _CategoryID;
            }
            set
            {
                if (_CategoryID != value)
                {
                    _CategoryID = value;
                    OnPropertyChanged("CategoryID");
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
                return PriceExcVAT + PriceVAT;
            }
        }

        public int Capacity
        {
            get
            {
                return _Capacity;
            }
            set
            {
                if (_Capacity != value)
                {
                    _Capacity = value;
                    OnPropertyChanged("Capacity");
                }
            }
        }

        public List<CourseModule> Modules
        {
            get
            {
                LoadCourseModules();
                return _Modules;
            }
        }

        public int UserCount
        {
            get
            {
                GetUserCount();
                return _UserCount;
            }
        }

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Creates a new instance with default values
        /// </summary>
        /// <returns>A new instance</returns>
        public static new Course Create()
        {
            Course obj = new Course();
            obj.SetDefaultValues();
            return obj;
        }

        public static new Course Load(Guid ID)
        {
            Course obj = new Course();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        private void GetUserCount()
        {
            // _UserCount = CourseUser.CountByCourseID(ID).ToList().Count;
            _UserCount = 0;
        }

        private void LoadCourseModules()
        {
            _Modules = new List<CourseModule>();
            // return CourseModule.LoadByCourseID(ID);
        }

        public override string ToString()
        {
            return $"{Name}";
        }

        public override Course Clone()
        {
            return Course.Load(ID);
        }

        #endregion

    }
}