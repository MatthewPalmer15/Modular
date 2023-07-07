namespace Modular.Core
{
    public class Notification : ModularBase
    {

        #region "  Constructors  "

        public Notification()
        {
        }

        public Notification(string message)
        {
            Message = message;
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Notification";

        #endregion

        #region "  Enums  "

        public enum NotificationStatusType
        {
            Unknown = 1,
            Pending = 2,
            Delivered = 3
        }

        #endregion

        #region "  Variables  "

        private string _Message = string.Empty;

        private NotificationStatusType _Status = NotificationStatusType.Unknown;

        private Guid _ContactID;

        #endregion

        #region "  Properties  "

        public string Message
        {
            get
            {
                return _Message;
            }
            set
            {
                if (_Message != value)
                {
                    _Message = value;
                    OnPropertyChanged("Message");
                }
            }
        }

        public NotificationStatusType Status
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

        #endregion

        #region "  Static Methods  "

        public static new Notification Create()
        {
            Notification obj = new Notification();
            obj.SetDefaultValues();
            return obj;
        }

        public static new Notification Load(Guid ID)
        {
            Notification obj = new Notification();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Data Methods  "

        public static List<Notification> LoadAll()
        {
            // TODO: Load all notifications from the database
            List<Notification> list = new List<Notification>();
            return list;
        }

        #endregion
    }
}
