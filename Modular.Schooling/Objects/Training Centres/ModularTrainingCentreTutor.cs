using Modular.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Schooling
{
    public class Tutor : ModularBase
    {

        #region "  Constructors  "

        public Tutor()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_TrainingCentre_Tutor";

        #endregion

        #region "  Enums  "

        public enum StatusType
        {
            Unknown = 0,
            Active = 1,
            Inactive = 2,
            Deleted = 3
        }

        #endregion

        #region "  Variables  "

        private Guid _TrainingCentreID;

        private Guid _ContactID;

        private StatusType _Status;

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

        public Core.Entity.Contact Contact
        {
            get
            {
                return Core.Entity.Contact.Load(ContactID);
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
        public static new Tutor Create()
        {
            Tutor obj = new Tutor();
            obj.SetDefaultValues();
            return obj;
        }

        public static new Tutor Load(Guid ID)
        {
            Tutor obj = new Tutor();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return $"{Contact.FullName} at {TrainingCentre.Name}";
        }

        public override Tutor Clone()
        {
            return Tutor.Load(ID);
        }

        #endregion

    }
}
