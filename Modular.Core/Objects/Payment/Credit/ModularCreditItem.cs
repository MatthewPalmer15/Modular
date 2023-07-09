namespace Modular.Core
{

    [Serializable]
    public class CreditItem : ModularBase
    {

        #region "  Constructors  "

        public CreditItem()
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

        private Guid _CreditID;

        private ObjectType _ObjectType;

        private Guid _ObjectID;

        private decimal _UnitPrice;

        private decimal _Quantity;

        #endregion

        #region "  Properties  "

        public Guid CreditID
        {
            get
            {
                return _CreditID;
            }
            set
            {
                if (_CreditID != value)
                {
                    _CreditID = value;
                    OnPropertyChanged("InvoiceID");
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

        #endregion

        #region "  Static Methods  "

        public static CreditItem Create(Guid CreditID)
        {
            CreditItem obj = new CreditItem();
            obj.SetDefaultValues();
            obj.CreditID = CreditID;
            return obj;
        }

        public static new CreditItem Load(Guid ID)
        {
            CreditItem obj = new CreditItem();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

    }
}
