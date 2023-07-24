using Modular.Core;

namespace Modular.Chat
{
    public class GroupMember : ModularBase
    {

        #region "  Constructors  "

        public GroupMember()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Chat_GroupMember";

        #endregion

        #region "  Variables  "

        private Guid _GroupID;

        private Guid _ContactID;

        private bool _IsAdmin = false;

        #endregion

        #region "  Properties  "

        public Guid GroupID
        {
            get
            {
                return _GroupID;
            }
            private set
            {
                if (_GroupID != value)
                {
                    _GroupID = value;
                    OnPropertyChanged("GroupID");
                }
            }
        }

        public Group Group
        {
            get
            {
                return Group.Load(GroupID);
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

        public bool IsAdmin
        {
            get
            {
                return _IsAdmin;
            }
            set
            {
                if (_IsAdmin != value)
                {
                    _IsAdmin = value;
                    OnPropertyChanged("IsAdmin");
                }
            }
        }

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Creates a new instance with default values
        /// </summary>
        /// <returns>A new instance</returns>
        public static new GroupMember Create()
        {
            GroupMember obj = new GroupMember();
            obj.SetDefaultValues();
            return obj;
        }

        public static new GroupMember Load(Guid ID)
        {
            GroupMember obj = new GroupMember();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Contact.FullName;
        }

        public override GroupMember Clone()
        {
            return GroupMember.Load(ID);
        }

        #endregion

    }
}