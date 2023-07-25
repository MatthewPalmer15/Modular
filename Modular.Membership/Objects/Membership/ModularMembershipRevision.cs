using Modular.Core;
using Modular.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Membership
{
    public class Revision : ModularBase
    {

        #region "  Constructors  "

        public Revision() 
        { 
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_MembershipRevision";

        #endregion

        #region "  Variables  "

        private int _RevisionNumber;

        private Guid _PolicyID;

        private Guid _MembershipID;

        private Guid _InvoiceID;

        private Guid _CreditID;

        private StatusType _Status;

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

        #endregion

    }
}
