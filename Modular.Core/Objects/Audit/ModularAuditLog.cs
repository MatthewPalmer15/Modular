using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Modular.Core.Databases;
using Modular.Core.Utility;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;

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


        /// <summary>
        /// Loads all instances from the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ModularException"></exception>
        public static new List<AuditLog> LoadList()
        {
            List<AuditLog> AllAuditLogs = new List<AuditLog>();

            // Check if the database can be connected to.
            if (Database.CheckDatabaseConnection())
            {
                FieldInfo[] AllFields = Class.GetFields();

                // If table does not exist within the database, create it.
                if (!Database.CheckDatabaseTableExists(MODULAR_DATABASE_TABLE))
                {
                    DatabaseUtils.CreateDatabaseTable(MODULAR_DATABASE_TABLE, AllFields);
                }

                switch (Database.ConnectionMode)
                {
                    // If the database is a remote database, connect to it.
                    case Database.DatabaseConnectivityMode.Remote:
                        using (SqlConnection Connection = new SqlConnection(Database.ConnectionString))
                        {
                            Connection.Open();
                            string StoredProcedureName = $"{MODULAR_DATABASE_STOREDPROCEDURE_PREFIX}_Fetch";

                            // If stored procedures are enabled, and the stored procedure does not exist, create it.
                            if (Database.EnableStoredProcedures && !Database.CheckStoredProcedureExists(StoredProcedureName))
                            {
                                DatabaseUtils.CreateStoredProcedure(DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE, AllFields.SingleOrDefault(x => x.Name.Equals("_ID"))), StoredProcedureName);
                            }

                            using (SqlCommand Command = new SqlCommand())
                            {
                                Command.Connection = Connection;
                                Command.CommandType = Database.EnableStoredProcedures ? CommandType.StoredProcedure : CommandType.Text;
                                Command.CommandText = Database.EnableStoredProcedures ? StoredProcedureName : DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE);

                                using (SqlDataReader DataReader = Command.ExecuteReader())
                                {
                                    AuditLog obj = GetOrdinals(DataReader);
                                    while (DataReader.Read())
                                    {
                                        AllAuditLogs.Add(obj);
                                    }
                                }
                            }

                            Connection.Close();
                        }
                        break;

                    case Database.DatabaseConnectivityMode.Local:
                        using (SqliteConnection Connection = new SqliteConnection(Database.ConnectionString))
                        {
                            Connection.Open();

                            using (SqliteCommand Command = new SqliteCommand())
                            {
                                Command.Connection = Connection;

                                // Stored procedures are not supported in SQLite, so use a query.
                                Command.CommandType = CommandType.Text;
                                Command.CommandText = DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE);

                                using (SqliteDataReader DataReader = Command.ExecuteReader())
                                {
                                    AuditLog obj = GetOrdinals(DataReader);

                                    while (DataReader.Read())
                                    {
                                        AllAuditLogs.Add(obj);
                                    }
                                }
                            }

                            Connection.Close();
                        }
                        break;

                }
            }
            else
            {
                throw new ModularException(ExceptionType.DatabaseConnectionError, "There was an issue trying to connect to the database.");
            }

            return AllAuditLogs;
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

        #region "  Data Methods  "

        protected static AuditLog GetOrdinals(SqlDataReader DataReader)
        {
            AuditLog obj = new AuditLog();
            obj.SetFieldValues(Class.GetFields(), DataReader);
            return obj;
        }

        protected static AuditLog GetOrdinals(SqliteDataReader DataReader)
        {
            AuditLog obj = new AuditLog();
            obj.SetFieldValues(Class.GetFields(), DataReader);
            return obj;
        }

        #endregion

    }
}