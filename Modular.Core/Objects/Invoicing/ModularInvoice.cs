namespace Modular.Core
{

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

        #region "  Enums  "

        public enum InvoiceStatusType
        {
            Unknown = 0,
            Open = 1,
            Closed = 2,
            Completed = 3,
            Cancelled = 4,
            Deleted = 5
        }

        #endregion

        #region "  Variables  "

        private Guid _ContactID;

        private ObjectType _ObjectType;

        private Guid _ObjectID;

        private InvoiceStatusType _InvoiceStatus;

        private string _InvoiceNumber = string.Empty;

        private DateTime _InvoiceDate;

        private List<InvoiceItem> _InvoiceItems = new List<InvoiceItem>();

        private List<InvoicePayment> _InvoicePayments = new List<InvoicePayment>();

        private bool _IsPaid;

        private DateTime _PaidDate;

        private bool _IsPrinted;

        private DateTime _PrintedDate;

        private string _PONumber = string.Empty;

        private string _Notes = string.Empty;

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

        public ObjectType ObjectType
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

        public InvoiceStatusType InvoiceStatus
        {
            get
            {
                return _InvoiceStatus;
            }
            set
            {
                if (_InvoiceStatus != value)
                {
                    _InvoiceStatus = value;
                    OnPropertyChanged("InvoiceStatus");
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

        public List<InvoiceItem> InvoiceItems
        {
            get
            {
                return _InvoiceItems;
            }
            set
            {
                if (_InvoiceItems != value)
                {
                    _InvoiceItems = value;
                    OnPropertyChanged("InvoiceItems");
                }
            }
        }

        public List<InvoicePayment> InvoicePayments
        {
            get
            {
                return _InvoicePayments;
            }
            set
            {
                if (_InvoicePayments != value)
                {
                    _InvoicePayments = value;
                    OnPropertyChanged("InvoicePayments");
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

        public bool IsPrinted
        {
            get
            {
                return _IsPrinted;
            }
            set
            {
                if (_IsPrinted != value)
                {
                    _IsPrinted = value;
                    OnPropertyChanged("IsPrinted");
                }
            }
        }

        public DateTime PrintedDate
        {
            get
            {
                return _PrintedDate;
            }
            set
            {
                if (_PrintedDate != value)
                {
                    _PrintedDate = value;
                    OnPropertyChanged("PrintedDate");
                }
            }
        }

        public string PONumber
        {
            get
            {
                return _PONumber;
            }
            set
            {
                if (_PONumber != value)
                {
                    _PONumber = value;
                    OnPropertyChanged("PONumber");
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

        public decimal TotalPrice
        {
            get
            {
                List<InvoiceItem> InvoiceLines = GetInvoiceItems();
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

        public InvoiceItem CreateInvoiceItem()
        {
            InvoiceItem obj = InvoiceItem.Create(ID);
            return obj;
        }

        public InvoicePayment CreateInvoicePayment()
        {
            InvoicePayment obj = InvoicePayment.Create(ID);
            return obj;
        }

        public List<InvoiceItem> GetInvoiceItems()
        {
            return InvoiceItem.LoadAll().Where(InvoiceItem => InvoiceItem.InvoiceID == ID).ToList();
        }

        public List<InvoicePayment> GetInvoicePayments()
        {
            return InvoicePayment.LoadAll().Where(InvoicePayment => InvoicePayment.InvoiceID == ID).ToList();
        }


        public void LoadInvoiceItems()
        {
            _InvoiceItems = GetInvoiceItems();
        }

        public void LoadInvoicePayments()
        {
            _InvoicePayments = GetInvoicePayments();
        }

        #endregion

    }
}
