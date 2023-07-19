using System.ComponentModel.DataAnnotations;

namespace Modular.Core
{
    public class AuditLog : ModularBase
    {
        #region "  Constructor  "

        private AuditLog()
        {
        }

        #endregion

        #region "  Constants  "

        protected static readonly new string MODULAR_DATABASE_TABLE = "tbl_Modular_AuditLog";

        #endregion

        #region "  Variables  "

        private ObjectType _ObjectType;

        private Guid _ObjectID;

        private string _Message = string.Empty;

        private string _DeviceInformation = string.Empty;

        #endregion

        #region "  Properties  "

        [Required]
        public ObjectType ObjectType
        {
            get
            {
                return _ObjectType;
            }
            private set
            {
                if (_ObjectType != value)
                {
                    _ObjectType = value;
                }
            }
        }

        [Required]
        public Guid ObjectID
        {
            get
            {
                return _ObjectID;
            }
            private set
            {
                if (_ObjectID != value)
                {
                    _ObjectID = value;
                }
            }
        }

        [Required]
        [MinLength(2)]
        [MaxLength(1000)]
        public string Message
        {
            get
            {
                return _Message;
            }
            private set
            {
                if (_Message != value)
                {
                    _Message = value;
                }
            }
        }

        public string DeviceInformation
        {
            get
            {
                return _DeviceInformation;
            }
            private set
            {
                if (_DeviceInformation != value)
                {
                    _DeviceInformation = value;
                }
            }
        }

        #endregion

        #region "  Static Methods  "

        public static void Create(string Message)
        {
            Create(ObjectType.Unknown, Guid.Empty, Message);
        }

        /// <summary>
        /// Creates a new instance with default values
        /// </summary>
        /// <returns>A new instance</returns>
        public static void Create(ObjectType ObjectType, Guid ObjectID, string Message)
        {
            AuditLog obj = new AuditLog()
            {
                ObjectType = ObjectType,
                ObjectID = ObjectID,
                Message = Message,
                DeviceInformation = ModularUtils.GetDeviceSummary()

            };
            obj.SetDefaultValues(); // Prevent any null values.
            obj.Save();
        }

        public static new AuditLog Load(Guid ID)
        {
            AuditLog obj = new AuditLog();
            obj.Fetch(ID);
            return obj;
        }

        #endregion
    }
}