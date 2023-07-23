using Modular.Core;
using Modular.Core.Utility;

namespace Modular.Bookings
{
    [Serializable]
    public class Booking : ModularBase
    {

        #region "  Constructors  "

        public Booking()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Booking";

        #endregion

        #region "  Enums  "

        public enum BookingStatus
        {
            Pending = 0,
            Confirmed = 1,
            Cancelled = 2
        }

        #endregion

        #region "  Variables  "

        private string _Title = string.Empty;

        private Guid _ContactID;

        private Guid _InvoiceID;

        private Guid _VenueID;

        private DateTime _BookedDate;

        private DurationType _Duration;

        private StatusType _Status;

        private string _Notes = string.Empty;

        #endregion

        #region "  Properties  "

        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                if (_Title != value)
                {
                    _Title = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        public Guid ContactID
        {
            get
            {
                return _ContactID;
            }
            set
            {
                if (_ContactID != value)
                {
                    _ContactID = value;
                    OnPropertyChanged("ContactID");
                }
            }
        }

        public Guid InvoiceID
        {
            get
            {
                return _InvoiceID;
            }
            set
            {
                if (_InvoiceID != value)
                {
                    _InvoiceID = value;
                    OnPropertyChanged("InvoiceID");
                }
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

        public DateTime BookedDate
        {
            get
            {
                return _BookedDate;
            }
            set
            {
                if (_BookedDate != value)
                {
                    _BookedDate = value;
                    OnPropertyChanged("BookedDate");
                }
            }
        }

        public DurationType Duration
        {
            get
            {
                return _Duration;
            }
            set
            {
                if (_Duration != value)
                {
                    _Duration = value;
                    OnPropertyChanged("Duration");
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

        public string Notes
        {
            get
            {
                return _Notes;
            }
            set
            {
                if (_Notes != value)
                {
                    _Notes = value;
                    OnPropertyChanged("Notes");
                }
            }
        }

        #endregion

        #region "  Static Methods  "

        public static new Booking Create()
        {
            Booking obj = new Booking();
            obj.SetDefaultValues();
            return obj;
        }

        public static new Booking Load(Guid ID)
        {
            Booking obj = new Booking();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Title;
        }

        #endregion

    }
}