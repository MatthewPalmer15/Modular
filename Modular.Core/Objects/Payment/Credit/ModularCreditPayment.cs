namespace Modular.Core
{
    public class CreditPayment : ModularBase
    {

        #region "  Constructors  "

        public CreditPayment()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Payment";

        #endregion

        #region "  Enums  "

        public enum PaymentMethodType
        {
            Unknown = 0,
            Cash = 1,
            Cheque = 2,
            CreditCard = 3,
            DirectDebit = 4,
            EFT = 5,
            PayPal = 6
        }

        #endregion

        #region "  Variables  "

        private Guid _InvoiceID;

        private string _Reference = string.Empty;

        private DateTime _PaymentDate;

        private PaymentMethodType _PaymentMethod;

        private decimal _Amount;

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

        public string Reference
        {
            get
            {
                return _Reference;
            }
            set
            {
                if (_Reference != value)
                {
                    _Reference = value;
                    OnPropertyChanged("Reference");
                }
            }
        }

        public DateTime PaymentDate
        {
            get
            {
                return _PaymentDate;
            }
            set
            {
                if (_PaymentDate != value)
                {
                    _PaymentDate = value;
                    OnPropertyChanged("PaymentDate");
                }
            }
        }

        public PaymentMethodType PaymentMethod
        {
            get
            {
                return _PaymentMethod;
            }
            set
            {
                if (_PaymentMethod != value)
                {
                    _PaymentMethod = value;
                    OnPropertyChanged("PaymentMethod");
                }
            }
        }

        public decimal Amount
        {
            get
            {
                return _Amount;
            }
            set
            {
                if (_Amount != value)
                {
                    _Amount = value;
                    OnPropertyChanged("Amount");
                }
            }
        }

        #endregion

    }
}
