using Modular.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Membership
{
    public class MembershipPolicy : ModularBase
    {

        #region "  Constructors  "

        public MembershipPolicy() 
        { 
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_MembershipPolicy";

        #endregion

        #region "  Variables  "

        private string _PolicyNumber = string.Empty;

        private 

        #endregion

        #region "  Properties  "

        public string PolicyNumber
        {
            get
            {
                return _PolicyNumber;
            }
            set
            {
                if (_PolicyNumber != value)
                {
                    _PolicyNumber = value;
                    OnPropertyChanged("PolicyNumber");
                }
            }
        }

        #endregion
    }
}
