using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Modular.Core.Databases;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;

namespace Modular.Core.Entity
{
    [Serializable]
    public partial class Department : ModularBase
    {
        #region "  Constructors  "

        public Department()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Department";
        protected static new readonly string MODULAR_DATABASE_STOREDPROCEDURE_PREFIX = "usp_Modular_Department";
        protected static new readonly Type MODULAR_OBJECTTYPE = typeof(Department);

        #endregion

        #region "  Variables  "

        private string _Name = string.Empty;

        #endregion

        #region "  Properties  "

        [Display(Name = "Name")]
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
        /// Creates a new instance.
        /// </summary>
        /// <returns>A new instance</returns>
        public static new Department Create()
        {
            Department obj = new Department();
            obj.SetDefaultValues();
            return obj;
        }


        /// <summary>
        /// Loads an instance from the database.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static new Department Load(Guid ID)
        {
            Department obj = new Department();
            obj.Fetch(ID);
            return obj;
        }


        /// <summary>
        /// Loads all instances from the database.
        /// </summary>
        /// <returns></returns>
        public static new List<Department> LoadList()
        {
            List<Department> AllDepartments = new List<Department>();

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
                                    Department obj = GetOrdinals(DataReader);
                                    while (DataReader.Read())
                                    {
                                        AllDepartments.Add(obj);
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
                                    Department obj = GetOrdinals(DataReader);

                                    while (DataReader.Read())
                                    {
                                        AllDepartments.Add(obj);
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

            return AllDepartments;

        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Name;
        }

        public override Department Clone()
        {
            return Department.Load(ID);
        }

        #endregion

        #region "  Data Methods  "

        protected static Department GetOrdinals(SqlDataReader DataReader)
        {
            Department obj = new Department();
            obj.SetFieldValues(CurrentClass.GetFields(), DataReader);
            return obj;
        }

        protected static Department GetOrdinals(SqliteDataReader DataReader)
        {
            Department obj = new Department();
            obj.SetFieldValues(CurrentClass.GetFields(), DataReader);
            return obj;
        }

        #endregion

    }
}
