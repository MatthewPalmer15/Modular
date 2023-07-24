using Modular.Core;
using Modular.Core.Invoicing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Schooling.Accreditation
{
    public class AccreditationItem : ModularBase
    {

        #region "  Constructors  "

        public AccreditationItem()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_AccreditationItem";

        #endregion

        #region "  Variables  "

        private Guid _AccreditationID;

        private Guid _AccreditationLevelItemID;

        private Guid _InvoiceID;

        private DateTime _ValidFrom;

        private DateTime _ValidTo;

        private decimal _PriceExcVAT;

        private decimal _PriceVAT;

        #endregion

        #region "  Properties  "

        public Guid AccreditationID
        {
            get
            {
                return _AccreditationID;
            }
            set
            {
                if (_AccreditationID != value)
                {
                    _AccreditationID = value;
                    OnPropertyChanged("AccreditationID");
                }
            }
        }

        public Accreditation Accreditation
        {
            get
            {
                return Accreditation.Load(AccreditationID);
            }
        }

        public Guid ItemID
        {
            get
            {
                return _AccreditationLevelItemID;
            }
            set
            {
                if (_AccreditationLevelItemID != value)
                {
                    _AccreditationLevelItemID = value;
                    OnPropertyChanged("ItemID");
                }
            }
        }

        public AccreditationLevelItem Item
        {
            get
            {
                return AccreditationLevelItem.Load(ItemID);
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
                return _PriceExcVAT + _PriceVAT;
            }
        }

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Creates a new instance with default values
        /// </summary>
        /// <returns>A new instance</returns>
        public static new AccreditationItem Create()
        {
            AccreditationItem obj = new AccreditationItem();
            obj.SetDefaultValues();
            return obj;
        }

        public static new AccreditationItem Load(Guid ID)
        {
            AccreditationItem obj = new AccreditationItem();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Item.Name;
        }

        public override AccreditationItem Clone()
        {
            return AccreditationItem.Load(ID);
        }

        #endregion

    }
}
