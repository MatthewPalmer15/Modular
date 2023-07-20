using Modular.Core.Entity;
using Modular.Core.Utility;

namespace Modular.Core.Credits
{
    [Serializable]
    public class Credit : ModularBase
    {

        #region "  Constructors  "

        public Credit()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Credit";

        #endregion

        #region "  Enums  "

        public enum CreditStatusType
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

        private CreditStatusType _CreditStatus;

        private string _CreditNumber = string.Empty;

        private DateTime _CreditDate;

        private List<CreditItem> _CreditItems = new List<CreditItem>();

        private List<CreditPayment> _CreditPayments = new List<CreditPayment>();

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

        public Entity.Contact Contact
        {
            get
            {
                return Entity.Contact.Load(_ContactID);
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

        public CreditStatusType CreditStatus
        {
            get
            {
                return _CreditStatus;
            }
            set
            {
                if (_CreditStatus != value)
                {
                    _CreditStatus = value;
                    OnPropertyChanged("CreditStatus");
                }
            }
        }

        public string CreditNumber
        {
            get
            {
                return _CreditNumber;
            }
            set
            {
                if (_CreditNumber != value)
                {
                    _CreditNumber = value;
                    OnPropertyChanged("CreditNumber");
                }
            }
        }

        public DateTime CreditDate
        {
            get
            {
                return _CreditDate;
            }
            set
            {
                if (_CreditDate != value)
                {
                    _CreditDate = value;
                    OnPropertyChanged("CreditDate");
                }
            }
        }

        public List<CreditItem> CreditItems
        {
            get
            {
                LoadCreditItems();
                return _CreditItems;
            }
        }

        public List<CreditPayment> CreditPayments
        {
            get
            {
                LoadCreditPayments();
                return _CreditPayments;
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
                decimal Total = 0;
                foreach (CreditItem Item in CreditItems)
                {
                    Total += Item.TotalPrice;
                }
                return Total;
            }
        }

        public decimal TotalPaid
        {
            get
            {
                decimal Total = 0;
                foreach (CreditPayment Item in CreditPayments)
                {
                    Total += Item.Amount;
                }
                return Total;
            }
        }



        #endregion

        #region "  Static Methods  "

        public static new Credit Create()
        {
            Credit obj = new Credit();
            obj.SetDefaultValues();
            return obj;
        }

        public static new Credit Load(Guid ID)
        {
            Credit obj = new Credit();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        private void LoadCreditItems()
        {
            _CreditItems = CreditItem.LoadAll().Where(CreditItem => CreditItem.CreditID == ID).ToList();
        }

        private void LoadCreditPayments()
        {
            _CreditPayments = CreditPayment.LoadAll().Where(CreditPayment => CreditPayment.CreditID == ID).ToList();
        }

        public override string ToString()
        {
            return $"Credit #{CreditNumber}";
        }

        #endregion

    }
}
