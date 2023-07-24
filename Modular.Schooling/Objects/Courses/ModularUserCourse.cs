using Modular.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Schooling
{
    [Serializable]
    public class UserCourse : ModularBase
    {

        #region "  Constructors  "

        public UserCourse()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_UserCourse";

        #endregion

        #region "  Variables  "

        private Guid _ContactID;

        private Guid _CourseID;

        private DateTime _StartDate;

        private DateTime _CompletedDate;

        private int _ModuleProgress;

        #endregion

        #region "  Properties  "

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

        public Guid CourseID
        {
            get
            {
                return _CourseID;
            }
            set
            {
                if (_CourseID != value)
                {
                    _CourseID = value;
                    OnPropertyChanged("CourseID");
                }
            }
        }

        public Course Course
        {
            get
            {
                return Course.Load(CourseID);
            }
        }

        public DateTime StartDate
        {
            get
            {
                return _StartDate;
            }
            set
            {
                if (_StartDate != value)
                {
                    _StartDate = value;
                    OnPropertyChanged("StartDate");
                }
            }
        }

        public DateTime CompletedDate
        {
            get
            {
                return _CompletedDate;
            }
            set
            {
                if (_CompletedDate != value)
                {
                    _CompletedDate = value;
                    OnPropertyChanged("CompletedDate");
                }
            }
        }

        public bool IsCompleted
        {
            get
            {
                return CompletedDate != DateTime.MinValue;
            }
        }

        public int ModuleProgress
        {
            get
            {
                return _ModuleProgress;
            }
            set
            {
                if (_ModuleProgress != value)
                {
                    _ModuleProgress = value;
                    OnPropertyChanged("ModuleProgress");
                }
            }
        }

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Creates a new instance with default values
        /// </summary>
        /// <returns>A new instance</returns>
        public static new UserCourse Create()
        {
            UserCourse obj = new UserCourse();
            obj.SetDefaultValues();
            return obj;
        }

        public static new UserCourse Load(Guid ID)
        {
            UserCourse obj = new UserCourse();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Course.Name;
        }

        public override UserCourse Clone()
        {
            return UserCourse.Load(ID);
        }

        #endregion

    }
}
