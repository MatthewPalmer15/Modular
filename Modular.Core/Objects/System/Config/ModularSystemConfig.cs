using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Modular.Core.Databases;
using Modular.Core.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;

namespace Modular.Core.Configuration
{
    public class SystemConfig : ModularBase
    {

        #region "  Constructors  "

        public SystemConfig()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_SystemConfig";
        protected static new readonly string MODULAR_DATABASE_STOREDPROCEDURE_PREFIX = "usp_Modular_SystemConfig";
        protected static new readonly Type MODULAR_OBJECTTYPE = typeof(SystemConfig);

        #endregion

        #region "  Variables  "

        private string _Key = string.Empty;

        private string _Value = string.Empty;

        #endregion

        #region "  Properties  "

        [Unique]
        [Required]
        public string Key
        {
            get
            {
                return _Key;
            }
            set
            {
                if (_Key != value)
                {
                    _Key = value;
                    OnPropertyChanged("Key");
                }
            }
        }

        [Required]
        public string Value
        {
            get
            {
                return _Value;
            }
            set
            {
                if (_Value != value)
                {
                    _Value = value;
                    OnPropertyChanged("Value");
                }
            }
        }

        #endregion

        #region "  Static Methods  "

        public static string GetValue(string Key)
        {
            SystemConfig obj = new SystemConfig();
            obj.Fetch(CurrentClass.GetField("Key"), Key);
            return obj.Value;
        }


        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <returns></returns>
        public static new SystemConfig Create()
        {
            SystemConfig obj = new SystemConfig();
            obj.SetDefaultValues();
            return obj;
        }


        /// <summary>
        /// Loads an existing instance from the database.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static new SystemConfig Load(Guid ID)
        {
            SystemConfig obj = new SystemConfig();
            obj.Fetch(ID);
            return obj;
        }


        /// <summary>
        /// Loads an existing instance from the database.
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static SystemConfig Load(string Key)
        {
            SystemConfig obj = new SystemConfig();
            obj.Fetch(CurrentClass.GetField("Key"), Key);
            return obj;
        }


        /// <summary>
        /// Loads all instances from the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ModularException"></exception>
        public static new List<SystemConfig> LoadList()
        {
            List<SystemConfig> AllSystemConfigs = new List<SystemConfig>();

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
                                DatabaseUtils.CreateStoredProcedure(DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE, AllFields.SingleOrDefault(x => x.Name.Equals("_ID"))), StoredProcedureName);
                            }

                            using (SqlCommand Command = new SqlCommand())
                            {
                                Command.Connection = Connection;
                                Command.CommandType = Database.EnableStoredProcedures ? CommandType.StoredProcedure : CommandType.Text;
                                Command.CommandText = Database.EnableStoredProcedures ? StoredProcedureName : DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE);

                                using (SqlDataReader DataReader = Command.ExecuteReader())
                                {
                                    SystemConfig obj = GetOrdinals(DataReader);
                                    while (DataReader.Read())
                                    {
                                        AllSystemConfigs.Add(obj);
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
                                    SystemConfig obj = GetOrdinals(DataReader);

                                    while (DataReader.Read())
                                    {
                                        AllSystemConfigs.Add(obj);
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

            return AllSystemConfigs;
        }


        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Key;
        }

        public override SystemConfig Clone()
        {
            return SystemConfig.Load(ID);
        }

        #endregion

        #region "  Data Methods  "

        protected static SystemConfig GetOrdinals(SqlDataReader DataReader)
        {
            SystemConfig obj = new SystemConfig();
            obj.SetFieldValues(CurrentClass.GetFields(), DataReader);
            return obj;
        }

        protected static SystemConfig GetOrdinals(SqliteDataReader DataReader)
        {
            SystemConfig obj = new SystemConfig();
            obj.SetFieldValues(CurrentClass.GetFields(), DataReader);
            return obj;
        }

        #endregion

    }
}