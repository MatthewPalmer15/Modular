using Modular.Core;

namespace Modular.Bookings
{
    [Serializable]
    public class BookingItem : ModularBase
    {

        #region "  Constructors  "

        public BookingItem()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Booking_Item";

        #endregion

        #region "  Variables  "

        private Guid _BookingID;

        private Guid _InvoiceID;

        private Guid _VenueItemID;

        private int _Quantity;

        #endregion

        #region "  Properties  "

        public Guid BookingID
        {
            get
            {
                return _BookingID;
            }
            set
            {
                if (_BookingID != value)
                {
                    _BookingID = value;
                    OnPropertyChanged("BookingID");
                }
            }
        }

        public Guid ItemID
        {
            get
            {
                return _VenueItemID;
            }
            set
            {
                if (_VenueItemID != value)
                {
                    _VenueItemID = value;
                    OnPropertyChanged("VenueItemID");
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

        public int Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                if (_Quantity != value)
                {
                    _Quantity = value;
                    OnPropertyChanged("Quantity");
                }
            }
        }

        #endregion

        #region "  Static Methods  "

        public static new BookingItem Create()
        {
            BookingItem obj = new BookingItem();
            obj.SetDefaultValues();
            return obj;
        }

        public static new BookingItem Load(Guid ID)
        {
            BookingItem obj = new BookingItem();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return ID.ToString();
        }

        #endregion

    }
}