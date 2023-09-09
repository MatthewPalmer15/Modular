using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Reflection;
using Modular.Core.Databases;

namespace Modular.Core.Documents
{
    public class Document : ModularBase
    {

        #region "  Constructors  "

        public Document()
        {
        }

        #endregion

        #region "  Constants  "

        protected static readonly new string MODULAR_DATABASE_TABLE = "tbl_Modular_Document";
        protected static new readonly string MODULAR_DATABASE_STOREDPROCEDURE_PREFIX = "usp_Modular_Document";
        protected static new readonly Type MODULAR_OBJECTTYPE = typeof(Document);

        #endregion

        #region "  Variables  "

        private Guid _DocumentPackID;

        private string _FileName = string.Empty;

        private string _FileExtension = string.Empty;

        private byte[] _FileData;

        private int _Version;

        private DateTime _ValidFrom;

        private DateTime _ValidTo;

        private bool _IsStatic;

        #endregion

        #region "  Properties  "

        public DocumentPack DocumentPack
        {
            get
            {
                return DocumentPack.Load(_DocumentPackID);
            }
            set
            {
                if (_DocumentPackID != value.ID)
                {
                    _DocumentPackID = value.ID;
                    OnPropertyChanged("DocumentPackID");
                }
            }
        }

        public string Filename
        {
            get
            {
                return _FileName;
            }
            set
            {
                if (_FileName != value)
                {
                    _FileName = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public string FileExtension
        {
            get
            {
                return _FileExtension;
            }
            set
            {
                if (_FileExtension != value)
                {
                    _FileExtension = value;
                    OnPropertyChanged("FileExtension");
                }
            }
        }

        public string FullFilename
        {
            get
            {
                return $"{Filename}.{FileExtension}";
            }
        }

        public byte[] FileData
        {
            get
            {
                return _FileData;
            }
            set
            {
                if (_FileData != value)
                {
                    _FileData = value;
                    OnPropertyChanged("FileData");
                }
            }
        }

        public int Version
        {
            get
            {
                return _Version;
            }
            set
            {
                if (_Version != value)
                {
                    _Version = value;
                    OnPropertyChanged("Version");
                }
            }
        }

        public DateTime ValidFrom
        {
            get
            {
                return _ValidFrom;
            }
            set
            {
                if (_ValidFrom != value)
                {
                    _ValidFrom = value;
                    OnPropertyChanged("ValidFrom");
                }
            }
        }

        public DateTime ValidTo
        {
            get
            {
                return _ValidTo;
            }
            set
            {
                if (_ValidTo != value)
                {
                    _ValidTo = value;
                    OnPropertyChanged("ValidTo");
                }
            }
        }

        public bool IsStatic
        {
            get
            {
                return _IsStatic;
            }
            set
            {
                if (_IsStatic != value)
                {
                    _IsStatic = value;
                    OnPropertyChanged("IsStatic");
                }
            }
        }

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <returns></returns>
        public static new Document Create()
        {
            Document obj = new Document();
            obj.SetDefaultValues();
            return obj;
        }

        
        /// <summary>
        /// Loads an existing instance from the database.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static new Document Load(Guid ID)
        {
            Document obj = new Document();
            obj.Fetch(ID);
            return obj;
        }

        /// <summary>
        /// Loads all instances from the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ModularException"></exception>
        public static new List<Document> LoadList()
        {
            List<Document> AllDocuments = new List<Document>();

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
                                    Document obj = GetOrdinals(DataReader);
                                    while (DataReader.Read())
                                    {
                                        AllDocuments.Add(obj);
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
                                    Document obj = GetOrdinals(DataReader);

                                    while (DataReader.Read())
                                    {
                                        AllDocuments.Add(obj);
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

            return AllDocuments;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return FullFilename;
        }

        #endregion

        #region "  Data Methods  "

        protected static Document GetOrdinals(SqlDataReader DataReader)
        {
            Document obj = new Document();
            obj.SetFieldValues(CurrentClass.GetFields(), DataReader);
            return obj;
        }

        protected static Document GetOrdinals(SqliteDataReader DataReader)
        {
            Document obj = new Document();
            obj.SetFieldValues(CurrentClass.GetFields(), DataReader);
            return obj;
        }


        #endregion

    }
}
