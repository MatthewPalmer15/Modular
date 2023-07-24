using Modular.Core;

namespace Modular.Events
{
    public class EventAttendee : ModularBase
    {

        #region "  Constructors  "

        public EventAttendee()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Event_Attendee";

        #endregion

        #region "  Enums  "

        public enum StatusType
        {
            Unknown = 0,
            Unconfirmed = 1,
            Confirmed = 2,
            Cancelled = 3,
            Attended = 4,
            DidNotAttend = 5
        }

        #endregion

        #region "  Variables  "

        private Guid _EventID;

        private Guid _ContactID;

        private Guid _ApplicationID;

        private StatusType _Status;

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

        public Guid ApplicationID
        {
            get
            {
                return _ApplicationID;
            }
            set
            {
                if (_ApplicationID != value)
                {
                    _ApplicationID = value;
                    OnPropertyChanged("ApplicationID");
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

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Creates a new instance with default values
        /// </summary>
        /// <returns>A new instance</returns>
        public static new EventAttendee Create()
        {
            EventAttendee obj = new EventAttendee();
            obj.SetDefaultValues();
            return obj;
        }

        public static new EventAttendee Load(Guid ID)
        {
            EventAttendee obj = new EventAttendee();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

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