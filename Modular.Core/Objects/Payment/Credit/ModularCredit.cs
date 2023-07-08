namespace Modular.Core
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

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Invoice";

        #endregion

        #region "  Variables  "

        private Guid _ContactID;

        private OwnerObjectType _ObjectType;

        private Guid _ObjectID;

        private string _CreditNumber = string.Empty;

        private DateTime _CreditDate;

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
                    OnPropertyChanged("InvoiceNumber");
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
                    OnPropertyChanged("InvoiceDate");
                }
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

        public override string ToString()
        {
            return $"Credit #{CreditNumber}";
        }

        #endregion

    }
}
