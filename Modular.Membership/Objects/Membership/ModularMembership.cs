using Modular.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Membership
{
    public class Membership : ModularBase
    {

        #region "  Constructors  "

        public ModularMembership() 
        { 
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Membership";

        #endregion

        #region "  Variables  "

        private Guid _MembershipLevelID;

        private decimal _CostExcTax;

        private decimal _CostTax;

        #endregion

        #region "  Properties  "

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

        public static new List<Membership> LoadAll()
        {
            return Membership();
        }

        public static new Membership Load(Guid ID)
        {
            Membership obj = new Membership();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return ID.ToString();
        }

        #endregion

    }
}
