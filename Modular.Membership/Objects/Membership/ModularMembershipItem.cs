using Modular.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Membership
{
    public class MembershipItem : ModularBase
    {

        #region "  Constructors  "

        public MembershipItem() 
        { 
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_MembershipItem";

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

        #endregion

        #region "  Variables  "

        private string _Name;

        private string _Description;

        private Guid _MembershipID;

        private Guid _InvoiceID;

        private decimal _CostExcTax;

        private decimal _CostTax;

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
                if (_Name != value)
                {
                    _Name = value;
                    OnPropertyChanged("Name");
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
                if (_Description != value)
                {
                    _Description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        public Guid MembershipID
        { 
            get 
            { 
                return _MembershipID; 
            }
            set
            {
                if (_MembershipID != value)
                {
                    _MembershipID = value;
                    OnPropertyChanged("MembershipID");
                }
            }
        }

        public decimal CostExcTax
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

        public decimal CostTax
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

        public decimal CostIncTax
        {
            get
            {
                return CostExcTax + CostTax;
            }
        }

        #endregion


    }
}
