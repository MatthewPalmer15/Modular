using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Modular.Core.Databases;
using System.Data;
using System.Reflection;

namespace Modular.Core.Geo
{
    [Serializable]
    public class Region : ModularBase
    {

        #region "  Constructor  "

        public Region()
        {
        }

        #endregion

        #region "  Constants  "

        protected static readonly new string MODULAR_DATABASE_TABLE = "tbl_Modular_Region";
        protected static readonly new string MODULAR_DATABASE_STOREDPROCEDURE_PREFIX = "usp_Modular_Region";
        protected static readonly new Type MODULAR_OBJECTTYPE = typeof(Region);

        #endregion

        #region "  Variables  "

        private string _Name = string.Empty;

        private List<Country> _Countries = new List<Country>();

        private DateTime _LastRetrievedCountries = DateTime.MinValue;

        #endregion

        #region "  Properties  "

        public string Name
        {
            get
            {
                return _Name;
            }
        }

        public List<Country> Countries
        {
            get
            {
                if (_Countries.Count == 0 || _LastRetrievedCountries.AddMinutes(5) < DateTime.Now)
                {
                    _Countries = Country.LoadList().Where(Country => Country.Region.ID == ID).ToList();
                    _LastRetrievedCountries = DateTime.Now;
                }
                return _Countries;
            }
        }

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Loads an existing instance from the database.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static new Region Load(Guid ID)
        {
            Region obj = new Region();
            obj.Fetch(ID);
            return obj;
        }

        /// <summary>
        /// Loads all instances from the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ModularException"></exception>
        public static new List<Region> LoadList()
        {
            List<Region> AllRegions = new List<Region>();

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
                                    Region obj = GetOrdinals(DataReader);
                                    while (DataReader.Read())
                                    {
                                        AllRegions.Add(obj);
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
                                    Region obj = GetOrdinals(DataReader);

                                    while (DataReader.Read())
                                    {
                                        AllRegions.Add(obj);
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

            return AllRegions;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Name;
        }

        #endregion

        #region "  Data Methods  "

        protected static Region GetOrdinals(SqlDataReader DataReader)
        {
            Region obj = new Region();
            obj.SetFieldValues(CurrentClass.GetFields(), DataReader);
            return obj;
        }

        protected static Region GetOrdinals(SqliteDataReader DataReader)
        {
            Region obj = new Region();
            obj.SetFieldValues(CurrentClass.GetFields(), DataReader);
            return obj;
        }

        #endregion

    }
}
