namespace Modular.Core.Invoicing
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

        #region "  Variables  "

        private Guid _InvoiceID;

        private string _Name = string.Empty;

        private string _Description = string.Empty;

        private decimal _UnitPrice;

        private decimal _UnitPriceVAT;

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

        public decimal UnitPriceVAT
        {
            get
            {
                return _UnitPriceVAT;
            }
            set
            {
                if (_UnitPriceVAT != value)
                {
                    _UnitPriceVAT = value;
                    OnPropertyChanged("UnitPriceVAT");
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

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Name;
        }

        #endregion

        #region "  Data Methods  "

        public static new List<InvoiceItem> LoadAll()
        {
            // TODO:  Add ModularInvoiceItem.LoadInstances implementation
            return new List<InvoiceItem>();
        }

        #endregion

    }
}
