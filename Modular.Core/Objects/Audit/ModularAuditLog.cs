using System.ComponentModel.DataAnnotations;
using Modular.Core.Utility;

namespace Modular.Core.Audit
{
    [Serializable]
    public class AuditLog : ModularBase
    {
        #region "  Constructor  "

        private AuditLog()
        {
        }

        #endregion

        #region "  Constants  "

        protected static readonly new string MODULAR_DATABASE_TABLE = "tbl_Modular_AuditLog";
        protected static new readonly string MODULAR_DATABASE_STOREDPROCEDURE_PREFIX = "usp_Modular_AuditLog";
        protected static new readonly Type MODULAR_OBJECTTYPE = typeof(AuditLog);

        #endregion

        #region "  Variables  "

        private ObjectTypes.ObjectType _ObjectType;

        private Guid _ObjectID;

        private string _Message = string.Empty;

        private string _DeviceInformation = string.Empty;

        #endregion

        #region "  Properties  "

        [Display(Name = "Type")]
        public ObjectTypes.ObjectType ObjectType
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


        [Required(ErrorMessage = "Please enter a message.")]
        [MaxLength(2048, ErrorMessage = "Message should be less than 2048 Characters.")]
        public string Message
        {
            get
            {
                return _Message;
            }
            private set
            {
                _Message = value;
            }
        }

        [Required]
        public string DeviceInformation
        {
            get
            {
                return _DeviceInformation;
            }
            private set
            {
                _DeviceInformation = value;
            }
        }

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Creates a new instance with default values
        /// </summary>
        /// <returns>A new instance</returns>
        public static void Create(ObjectTypes.ObjectType ObjectType, Guid ObjectID, string Message)
        {
            AuditLog obj = new AuditLog();
            obj.SetDefaultValues(); // Prevent any null values.

            obj.ObjectType = ObjectType;
            obj.ObjectID = ObjectID;
            obj.Message = Message;
            obj.DeviceInformation = ModularUtils.GetDeviceSummary();

            obj.Save();
        }


        /// <summary>
        /// Loads an existing instance from the database.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static new AuditLog Load(Guid ID)
        {
            AuditLog obj = new AuditLog();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Message;
        }

        public override AuditLog Clone()
        {
            return AuditLog.Load(ID);
        }

        #endregion

    }
}