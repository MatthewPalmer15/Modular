using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Modular.Core.Databases;
using System.Data;
using System.Reflection;

namespace Modular.Core.Entity
{
    [Serializable]
    public class AccountProfile : ModularBase
    {

        #region "  Constructors  "

        public AccountProfile()
        {
        }

        #endregion

        #region "  Constants  "

        protected static readonly new string MODULAR_DATABASE_TABLE = "tbl_Modular_AccountProfile";
        protected static new readonly string MODULAR_DATABASE_STOREDPROCEDURE_PREFIX = "usp_Modular_AccountProfile";
        protected static new readonly Type MODULAR_OBJECTTYPE = typeof(AccountProfile);

        #endregion

        #region "  Variables  "

        private Guid _AccountID;

        private string _Name = string.Empty;

        private string _Bio = string.Empty;

        private string _Colour = string.Empty;

        private byte[] _ProfilePicture;

        private byte[] _BackgroundPicture;

        #endregion

        #region "  Properties  "

        public Account Account
        {
            get
            {
                return Account.Load(_AccountID);
            }
            set
            {
                if (_AccountID != value.ID)
                {
                    _AccountID = value.ID;
                    OnPropertyChanged("AccountID");
                }
            }
        }

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

        public string Bio
        {
            get
            {
                return _Bio;
            }
            set
            {
                if (_Bio != value)
                {
                    _Bio = value;
                    OnPropertyChanged("Bio");
                }
            }
        }

        public string Colour
        {
            get
            {
                return _Colour;
            }
            set
            {
                if (_Colour != value)
                {
                    _Colour = value;
                    OnPropertyChanged("Colour");
                }
            }
        }

        public byte[] ProfilePicture
        {
            get
            {
                return _ProfilePicture;
            }
            set
            {
                if (_ProfilePicture != value)
                {
                    _ProfilePicture = value;
                    OnPropertyChanged("ProfilePicture");
                }
            }
        }

        public byte[] BackgroundPicture
        {
            get
            {
                return _BackgroundPicture;
            }
            set
            {
                if (_BackgroundPicture != value)
                {
                    _BackgroundPicture = value;
                    OnPropertyChanged("BackgroundPicture");
                }
            }
        }

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <returns></returns>
        public static new AccountProfile Create()
        {
            AccountProfile obj = new AccountProfile();
            obj.SetDefaultValues();
            return obj;
        }


        /// <summary>
        /// Load an existing instance from the database.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static new AccountProfile Load(Guid ID)
        {
            AccountProfile obj = new AccountProfile();
            obj.Fetch(ID);
            return obj;
        }


        /// <summary>
        /// Loads all instances from the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ModularException"></exception>
        public static new List<AccountProfile> LoadList()
        {
            List<AccountProfile> AllAccountProfiles = new List<AccountProfile>();

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
                                    AccountProfile obj = GetOrdinals(DataReader);
                                    while (DataReader.Read())
                                    {
                                        AllAccountProfiles.Add(obj);
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
                                    AccountProfile obj = GetOrdinals(DataReader);

                                    while (DataReader.Read())
                                    {
                                        AllAccountProfiles.Add(obj);
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

            return AllAccountProfiles;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Name;
        }

        public override AccountProfile Clone()
        {
            return AccountProfile.Load(ID);
        }

        #endregion

        #region "  Data Methods  "

        protected static AccountProfile GetOrdinals(SqlDataReader DataReader)
        {
            AccountProfile obj = new AccountProfile();
            obj.SetFieldValues(CurrentClass.GetFields(), DataReader);
            return obj;
        }

        protected static AccountProfile GetOrdinals(SqliteDataReader DataReader)
        {
            AccountProfile obj = new AccountProfile();
            obj.SetFieldValues(CurrentClass.GetFields(), DataReader);
            return obj;
        }

        #endregion

    }
}
