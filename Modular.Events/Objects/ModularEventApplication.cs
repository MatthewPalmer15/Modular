using Modular.Core;
using Modular.Core.Invoicing;

namespace Modular.Events
{
    [Serializable]
    public class EventApplication : ModularBase
    {

        #region "  Constructors  "

        public EventApplication()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Event_Application";

        #endregion

        #region "  Enums  "

        public enum ApplicationStatusType
        {
            Unknown = 0,
            Pending = 1,
            Approved = 2,
            Rejected = 3,
            Cancelled = 4
        }

        #endregion

        #region "  Variables  "

        private Guid _EventID;

        private Guid _ContactID;

        private Guid _InvoiceID;

        private List<EventApplicationItem> _EventItems;

        private decimal _PriceExcVAT;

        private decimal _PriceVAT;

        private ApplicationStatusType Status;

        #endregion

        #region "  Properties  "

        public Guid EventID
        {
            get
            {
                return _EventID;
            }
            set
            {
                if (_EventID != value)
                {
                    _EventID = value;
                    OnPropertyChanged("EventID");
                }
            }
        }

        public Event Event
        {
            get
            {
                return Event.Load(EventID);
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

        public Core.Entity.Contact Contact
        {
            get
            {
                return Core.Entity.Contact.Load(ContactID);
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

        public List<EventApplicationItem> EventItems
        {
            get
            {
                LoadEventItems();
                return _EventItems;

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

        public decimal TotalPriceExcVAT
        {
            get
            {
                decimal total = 0;
                foreach (EventApplicationItem Item in EventItems)
                {
                    total += Item.PriceExcVAT;
                }
                return total;
            }
        }

        public decimal TotalPriceVAT
        {
            get
            {
                decimal total = 0;
                foreach (EventApplicationItem Item in EventItems)
                {
                    total += Item.PriceVAT;
                }
                return total;
            }
        }

        public decimal TotalPriceIncVAT
        {
            get
            {
                return TotalPriceExcVAT + TotalPriceVAT;
            }
        }

        public bool IsPaid
        {
            get
            {
                return Invoice.Load(InvoiceID).IsPaid;
            }
        }

        public ApplicationStatusType ApplicationStatus
        {
            get
            {
                return Status;
            }
            set
            {
                if (Status != value)
                {
                    Status = value;
                    OnPropertyChanged("ApplicationStatus");
                }
            }
        }

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Creates a new instance with default values
        /// </summary>
        /// <returns>A new instance</returns>
        public static new EventApplication Create()
        {
            EventApplication obj = new EventApplication();
            obj.SetDefaultValues();
            return obj;
        }

        public static new EventApplication Load(Guid ID)
        {
            EventApplication obj = new EventApplication();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        private void LoadEventItems()
        {
            // TODO: Load event items
            _EventItems = new List<EventApplicationItem>();
        }

        public override string ToString()
        {
            return $"{Event.Name} ({Contact.FullName})";
        }

        public override EventAttendee Clone()
        {
            return EventAttendee.Load(ID);
        }

        #endregion

    }
}