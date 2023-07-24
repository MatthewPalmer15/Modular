using Modular.Core;

namespace Modular.Events
{
    [Serializable]
    public class EventItem : ModularBase
    {

        #region "  Constructors  "

        public EventItem()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Event_Item";

        #endregion

        #region "  Variables  "

        private Guid _EventID;

        private string _Name;

        private string _Description;

        private int _Quantity;

        private decimal _PriceExcVAT;

        private decimal _PriceVAT;

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
        public static new EventItem Create()
        {
            EventItem obj = new EventItem();
            obj.SetDefaultValues();
            return obj;
        }

        public static new EventItem Load(Guid ID)
        {
            EventItem obj = new EventItem();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Name;
        }

        public override EventItem Clone()
        {
            return EventItem.Load(ID);
        }

        #endregion

    }
}