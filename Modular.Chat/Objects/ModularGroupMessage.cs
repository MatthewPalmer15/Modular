using Modular.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Chat
{
    [Serializable]
    public class Message : ModularBase
    {
        #region "  Constructors  "

        public Message() 
        { 
        }

        // ~Message()
        // {
        //     Save();
        // }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Chat_Message";

        #endregion

        #region "  Variables  "

        private Guid _GroupID;

        private Guid _GroupMemberID;

        private string _Text = string.Empty;

        private DateTime _SentDate;

        private DateTime _DeletedDate;

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

        public Guid GroupMemberID
        {
            get
            {
                return _GroupMemberID;
            }
            private set
            {
                if (_GroupMemberID != value)
                {
                    _GroupMemberID = value;
                    OnPropertyChanged("GroupMemberID");
                }
            }
        }

        public string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                if (_Text != value)
                {
                    _Text = value;
                    OnPropertyChanged("Text");
                }
            }
        }

        public DateTime SentDate
        {
            get
            {
                return _SentDate;
            }
            set
            {
                if (_SentDate != value)
                {
                    _SentDate = value;
                    OnPropertyChanged("SentDate");
                }
            }
        }

        public DateTime DeletedDate
        {
            get
            {
                return _DeletedDate;
            }
            set
            {
                if (_DeletedDate != value)
                {
                    _DeletedDate = value;
                    OnPropertyChanged("DeletedDate");
                }
            }
        }

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Creates a new instance with default values
        /// </summary>
        /// <returns>A new instance</returns>
        public static new Message Create()
        {
            Message obj = new Message();
            obj.SetDefaultValues();
            return obj;
        }

        public static new Message Load(Guid ID)
        {
            Message obj = new Message();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Text;
        }

        public override Message Clone()
        {
            return Message.Load(ID);
        }

        #endregion



    }
}
