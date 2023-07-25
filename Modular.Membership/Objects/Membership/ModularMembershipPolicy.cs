using Modular.Core;
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

        #region "  Variables  "

        private string _PolicyNumber = string.Empty;

        private Guid _MemberID;

        private List<Revision> _Revision;

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

        public Guid MemberID
        {
            get
            {
                return _MemberID;
            }
            set
            {
                if (_MemberID != value)
                {
                    _MemberID = value;
                    OnPropertyChanged("MemberID");
                }
            }
        }

        public List<Revision> Revisions
        {
            get
            {
                LoadMembershipRevisions();
                return _Revision;
            }
        }

        public Revision ActiveRevision
        {
            get
            {
                return Revisions.First(Revision => Revision.Status == Core.Utility.StatusType.Verified);
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
            _Revision = new List<Revision>().OrderByDescending(Revision => Revision.RevisionNumber).ToList();
        }

        public override string ToString()
        {
            return PolicyNumber;
        }

        #endregion

    }
}
