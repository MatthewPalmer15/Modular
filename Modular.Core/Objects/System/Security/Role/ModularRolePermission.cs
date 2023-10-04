using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Modular.Core.Databases;
using System.Data;
using System.Reflection;

namespace Modular.Core.Security
{
    [Serializable]
    public class RolePermission : ModularBase
    {

        // Could do this? (int)Security.Document.View

        #region "  Constructors  "

        public RolePermission()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_RolePermission";
        protected static new readonly string MODULAR_DATABASE_STOREDPROCEDURE_PREFIX = "usp_Modular_RolePermission";
        protected static new readonly Type MODULAR_OBJECTTYPE = typeof(RolePermission);

        #endregion

        #region "  Variables  "

        private Guid _RoleID;

        private Enum _Permission;

        #endregion

        #region "  Properties  "

        public Role Role
        {
            get
            {
                return Role.Load(_RoleID);
            }
            set
            {
                if (_RoleID != value.ID)
                {
                    _RoleID = value.ID;
                    OnPropertyChanged("AccountID");
                }
            }
        }

        public Enum Permission
        {
            get
            {
                return _Permission;
            }
            set
            {
                if (_Permission != value)
                {
                    // Need to check to see if Permission actually exists
                    _Permission = value;
                    OnPropertyChanged("Permission");
                }
            }
        }

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <returns></returns>
        public static new RolePermission Create()
        {
            RolePermission obj = new RolePermission();
            obj.SetDefaultValues();
            return obj;
        }


        /// <summary>
        /// Loads an instance from the database.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static new RolePermission Load(Guid ID)
        {
            RolePermission obj = new RolePermission();
            obj.Fetch(ID);
            return obj;
        }


        /// <summary>
        /// Loads all instances from the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ModularException"></exception>
        public static new List<RolePermission> LoadList()
        {
            List<RolePermission> AllRolePermissions = new List<RolePermission>();

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
                                    RolePermission obj = GetOrdinals(DataReader);
                                    while (DataReader.Read())
                                    {
                                        AllRolePermissions.Add(obj);
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
                                    RolePermission obj = GetOrdinals(DataReader);

                                    while (DataReader.Read())
                                    {
                                        AllRolePermissions.Add(obj);
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

            return AllRolePermissions;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Permission.ToString();
        }

        public override RolePermission Clone()
        {
            return RolePermission.Load(ID);
        }

        #endregion

        #region "  Data Methods  "

        protected static RolePermission GetOrdinals(SqlDataReader DataReader)
        {
            RolePermission obj = new RolePermission();
            obj.SetFieldValues(Class.GetFields(), DataReader);
            return obj;
        }

        protected static RolePermission GetOrdinals(SqliteDataReader DataReader)
        {
            RolePermission obj = new RolePermission();
            obj.SetFieldValues(Class.GetFields(), DataReader);
            return obj;
        }

        #endregion

    }
}