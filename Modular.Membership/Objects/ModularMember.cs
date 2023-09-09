using Modular.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Membership
{
    public class Member : ModularBase
    {

        #region "  Constructors  "

        public Member() 
        { 
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Member";

        #endregion

        #region "  Enums  "

        public enum StatusType
        {
            Unknown = 0,
            Active = 1,
            NonActive = 2,
            Hidden = 3,
            Suspended = 4,
            Blocked = 5
        }

        #endregion

        #region "  Variables  "

        private int _MemberNumber;

        private Guid _ContactID;

        private StatusType _Status;

        #endregion

        #region "  Properties  "

        public int MemberNumber
        {
            get
            { 
                return _MemberNumber; 
            }
            set
            {
                if (_MemberNumber != value)
                {
                    _MemberNumber = value;
                    OnPropertyChanged("MemberNumber");
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
                if (_ContactID != value)
                {
                    _ContactID = value;
                    OnPropertyChanged("ContactID");
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
