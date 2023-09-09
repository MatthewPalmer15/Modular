using Modular.Core;
using Modular.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Membership
{
    public class Policy : ModularBase
    {

        #region "  Constructors  "

        public Policy() 
        { 
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_MembershipPolicy";

        #endregion

        #region "  Enums  "

        public enum StatusType
        {
            Unknown = 0,
            Active = 1,
            Inactive = 2,
            Cancelled = 3,
            Deleted = 4
        }

        #endregion

        #region "  Variables  "

        private string _PolicyNumber = string.Empty;

        private Guid _ContactID;

        private List<PolicyRevision> _Revision;

        private StatusType _Status;

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

        public List<PolicyRevision> Revisions
        {
            get
            {
                if (_Revision is null || SystemConfig.GetValue("Modular:ConstantLoadSideObjects").ToUpper() == "TRUE")
                {
                    LoadMembershipRevisions();
                }
                return _Revision;
            }
        }

        public PolicyRevision ActiveRevision
        {
            get
            {
                return Revisions.First(Revision => Revision.Status == Core.Utility.StatusType.Verified);
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

        #region "  Static Methods  "

        /// <summary>
        /// Creates a new instance with default values
        /// </summary>
        /// <returns>A new instance</returns>
        public static new Policy Create()
        {
            Policy obj = new Policy();
            obj.SetDefaultValues();
            return obj;
        }

        public static new Policy Load(Guid ID)
        {
            Policy obj = new Policy();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        private void LoadMembershipRevisions()
        {
            _Revision = new List<PolicyRevision>().OrderByDescending(Revision => Revision.RevisionNumber).ToList();
        }

        public override string ToString()
        {
            return PolicyNumber;
        }

        #endregion

    }
}
