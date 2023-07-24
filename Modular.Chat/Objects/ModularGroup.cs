using Modular.Core;

namespace Modular.Chat
{
    [Serializable]
    public class Group : ModularBase
    {

        #region "  Constructors  "

        public Group()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Chat_Group";

        #endregion

        #region "  Variables  "

        private Guid _CreatorID;

        private Guid _OwnerID;

        private string _Name = string.Empty;

        private string _Description = string.Empty;

        private bool _IsPublic = false;

        private List<GroupMember> _Members = new List<GroupMember>();

        private List<Message> _Messages = new List<Message>();

        #endregion

        #region "  Properties  "

        public Guid CreatorID
        {
            get
            {
                return _CreatorID;
            }
            private set
            {
                if (_CreatorID != value)
                {
                    _CreatorID = value;
                    OnPropertyChanged("CreatorID");
                }
            }
        }

        public Guid OwnerID
        {
            get
            {
                return _OwnerID;
            }
            set
            {
                if (_OwnerID != value)
                {
                    _OwnerID = value;
                    OnPropertyChanged("OwnerID");
                }
            }
        }

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

        public bool IsPublic
        {
            get
            {
                return _IsPublic;
            }
            set
            {
                if (_IsPublic != value)
                {
                    _IsPublic = value;
                    OnPropertyChanged("IsPublic");
                }
            }
        }

        public List<GroupMember> Members
        {
            get
            {
                LoadGroupMembers();
                return _Members;
            }
        }

        public List<Message> Messages
        {
            get
            {
                LoadMessages();
                return _Messages;
            }
        }

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Creates a new instance with default values
        /// </summary>
        /// <returns>A new instance</returns>
        public static new Group Create()
        {
            Group obj = new Group();
            obj.SetDefaultValues();
            return obj;
        }

        public static new Group Load(Guid ID)
        {
            Group obj = new Group();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        private void LoadGroupMembers()
        {
            _Members = new List<GroupMember>();
            //_Members = GroupMember.LoadByGroupID(ID);
        }

        public void LoadMessages()
        {
            _Messages = new List<Message>();
            //_Messages = Message.LoadByGroupID(ID);
        }

        public override string ToString()
        {
            return Name;
        }

        public override Group Clone()
        {
            return Group.Load(ID);
        }

        #endregion

    }
}