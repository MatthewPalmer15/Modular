namespace Modular.Core.Discount
{

    [Serializable]
    public class DiscountVoucher : ModularBase
    {

        #region "  Constructors  "

        public DiscountVoucher()
        {
        }

        #endregion

        #region "  Enums  "

        public enum StatusType
        {
            Unknown = 0,
            Active = 1,
            Inactive = 2,
            Expired = 3,
            Used = 4
        }

        public enum ReductionType
        {
            Unknown = 0,
            Percentage = 1,
            Amount = 2
        }

        public enum UsageType
        {
            Unknown = 0,
            SingleUse = 1,
            MultiUse = 2
        }

        #endregion

        #region "  Variables  "

        private string _Name = string.Empty;

        private string _Code = string.Empty;

        private string _Description = string.Empty;

        private Guid _ContactID;

        private DateTime _ValidFrom;

        private DateTime _ValidTo;

        private StatusType _Status;

        private ReductionType _ReductionMode;

        private decimal _Reduction;

        private int _Quantity;

        #endregion

        #region "  Properties  "

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (value != _Name)
                {
                    _Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public string Code
        {
            get
            {
                return _Code;
            }
            set
            {
                if (value != _Code)
                {
                    _Code = value;
                    OnPropertyChanged("Code");
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
                if (value != _Description)
                {
                    _Description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        public Guid ContactID
        {
            get
            {
                return _ContactID;
            }
            set
            {
                if (value != _ContactID)
                {
                    _ContactID = value;
                    OnPropertyChanged("ContactID");
                }
            }
        }

        public DateTime ValidFrom
        {
            get
            {
                return _ValidFrom;
            }
            set
            {
                if (value != _ValidFrom)
                {
                    _ValidFrom = value;
                    OnPropertyChanged("ValidFrom");
                }
            }
        }

        public DateTime ValidTo
        {
            get
            {
                return _ValidTo;
            }
            set
            {
                if (value != _ValidTo)
                {
                    _ValidTo = value;
                    OnPropertyChanged("ValidTo");
                }
            }
        }

        public StatusType Status
        {
            get
            {
                return _Status;
            }
            set
            {
                if (value != _Status)
                {
                    _Status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        public ReductionType ReductionMode
        {
            get
            {
                return _ReductionMode;
            }
            set
            {
                if (value != _ReductionMode)
                {
                    _ReductionMode = value;
                    OnPropertyChanged("ReductionMode");
                }
            }
        }

        public decimal Reduction
        {
            get
            {
                return _Reduction;
            }
            set
            {
                if (value != _Reduction)
                {
                    _Reduction = value;
                    OnPropertyChanged("Reduction");
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
                if (value != _Quantity)
                {
                    _Quantity = value;
                    OnPropertyChanged("Quantity");
                }
            }
        }

        #endregion

        #region "  Static Methods  "

        public static new DiscountVoucher Create()
        {
            DiscountVoucher obj = new DiscountVoucher();
            obj.SetDefaultValues();
            return obj;
        }

        public static new DiscountVoucher Load(Guid ID)
        {
            DiscountVoucher obj = new DiscountVoucher();
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
    }

}
