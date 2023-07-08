namespace Modular.Core
{

    [Serializable]
    public class InvoiceItem : ModularBase
    {

        #region "  Constructors  "

        public InvoiceItem()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_InvoiceLine";

        #endregion

        #region "  Enums  "

        public enum InvoiceType
        {
            Unknown = 0,
            Invoice = 1,
            Credit = 2,
            Quote = 3
        }

        #endregion

        #region "  Variables  "

        private Guid _InvoiceID;

        private InvoiceType _Type;

        private OwnerObjectType _ObjectType;

        private Guid _ObjectID;


        private decimal _UnitPrice;

        private decimal _Quantity;

        #endregion

        #region "  Properties  "

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

        public Invoice Invoice
        {
            get
            {
                return Invoice.Load(InvoiceID);
            }
        }

        public InvoiceType Type
        {
            get
            {
                return _Type;
            }
            set
            {
                if (_Type != value)
                {
                    _Type = value;
                    OnPropertyChanged("InvoiceType");
                }
            }
        }

        public OwnerObjectType ObjectType
        {
            get
            {
                return _ObjectType;
            }
            set
            {
                if (_ObjectType != value)
                {
                    _ObjectType = value;
                    OnPropertyChanged("ObjectType");
                }
            }
        }

        public Guid ObjectID
        {
            get
            {
                return _ObjectID;
            }
            set
            {
                if (_ObjectID != value)
                {
                    _ObjectID = value;
                    OnPropertyChanged("ObjectID");
                }
            }
        }

        public decimal UnitPrice
        {
            get
            {
                return _UnitPrice;
            }
            set
            {
                if (_UnitPrice != value)
                {
                    _UnitPrice = value;
                    OnPropertyChanged("UnitPrice");
                }
            }
        }

        public decimal Quantity
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

        public decimal TotalPrice
        {
            get
            {
                return UnitPrice * Quantity;
            }
        }

        #endregion

        #region "  Static Methods  "

        public static InvoiceItem Create(Guid InvoiceID)
        {
            InvoiceItem obj = new InvoiceItem();
            obj.SetDefaultValues();
            obj.InvoiceID = InvoiceID;
            return obj;
        }

        public static new InvoiceItem Load(Guid ID)
        {
            InvoiceItem obj = new InvoiceItem();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Data Methods  "

        public static new List<InvoiceItem> LoadInstances()
        {
            // TODO:  Add ModularInvoiceItem.LoadInstances implementation
            return new List<InvoiceItem>();
        }

        #endregion

    }
}
