using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Modular.Core.Databases;
using Modular.Core.Entity;
using Modular.Core.Attributes;
using System.Data;
using System.Reflection;

namespace Modular.Core.Security
{
    [Serializable]
    public class Role : ModularBase
    {

        #region "  Constructors  "

        public Role()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Role";
        protected static new readonly string MODULAR_DATABASE_STOREDPROCEDURE_PREFIX = "usp_Modular_Role";
        protected static new readonly Type MODULAR_OBJECTTYPE = typeof(Role);

        #endregion

        #region "  Variables  "

        private string _Name = string.Empty;

        [Ignore]
        private List<RolePermission> _Permissions = new List<RolePermission>();

        [Ignore]
        private DateTime _LastRetrievedPermissions = DateTime.MinValue;

        [Ignore]
        private List<Account> _AccountsInRole = new List<Account>();

        [Ignore]
        private DateTime _LastRetrievedAccounts = DateTime.MinValue;

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
                if (_Permissions.Count == 0 || _LastRetrievedPermissions.AddMinutes(5) > DateTime.Now)
                {
                    _Permissions = RolePermission.LoadList().Where(Permission => Permission.Role.ID == ID).ToList();
                    _LastRetrievedPermissions = DateTime.Now;
                }
                return _Permissions;
            }
        }

        public List<Account> Users
        {
            get
            {
                if (_AccountsInRole.Count == 0 || _LastRetrievedAccounts.AddMinutes(5) > DateTime.Now)
                {
                    _AccountsInRole = Account.LoadList().Where(Account => Account.Role.ID == ID).ToList();
                    _LastRetrievedAccounts = DateTime.Now;
                }
                return _AccountsInRole;
            }
        }

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <returns></returns>
        public static new Role Create()
        {
            Role obj = new Role();
            obj.SetDefaultValues();
            return obj;
        }

        /// <summary>
        /// Load an existing instance from the database.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static new Role Load(Guid ID)
        {
            Role obj = new Role();
            obj.Fetch(ID);
            return obj;
        }

        /// <summary>
        /// Loads all instances from the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ModularException"></exception>
        public static new List<Role> LoadList()
        {
            List<Role> AllRoles = new List<Role>();

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
                                    Role obj = GetOrdinals(DataReader);
                                    while (DataReader.Read())
                                    {
                                        AllRoles.Add(obj);
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
                                    Role obj = GetOrdinals(DataReader);

                                    while (DataReader.Read())
                                    {
                                        AllRoles.Add(obj);
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

            return AllRoles;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Name.ToString();
        }

        public override Role Clone()
        {
            return Role.Load(ID);
        }

        #endregion

        #region "  Data Methods  "

        protected static Role GetOrdinals(SqlDataReader DataReader)
        {
            Role obj = new Role();
            obj.SetFieldValues(Class.GetFields(), DataReader);
            return obj;
        }

        protected static Role GetOrdinals(SqliteDataReader DataReader)
        {
            Role obj = new Role();
            obj.SetFieldValues(Class.GetFields(), DataReader);
            return obj;
        }

        #endregion

    }
}
