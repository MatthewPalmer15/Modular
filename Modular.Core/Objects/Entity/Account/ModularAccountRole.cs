using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Reflection;

namespace Modular.Core
{
    public class Role : ModularBase
    {

        #region "  Constructors  "

        public Role()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Role";

        #endregion

        #region "  Variables  "

        private string _Name = string.Empty;

        private List<RolePermission> _Permissions = new List<RolePermission>();

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

        public List<RolePermission> Permissions
        {
            get
            {
                LoadPermissions();
                return _Permissions;
            }
        }

        #endregion

        #region "  Static Methods  "

        private void LoadPermissions()
        {
            _Permissions = RolePermission.LoadAll().Where(Permission => Permission.RoleID == ID).ToList();
        }

        public static new Role Create()
        {
            Role obj = new Role();
            obj.SetDefaultValues();
            return obj;
        }

        public static new List<Role> LoadAll()
        {
            return FetchAll();
        }

        public static new Role Load(Guid ID)
        {
            Role obj = new Role();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Name.ToString();
        }

        #endregion

        #region "  Data Methods  "

        /// <summary>
        /// Fetches all the contacts from the database
        /// </summary>
        /// <returns></returns>
        protected static List<Role> FetchAll()
        {
            List<Role> AllObjects = new List<Role>();

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
                                        Role obj = GetOrdinals(DataReader);

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
                                            Role obj = GetOrdinals(DataReader);

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

        protected static Role GetOrdinals(SqlDataReader DataReader)
        {
            Role obj = new Role();

            PropertyInfo[] AllProperties = GetProperties();
            if (AllProperties != null)
            {
                obj.SetPropertyValues(AllProperties, DataReader);
            }

            return obj;
        }

        protected static Role GetOrdinals(SqliteDataReader DataReader)
        {
            Role obj = new Role();

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
