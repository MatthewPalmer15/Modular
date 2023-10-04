using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Modular.Core.Databases;
using Modular.Core.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;

namespace Modular.Core.Geo
{
    public partial class Country : ModularBase
    {

        #region "  Constructors  "

        public Country()
        {
        }

        #endregion

        #region "  Constants  "

        protected static readonly new string MODULAR_DATABASE_TABLE = "tbl_Modular_Country";
        protected static new readonly string MODULAR_DATABASE_STOREDPROCEDURE_PREFIX = "usp_Modular_Country";
        protected static new readonly Type MODULAR_OBJECTTYPE = typeof(Country);

        #endregion

        #region "  Variables  "

        private string _Name = string.Empty;

        private string _Code = string.Empty;

        private Guid _ContinentID;

        #endregion

        #region "  Properties  "

        [Display(Name = "Name")]
        public string Name
        {
            get
            {
                return _Name;
            }
        }


        [Display(Name = "Code")]
        public string Code
        {
            get
            {
                return _Code;
            }
        }


        [Display(Name = "Continent")]
        public Continent Continent
        {
            get
            {
                return Continent.Load(_ContinentID);
            }
        }

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Loads an existing instance from the database.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static new Country Load(Guid ID)
        {
            Country obj = new Country();
            obj.Fetch(ID);
            return obj;
        }


        /// <summary>
        /// Loads an existing instance from the database.
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static Country Load(string Code)
        {
            Country obj = new Country();
            obj.Fetch(Code);
            return obj;
        }


        /// <summary>
        /// Loads all instances from the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ModularException"></exception>
        public static new List<Country> LoadList()
        {
            List<Country> AllCountries = new List<Country>();

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
                                    Country obj = GetOrdinals(DataReader);
                                    while (DataReader.Read())
                                    {
                                        AllCountries.Add(obj);
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
                                    Country obj = GetOrdinals(DataReader);

                                    while (DataReader.Read())
                                    {
                                        AllCountries.Add(obj);
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

            return AllCountries;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Name;
        }

        #endregion

        #region "  Data Methods  "

        public void Fetch(string Code)
        {
            // Check if the database can be connected to.
            if (Database.CheckDatabaseConnection())
            {
                FieldInfo[] AllFields = GetType().GetFields(BindingFlags.Instance).Where(Field => !Field.IsDefined(typeof(IgnoreAttribute), false)).ToArray();

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
                            string StoredProcedureName = $"{MODULAR_DATABASE_STOREDPROCEDURE_PREFIX}_FetchByCode";

                            // If stored procedures are enabled, and the stored procedure does not exist, create it.
                            if (Database.EnableStoredProcedures && !Database.CheckStoredProcedureExists(StoredProcedureName))
                            {
                                DatabaseUtils.CreateStoredProcedure(DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE, AllFields.SingleOrDefault(x => x.Name.Equals("_Code"))), StoredProcedureName);
                            }

                            using (SqlCommand Command = new SqlCommand())
                            {
                                Command.Connection = Connection;

                                // If stored procedures are enabled, use the stored procedure, otherwise use a text query.
                                Command.CommandType = Database.EnableStoredProcedures ? CommandType.StoredProcedure : CommandType.Text;
                                Command.CommandText = Database.EnableStoredProcedures ? StoredProcedureName : DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE, AllFields.SingleOrDefault(x => x.Name.Equals("_Code")));

                                Command.Parameters.Add(new SqlParameter($"@Code", Code));

                                using (SqlDataReader DataReader = Command.ExecuteReader())
                                {
                                    // If data was returned, set the field values.
                                    if (DataReader.HasRows)
                                    {
                                        DataReader.Read();
                                        SetFieldValues(AllFields, DataReader);
                                    }
                                }
                            }

                            Connection.Close();
                        }
                        break;

                    // If the database is a local database, connect to it.
                    case Database.DatabaseConnectivityMode.Local:
                        using (SqliteConnection Connection = new SqliteConnection(Database.ConnectionString))
                        {
                            Connection.Open();

                            using (SqliteCommand Command = new SqliteCommand())
                            {
                                Command.Connection = Connection;

                                // Stored procedures are not supported in SQLite, so use a query.
                                Command.CommandType = CommandType.Text;
                                Command.CommandText = DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE, AllFields.SingleOrDefault(x => x.Name.Equals("_ID")));

                                Command.Parameters.Add(new SqliteParameter($"@ID", ID));

                                using (SqliteDataReader DataReader = Command.ExecuteReader())
                                {
                                    // If data was returned, set the field values.
                                    if (DataReader.HasRows)
                                    {
                                        DataReader.Read();
                                        SetFieldValues(AllFields, DataReader);
                                    }
                                }
                            }

                            Connection.Close();
                        }
                        break;

                    // If the database connection mode was not defined, throw an exception.
                    default:
                        throw new ModularException(ExceptionType.DatabaseConnectivityNotDefined, "Database Connection Mode was not defined.");
                }
            }
            else
            {
                throw new ModularException(ExceptionType.DatabaseConnectionError, $"There was an issue trying to connect to the database.");
            }
        }

        protected static Country GetOrdinals(SqlDataReader DataReader)
        {
            Country obj = new Country();
            obj.SetFieldValues(Class.GetFields(), DataReader);
            return obj;
        }

        protected static Country GetOrdinals(SqliteDataReader DataReader)
        {
            Country obj = new Country();
            obj.SetFieldValues(Class.GetFields(), DataReader);
            return obj;
        }

        #endregion

    }
}