using Modular.Core;
using Modular.Core.Discount;
using Modular.Core.System;
using Modular.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Membership
{
    [Serializable]
    public class Membership : ModularBase
    {

        #region "  Constructors  "

        public Membership()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Membership";

        #endregion

        #region "  Enums  "

        public enum StatusType
        {
            Unknown = 0,
            Unverified = 1,
            Verified = 2,
            Upgraded = 3,
            PendingMTA = 4,
            Cancelled = 4,
            Deleted = 5,
        }

        public enum MembershipType
        {
            Unknown = 0,
            New = 1,
            Renewal = 2,
            Upgrade = 3,
            MTA = 4,
        }

        #endregion

        #region "  Variables  "

        private int _RevisionNumber;

        private Guid _PolicyID;

        private Guid _GroupSchemeMembershipID;

        private Guid _MembershipBranchID;

        private Guid _MembershipLevelID;

        private Guid _InvoiceID;

        private Guid _CreditID;

        private Guid _DocumentPackID;

        private DateTime _ValidFrom;

        private DateTime _ValidTo;

        private StatusType _Status;

        private MembershipType _Type;

        private SourceType _Source;

        private decimal _CostExcTax;

        private decimal _CostTax;

        private decimal _LoadFeeExcTax;

        private decimal _LoadFeeTax;

        private decimal _JoiningFeeExcTax;

        private decimal _JoiningFeeTax;

        private decimal _AdminFeeExcTax;

        private decimal _AdminFeeTax;

        private decimal _DiscountExcTax;

        private decimal _DiscountTax;

        private string _DiscountCode = string.Empty;

        private DateTime _ProcessedDate;

        private Guid _ProcessedBy;

        private DateTime _CancelledDate;

        private Guid _CancelledBy;

        private bool _AutoRenew;

        private Guid _MembershipApplicationID;

        private List<MembershipItem> _Items = new List<MembershipItem>();

        #endregion

        #region "  Properties  "

        public int RevisionNumber
        {
            get
            {
                return _RevisionNumber;
            }
            set
            {
                if (_RevisionNumber != value)
                {
                    _RevisionNumber = value;
                    OnPropertyChanged("RevisionNumber");
                }
            }
        }

        public Guid PolicyID
        {
            get
            {
                return _PolicyID;
            }
            set
            {
                if (_PolicyID != value)
                {
                    _PolicyID = value;
                    OnPropertyChanged("PolicyID");
                }
            }
        }

        public Guid GroupSchemeMembershipID
        {
            get
            {
                return _GroupSchemeMembershipID;
            }
            set
            {
                if (_GroupSchemeMembershipID != value)
                {
                    _GroupSchemeMembershipID = value;
                    OnPropertyChanged("GroupSchemeMembershipID");
                }
            }
        }

        public Guid MembershipBranchID
        {
            get
            {
                return _MembershipBranchID;
            }
            set
            {
                if (_MembershipBranchID != value)
                {
                    _MembershipBranchID = value;
                    OnPropertyChanged("MembershipBranchID");
                }
            }
        }

        public Guid MembershipLevelID
        {
            get
            {
                return _MembershipLevelID;
            }
            set
            {
                if (_MembershipLevelID != value)
                {
                    _MembershipLevelID = value;
                    OnPropertyChanged("MembershipLevelID");
                }
            }
        }

        public List<MembershipItem> Items
        {
            get
            {
                LoadMembershipItems();
                return _Items;
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
                    OnPropertyChanged("CreditID");
                }
            }
        }

        public Guid DocumentPackID
        {
            get
            {
                return _DocumentPackID;
            }
            set
            {
                if (_DocumentPackID != value)
                {
                    _DocumentPackID = value;
                    OnPropertyChanged("DocumentPackID");
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
                if (_ValidFrom != value)
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
                if (_ValidTo != value)
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
                if (_Status != value)
                {

                    switch(value)
                    {
                        case StatusType.Cancelled:
                            CancelledDate = DateTime.Now;
                            CancelledBy = SystemCore.Context.Identity.ID;
                            ProcessedDate = DateTime.MinValue;
                            ProcessedBy = Guid.Empty;
                            break;

                        case StatusType.Verified:
                            CancelledDate = DateTime.MinValue;
                            CancelledBy = Guid.Empty;
                            ProcessedDate = DateTime.Now;
                            ProcessedBy = SystemCore.Context.Identity.ID;
                            break;

                        default:
                            CancelledDate = DateTime.MinValue;
                            CancelledBy = Guid.Empty;
                            ProcessedDate = DateTime.MinValue;
                            ProcessedBy = Guid.Empty;
                            break;
                    }

                    _Status = value;

                    OnPropertyChanged("Status");
                }
            }
        }
        public MembershipType Type
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
                    OnPropertyChanged("Type");
                }
            }
        }
        public SourceType Source
        {
            get
            {
                return _Source;
            }
            set
            {
                if (_Source != value)
                {
                    _Source = value;
                    OnPropertyChanged("Source");
                }
            }
        }

        public decimal MembershipCostExcTax
        {
            get
            {
                return _CostExcTax;
            }
            set
            {
                if (_CostExcTax != value)
                {
                    _CostExcTax = value;
                    OnPropertyChanged("CostExcTax");
                }
            }
        }

        public decimal MembershipCostTax
        {
            get
            {
                return _CostTax;
            }
            set
            {
                if (_CostTax != value)
                {
                    _CostTax = value;
                    OnPropertyChanged("CostTax");
                }
            }
        }

        public decimal MembershipCostIncTax
        {
            get
            {
                return MembershipCostExcTax + MembershipCostTax;
            }
        }

        public decimal ItemCostExcTax
        {
            get
            {
                decimal Total = 0;
                foreach (MembershipItem Item in Items)
                {
                    Total += Item.CostExcTax;
                }
                return Total;
            }
        }

        public decimal ItemCostTax
        {
            get
            {
                decimal Total = 0;
                foreach (MembershipItem Item in Items)
                {
                    Total += Item.CostTax;
                }
                return Total;
            }
        }

        public decimal ItemCostIncTax
        {
            get
            {
                return ItemCostExcTax + ItemCostTax;
            }
        }

        public decimal LoadFeeExcTax
        {
            get
            {
                return _LoadFeeExcTax;
            }
            set
            {
                if (_LoadFeeExcTax != value)
                {
                    _LoadFeeExcTax = value;
                    OnPropertyChanged("LoadFeeExcTax");
                }
            }
        }

        public decimal LoadFeeTax
        {
            get
            {
                return _LoadFeeTax;
            }
            set
            {
                if (_LoadFeeTax != value)
                {
                    _LoadFeeTax = value;
                    OnPropertyChanged("LoadFeeTax");
                }
            }
        }

        public decimal LoadFeeIncTax
        {
            get
            {
                return LoadFeeExcTax + LoadFeeTax;
            }
        }

        public decimal JoiningFeeExcTax
        {
            get
            {
                return _JoiningFeeExcTax;
            }
            set
            {
                if (_JoiningFeeExcTax != value)
                {
                    _JoiningFeeExcTax = value;
                    OnPropertyChanged("JoiningFeeExcTax");
                }
            }
        }

        public decimal JoiningFeeTax
        {
            get
            {
                return _JoiningFeeTax;
            }
            set
            {
                if (_JoiningFeeTax != value)
                {
                    _JoiningFeeTax = value;
                    OnPropertyChanged("JoiningFeeTax");
                }
            }
        }

        public decimal JoiningFeeIncTax
        {
            get
            {
                return JoiningFeeExcTax + JoiningFeeTax;
            }
        }

        public decimal AdminFeeExcTax
        {
            get
            {
                return _AdminFeeExcTax;
            }
            set
            {
                if (_AdminFeeExcTax != value)
                {
                    _AdminFeeExcTax = value;
                    OnPropertyChanged("AdminFeeExcTax");
                }
            }
        }

        public decimal AdminFeeTax
        {
            get
            {
                return _AdminFeeTax;
            }
            set
            {
                if (_AdminFeeTax != value)
                {
                    _AdminFeeTax = value;
                    OnPropertyChanged("AdminFeeTax");
                }
            }
        }

        public decimal AdminFeeIncTax
        {
            get
            {
                return _AdminFeeExcTax + AdminFeeTax;
            }
        }

        public decimal DiscountExcTax
        {
            get
            {
                return _DiscountExcTax;
            }
            set
            {
                if (_DiscountExcTax != value)
                {
                    _DiscountExcTax = value;
                    OnPropertyChanged("DiscountExcTax");
                }
            }
        }

        public decimal DiscountTax
        {
            get
            {
                return _DiscountTax;
            }
            set
            {
                if (_DiscountTax != value)
                {
                    _DiscountTax = value;
                    OnPropertyChanged("DiscountTax");
                }
            }
        }

        public decimal DiscountIncTax
        {
            get
            {
                return _DiscountExcTax + DiscountTax;
            }
        }

        public string DiscountCode
        {
            get
            {
                return _DiscountCode;
            }
            set
            {
                if (_DiscountCode  != value)
                {
                    _DiscountCode = value;
                    OnPropertyChanged("DiscountCode");
                }
            }
        }

        public decimal TotalPriceExcTax
        {
            get
            {
                return (MembershipCostExcTax + ItemCostExcTax + LoadFeeExcTax + JoiningFeeExcTax + AdminFeeExcTax) - DiscountExcTax;
            }
        }

        public decimal TotalPriceTax
        {
            get
            {
                return (MembershipCostTax + ItemCostTax + LoadFeeExcTax + JoiningFeeTax + AdminFeeTax) - DiscountTax;
            }
        }

        public decimal TotalPriceIncTax
        {
            get
            {
                return TotalPriceExcTax + TotalPriceTax;
            }
        }

        public DateTime ProcessedDate
        {
            get
            {
                return _ProcessedDate;
            }
            private set
            {
                if (_ProcessedDate != value) 
                {
                    _ProcessedDate = value;
                    OnPropertyChanged("ProcessedDate");
                }
            }
        }

        public Guid ProcessedBy
        {
            get
            {
                return _ProcessedBy;
            }
            private set
            {
                if (_ProcessedBy != value)
                {
                    _ProcessedBy = value;
                    OnPropertyChanged("PropertyBy");
                }
            }
        }

        public DateTime CancelledDate
        {
            get
            {
                return _CancelledDate;
            }
            private set
            {
                if (_CancelledDate != value)
                {
                    _CancelledDate = value;
                    OnPropertyChanged("CancelledDate");
                }
            }
        }

        public Guid CancelledBy
        {
            get
            {
                return _CancelledBy;
            }
            private set
            {
                if (_CancelledBy != value)
                {
                    _CancelledBy = value;
                    OnPropertyChanged("CancelledBy");
                }
            }
        }

        public bool AutoRenew
        {
            get
            {
                return _AutoRenew;
            }
            set
            {
                if (_AutoRenew != value)
                {
                    _AutoRenew = value;
                    OnPropertyChanged("AutoRenew");
                }
            }
        }

        public Guid MembershipApplicationID
        {
            get
            {
                return _MembershipApplicationID;
            }
            set
            {
                if (_MembershipApplicationID != value)
                {
                    _MembershipApplicationID = value;
                    OnPropertyChanged("MembershipApplicationID");
                }
            }
        }

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Creates a new instance with default values
        /// </summary>
        /// <returns>A new instance</returns>
        public static new Membership Create()
        {
            Membership obj = new Membership();
            obj.SetDefaultValues();
            return obj;
        }

        public static new Membership Load(Guid ID)
        {
            Membership obj = new Membership();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        private void LoadMembershipItems()
        {
            _Items = new List<MembershipItem>();
        }

        public override string ToString()
        {
            return ID.ToString();
        }

        #endregion

    }
}
