using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Reflection;

namespace Modular.Core
{
    public class Department : ModularBase
    {
        #region "  Constructors  "

        public Department()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Contact";

        #endregion

        #region "  Variables  "

        private string _Name = string.Empty;

        #endregion

        #region "  Properties  "

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

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Creates a new instance with default values
        /// </summary>
        /// <returns>A new instance</returns>
        public static new Department Create()
        {
            Department obj = new Department();
            obj.SetDefaultValues();
            return obj;
        }

        public static new List<Department> LoadInstances()
        {
            return FetchAll();
        }

        public static new Department Load(Guid ID)
        {
            Department obj = new Department();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Name;
        }

        #endregion

        #region "  Data Methods  "

        /// <summary>
        /// Fetches all the contacts from the database
        /// </summary>
        /// <returns></returns>
        protected static List<Department> FetchAll()
        {
            List<Department> AllObjects = new List<Department>();

            if (Database.CheckDatabaseConnection())
            {
                Database.DatabaseConnectivityMode DatabaseConnectionMode = Database.ConnectionMode;
                PropertyInfo[]? AllProperties = GetProperties();
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
                                        Department obj = GetOrdinals(DataReader);

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
                                            Department obj = GetOrdinals(DataReader);

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

        protected static Department GetOrdinals(SqlDataReader DataReader)
        {
            Department obj = new Department();

            PropertyInfo[]? AllProperties = GetProperties();
            if (AllProperties != null)
            {
                obj.SetPropertyValues(AllProperties, DataReader);
            }

            return obj;
        }

        protected static Department GetOrdinals(SqliteDataReader DataReader)
        {
            Department obj = new Department();

            PropertyInfo[]? AllProperties = GetProperties();
            if (AllProperties != null)
            {
                obj.SetPropertyValues(AllProperties, DataReader);
            }

            return obj;
        }

        #endregion

    }
}
