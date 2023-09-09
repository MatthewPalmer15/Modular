using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Reflection;
using Modular.Core.Databases;
using System.ComponentModel.DataAnnotations;

namespace Modular.Core.Entity
{
    public class Industry : ModularBase
    {

        #region "  Constructors  "

        public Industry()
        {

        }

        #endregion

        #region "  Constants  "

        protected readonly static new string MODULAR_DATABASE_TABLE = "tbl_Modular_Industry";
        protected static new readonly string MODULAR_DATABASE_STOREDPROCEDURE_PREFIX = "usp_Modular_Industry";
        protected static new readonly Type MODULAR_OBJECTTYPE = typeof(Industry);

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
        /// <returns></returns>
        public static new Industry Create()
        {
            Industry objIndustry = new Industry();
            objIndustry.SetDefaultValues();
            return objIndustry;
        }


        /// <summary>
        /// Loads an existing instance from the database.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static new Industry Load(Guid ID)
        {
            Industry obj = new Industry();
            obj.Fetch(ID);
            return obj;
        }


        /// <summary>
        /// Loads all instances from the database.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ModularException"></exception>
        public static new List<Industry> LoadList()
        {
            List<Industry> AllIndustries = new List<Industry>();

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
                                    Industry obj = GetOrdinals(DataReader);
                                    while (DataReader.Read())
                                    {
                                        AllIndustries.Add(obj);
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
                                    Industry obj = GetOrdinals(DataReader);

                                    while (DataReader.Read())
                                    {
                                        AllIndustries.Add(obj);
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

            return AllIndustries;

        }


        #endregion

        #region "  Instance Methods  "

        /// <summary>
        /// Returns a string representation of the object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }


        /// <summary>
        /// Returns a clone of the object.
        /// </summary>
        /// <returns></returns>
        public override Industry Clone()
        {
            return Industry.Load(ID);
        }

        #endregion

        #region "  Data Methods  "

        protected static Industry GetOrdinals(SqlDataReader DataReader)
        {
            Industry obj = new Industry();
            obj.SetFieldValues(CurrentClass.GetFields(), DataReader);
            return obj;
        }

        protected static Industry GetOrdinals(SqliteDataReader DataReader)
        {
            Industry obj = new Industry();
            obj.SetFieldValues(CurrentClass.GetFields(), DataReader);
            return obj;
        }

        #endregion

    }
}
