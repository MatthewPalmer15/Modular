using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;
using Modular.Core.Databases;
using Modular.Core.System.Attributes;

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

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Account";

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
            obj.Fetch(GetProperty("Key"), Key);
            return obj.Value;
        }

        public static new SystemConfig Create()
        {
            SystemConfig obj = new SystemConfig();
            obj.SetDefaultValues();
            return obj;
        }

        public static new List<SystemConfig> LoadAll()
        {
            return new List<SystemConfig> { new SystemConfig() };
        }

        public static new SystemConfig Load(Guid ID)
        {
            SystemConfig obj = new SystemConfig();
            obj.Fetch(ID);
            return obj;
        }

        public static SystemConfig Load(string Key)
        {
            SystemConfig obj = new SystemConfig();
            obj.Fetch(GetProperty("Key"), Key);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Key;
        }

        #endregion

        #region "  Data Methods  "

        /// <summary>
        /// Fetches all the contacts from the database
        /// </summary>
        /// <returns></returns>
        protected static List<SystemConfig> FetchAll()
        {
            List<SystemConfig> AllObjects = new List<SystemConfig>();

            if (Database.CheckDatabaseConnection())
            {
                Database.DatabaseConnectivityMode DatabaseConnectionMode = Database.ConnectionMode;
                PropertyInfo[] AllProperties = GetProperties();
                if (AllProperties != null)
                {
                    switch (DatabaseConnectionMode)
                    {

                        case Database.DatabaseConnectivityMode.Remote:
                            using (SqlConnection Connection = new SqlConnection(Database.ConnectionString))
                            {
                                Connection.Open();

                                if (Database.CheckDatabaseTableExists(MODULAR_DATABASE_TABLE))
                                {
                                    DatabaseUtils.CreateDatabaseTable(MODULAR_DATABASE_TABLE, AllProperties);
                                }

                                using (SqlCommand Command = new SqlCommand())
                                {
                                    Command.Connection = Connection;
                                    Command.CommandType = CommandType.Text;
                                    Command.CommandText = DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE);

                                    using (SqlDataReader DataReader = Command.ExecuteReader())
                                    {
                                        SystemConfig obj = GetOrdinals(DataReader);

                                        while (DataReader.Read())
                                        {
                                            AllObjects.Add(obj);
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

                                if (Database.CheckDatabaseTableExists(MODULAR_DATABASE_TABLE))
                                {
                                    using (SqliteCommand Command = new SqliteCommand())
                                    {

                                        Command.Connection = Connection;
                                        Command.CommandType = CommandType.Text;
                                        Command.CommandText = DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE);

                                        using (SqliteDataReader DataReader = Command.ExecuteReader())
                                        {
                                            SystemConfig obj = GetOrdinals(DataReader);

                                            while (DataReader.Read())
                                            {
                                                AllObjects.Add(obj);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    DatabaseUtils.CreateDatabaseTable(MODULAR_DATABASE_TABLE, AllProperties);
                                }
                                Connection.Close();
                            }
                            break;
                    }
                }
            }
            else
            {
                throw new ModularException(ExceptionType.DatabaseConnectionError, "There was an issue trying to connect to the database.");
            }

            return AllObjects;
        }

        protected static SystemConfig GetOrdinals(SqlDataReader DataReader)
        {
            SystemConfig obj = new SystemConfig();

            PropertyInfo[] AllProperties = GetProperties();
            if (AllProperties != null)
            {
                obj.SetPropertyValues(AllProperties, DataReader);
            }

            return obj;
        }

        protected static SystemConfig GetOrdinals(SqliteDataReader DataReader)
        {
            SystemConfig obj = new SystemConfig();

            PropertyInfo[] AllProperties = GetProperties();
            if (AllProperties != null)
            {
                obj.SetPropertyValues(AllProperties, DataReader);
            }

            return obj;
        }

        #endregion
    }
}