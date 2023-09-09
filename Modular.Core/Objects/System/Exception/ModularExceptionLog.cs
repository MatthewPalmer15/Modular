using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Modular.Core.Databases;
using System.Data;
using System.Reflection;

namespace Modular.Core
{
    public class ExceptionLog : ModularBase
    {

        #region "  Constructors  "

        public ExceptionLog()
        {
        }

        #endregion

        #region "  Constants  "

        protected static readonly new string MODULAR_DATABASE_TABLE = "tbl_Modular_ExceptionLog";
        protected static new readonly string MODULAR_DATABASE_STOREDPROCEDURE_PREFIX = "usp_Modular_ExceptionLog";
        protected static new readonly Type MODULAR_OBJECTTYPE = typeof(ExceptionLog);


        #endregion

        #region "  Variables  "

        private ExceptionType _ExceptionType;

        private string _Message = string.Empty;

        private string _StackTrace = string.Empty;

        private string _Source = string.Empty;

        private string _Target = string.Empty;

        private string _DeviceInformation = string.Empty;

        #endregion

        #region "  Properties  "

        public ExceptionType Type
        {
            get
            {
                return _ExceptionType;
            }
            set
            {
                if (_ExceptionType != value)
                {
                    _ExceptionType = value;
                    OnPropertyChanged("ExceptionType");
                }
            }
        }

        public string Message
        {
            get
            {
                return _Message;
            }
            set
            {
                if (_Message != value)
                {
                    _Message = value;
                }
            }
        }

        public string StackTrace
        {
            get
            {
                return _StackTrace;
            }
            set
            {
                if (_StackTrace != value)
                {
                    _StackTrace = value;

                }
            }
        }

        public string Source
        {
            get
            {
                return _Source;
            }
            set
            {
                if (_Source != value)
                {
                    _Source = value;
                }

            }
        }

        public string Target
        {
            get
            {
                return _Target;
            }
            set
            {
                if (_Target != value)
                {
                    _Target = value;
                }
            }
        }

        public string DeviceInformation
        {
            get
            {
                return _DeviceInformation;
            }
            set
            {
                if (_DeviceInformation != value)
                {
                    _DeviceInformation = value;
                }
            }
        }

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <returns></returns>
        public static new ExceptionLog Create()
        {
            ExceptionLog obj = new ExceptionLog();
            obj.SetDefaultValues();
            return obj;
        }


        /// <summary>
        /// Loads an existing instance from the database.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static new ExceptionLog Load(Guid ID)
        {
            ExceptionLog obj = new ExceptionLog();
            obj.Fetch(ID);
            return obj;
        }


        /// <summary>
        /// Loads all instances from the database.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ModularException"></exception>
        public static new List<ExceptionLog> LoadList()
        {
            List<ExceptionLog> AllExceptionLogs = new List<ExceptionLog>();

            // Check if the database can be connected to.
            if (Database.CheckDatabaseConnection())
            {
                FieldInfo[] AllFields = CurrentClass.GetFields();

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
                                DatabaseUtils.CreateStoredProcedure(DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE, AllFields.SingleOrDefault(x => x.Name.Equals("_ID"))));
                            }

                            using (SqlCommand Command = new SqlCommand())
                            {
                                Command.Connection = Connection;
                                Command.CommandType = Database.EnableStoredProcedures ? CommandType.StoredProcedure : CommandType.Text;
                                Command.CommandText = Database.EnableStoredProcedures ? StoredProcedureName : DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE);

                                using (SqlDataReader DataReader = Command.ExecuteReader())
                                {
                                    ExceptionLog obj = GetOrdinals(DataReader);
                                    while (DataReader.Read())
                                    {
                                        AllExceptionLogs.Add(obj);
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
                                    ExceptionLog obj = GetOrdinals(DataReader);

                                    while (DataReader.Read())
                                    {
                                        AllExceptionLogs.Add(obj);
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

            return AllExceptionLogs;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return $"{Message}";
        }

        public override ExceptionLog Clone()
        {
            return ExceptionLog.Load(ID);
        }

        #endregion

        #region "  Data Methods  "

        protected static ExceptionLog GetOrdinals(SqlDataReader DataReader)
        {
            ExceptionLog obj = new ExceptionLog();
            obj.SetFieldValues(CurrentClass.GetFields(), DataReader);
            return obj;
        }

        protected static ExceptionLog GetOrdinals(SqliteDataReader DataReader)
        {
            ExceptionLog obj = new ExceptionLog();
            obj.SetFieldValues(CurrentClass.GetFields(), DataReader);
            return obj;
        }

        #endregion

    }
}
