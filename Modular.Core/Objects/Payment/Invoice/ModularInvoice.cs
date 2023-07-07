namespace Modular.Core
{
    [Serializable]
    public class Invoice : ModularBase
    {

        #region "  Constructors  "

        public Invoice()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Invoice";

        #endregion

        #region "  Variables  "

        private Guid _ContactID;

        private OwnerObjectType _ObjectType;

        private Guid _ObjectID;

        private string _InvoiceNumber = string.Empty;

        private DateTime _InvoiceDate;

        private bool _IsPaid;

        private DateTime _PaidDate;

        #endregion

        #region "  Properties  "

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

        public string InvoiceNumber
        {
            get
            {
                return _InvoiceNumber;
            }
            set
            {
                if (_InvoiceNumber != value)
                {
                    _InvoiceNumber = value;
                    OnPropertyChanged("InvoiceNumber");
                }
            }
        }

        public DateTime InvoiceDate
        {
            get
            {
                return _InvoiceDate;
            }
            set
            {
                if (_InvoiceDate != value)
                {
                    _InvoiceDate = value;
                    OnPropertyChanged("InvoiceDate");
                }
            }
        }

        public bool IsPaid
        {
            get
            {
                return _IsPaid;
            }
            set
            {
                if (_IsPaid != value)
                {
                    _IsPaid = value;
                    OnPropertyChanged("IsPaid");
                }
            }
        }

        public DateTime PaidDate
        {
            get
            {
                return _PaidDate;
            }
            set
            {
                if (_PaidDate != value)
                {
                    _PaidDate = value;
                    OnPropertyChanged("PaidDate");
                }
            }
        }

        public List<InvoiceItem> InvoiceLines
        {
            get
            {
                return InvoiceItem.LoadInstances();
            }
        }

        public List<InvoicePayment> InvoicePayments
        {
            get
            {
                return InvoicePayment.LoadInstances();
            }
        }

        public decimal TotalPrice
        {
            get
            {
                decimal total = 0;
                foreach (InvoiceItem item in InvoiceLines)
                {
                    total += item.TotalPrice;
                }
                return total;
            }
        }

        #endregion

        #region "  Static Methods  "

        public static new Invoice Create()
        {
            Invoice obj = new Invoice();
            obj.SetDefaultValues();
            return obj;
        }

        public static new Invoice Load(Guid ID)
        {
            Invoice obj = new Invoice();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return $"Invoice #{_InvoiceNumber}";
        }

        #endregion

    }
}
