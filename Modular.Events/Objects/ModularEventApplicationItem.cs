using Modular.Core;
using Modular.Core.Invoicing;

namespace Modular.Events
{
    [Serializable]
    public class EventApplicationItem : ModularBase
    {

        #region "  Constructors  "

        public EventApplicationItem()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Event_ApplicationItem";

        #endregion

        #region "  Variables  "

        private Guid _ApplicationID;

        private Guid _EventItem;

        private Guid _InvoiceID;

        private decimal _PriceExcVAT;

        private decimal _PriceVAT;

        #endregion

        #region "  Properties  "

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

        public Guid EventItem
        {
            get
            {
                return _EventItem;
            }
            set
            {
                if (_EventItem != value)
                {
                    _EventItem = value;
                    OnPropertyChanged("EventItem");
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

        #endregion



    }
}