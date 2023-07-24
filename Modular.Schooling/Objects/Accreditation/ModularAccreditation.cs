using Modular.Core;
using Modular.Core.Invoicing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Schooling.Accreditation
{
    public class Accreditation : ModularBase
    {

        #region "  Constructors  "

        public Accreditation()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Accreditation";

        #endregion

        #region "  Enums  "

        public enum StatusType
        {
            Unknown = 0,
            Active = 1,
            Inactive = 2,
            Pending = 3,
            Cancelled = 4,
            Suspended = 5
        }

        #endregion

        #region "  Variables  "

        private Guid _TrainingCentreID;

        private Guid _AccreditationLevelID;

        private Guid _InvoiceID;

        private string _AccreditationNumber = string.Empty;

        private DateTime _ValidFrom;

        private DateTime _ValidTo;

        private decimal _PriceExcVAT;

        private decimal _PriceVAT;

        private StatusType _Status;

        private string _Notes = string.Empty;

        private List<AccreditationItem> _AccreditationItems = new List<AccreditationItem>();

        #endregion

        #region "  Properties  "

        public Guid TrainingCentreID
        {
            get
            {
                return _TrainingCentreID;
            }
            set
            {
                if (_TrainingCentreID != value)
                {
                    _TrainingCentreID = value;
                    OnPropertyChanged("TrainingCentreID");
                }
            }
        }

        public TrainingCentre TrainingCentre
        {
            get
            {
                return TrainingCentre.Load(TrainingCentreID);
            }
        }

        public Guid AccreditationLevelID
        {
            get
            {
                return _AccreditationLevelID;
            }
            set
            {
                if (_AccreditationLevelID != value)
                {
                    _AccreditationLevelID = value;
                    OnPropertyChanged("AccreditationLevelID");
                }
            }
        }

        public AccreditationLevel AccreditationLevel
        {
            get
            {
                return AccreditationLevel.Load(AccreditationLevelID);
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

        public Invoice Invoice
        {
            get
            {
                return Invoice.Load(InvoiceID);
            }
        }

        public string AccreditationNumber
        {
            get
            {
                return _AccreditationNumber;
            }
            set
            {
                if (_AccreditationNumber != value)
                {
                    _AccreditationNumber = value;
                    OnPropertyChanged("AccreditationNumber");
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

        public decimal PriceExcVAT
        {
            get
            {
                return _PriceExcVAT;
            }
            set
            {
                if (_PriceExcVAT != value)
                {
                    _PriceExcVAT = value;
                    OnPropertyChanged("PriceExcVAT");
                }
            }
        }

        public decimal PriceVAT
        {
            get
            {
                return _PriceVAT;
            }
            set
            {
                if (_PriceVAT != value)
                {
                    _PriceVAT = value;
                    OnPropertyChanged("PriceVAT");
                }
            }
        }

        public decimal PriceIncVAT
        {
            get
            {
                return PriceExcVAT + PriceVAT;
            }
        }

        public decimal ItemPriceExcVAT
        {
            get
            {
                decimal PriceTotal = 0;
                foreach (AccreditationItem Item in AccreditationItems)
                {
                    PriceTotal += Item.PriceExcVAT;
                }
                return PriceTotal;
            }
        }

        public decimal ItemPriceVAT
        {
            get
            {
                decimal PriceTotal = 0;
                foreach (AccreditationItem Item in AccreditationItems)
                {
                    PriceTotal += Item.PriceVAT;
                }
                return PriceTotal;
            }
        }

        public decimal ItemPriceIncVAT
        {
            get
            {
                return ItemPriceExcVAT + ItemPriceVAT;
            }
        }

        public decimal TotalPriceExcVAT
        {
            get
            {
                return PriceExcVAT + ItemPriceExcVAT;
            }
        }

        public decimal TotalPriceVAT
        {
            get
            {
                return PriceVAT + ItemPriceVAT;
            }
        }

        public decimal TotalPriceIncVAT
        {
            get
            {
                return TotalPriceExcVAT + TotalPriceVAT;
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
                    _Status = value;
                    OnPropertyChanged("Status");
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

        public List<AccreditationItem> AccreditationItems
        {
            get
            {
                LoadAccreditationItems();
                return _AccreditationItems;
            }
        }

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Creates a new instance with default values
        /// </summary>
        /// <returns>A new instance</returns>
        public static new Accreditation Create()
        {
            Accreditation obj = new Accreditation();
            obj.SetDefaultValues();
            return obj;
        }

        public static new Accreditation Load(Guid ID)
        {
            Accreditation obj = new Accreditation();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        private void LoadAccreditationItems()
        {
            _AccreditationItems = new List<AccreditationItem>();
            //_AccreditationItems = AccreditationItem.LoadByAccreditationID(ID);
        }

        public override string ToString()
        {
            return $"{TrainingCentre.Name} Accreditation ({ValidFrom:dd-MM-yyyy}-{ValidTo:dd-MM-yyyy}";
        }

        public override Accreditation Clone()
        {
            return Accreditation.Load(ID);
        }

        #endregion

    }
}
