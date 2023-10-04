using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Modular.Core.Databases;
using Modular.Core.Attributes;
using Modular.Core.Utility;
using System.Data;
using System.Reflection;

namespace Modular.Core.Documents
{
    public class DocumentPack : ModularBase
    {

        #region "  Constructors  "

        public DocumentPack()
        {
        }

        #endregion

        #region "  Constants  "

        protected static readonly new string MODULAR_DATABASE_TABLE = "tbl_Modular_DocumentPack";
        protected static new readonly string MODULAR_DATABASE_STOREDPROCEDURE_PREFIX = "usp_Modular_DocumentPack";
        protected static new readonly Type MODULAR_OBJECTTYPE = typeof(DocumentPack);

        #endregion

        #region "  Variables  "

        private Guid _ContactID;

        private string _Name = string.Empty;

        [Ignore]
        private List<Document> _Documents = new List<Document>();

        [Ignore]
        private DateTime _LastRetrievedDocuments = DateTime.MinValue;

        private EnumUtils.StatusType _Status;

        #endregion

        #region "  Properties  "

        public Entity.Contact Contact
        {
            get
            {
                return Entity.Contact.Load(_ContactID);
            }
            set
            {
                if (_ContactID != value.ID)
                {
                    _ContactID = value.ID;
                    OnPropertyChanged("ContactID");
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

        public EnumUtils.StatusType Status
        {
            get
            {
                return _Status;
            }
            set
            {
                if (_Status != value)
                {
                    _Status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        public List<Document> Documents
        {
            get
            {
                if (_Documents.Count == 0 || _LastRetrievedDocuments.AddMinutes(5) < DateTime.Now)
                {
                    _Documents = Document.LoadList().Where(x => x.DocumentPack.ID == ID).ToList();
                    _LastRetrievedDocuments = DateTime.Now;
                }
                return _Documents;
            }
        }

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <returns></returns>
        public static new DocumentPack Create()
        {
            DocumentPack obj = new DocumentPack();
            obj.SetDefaultValues();
            return obj;
        }


        /// <summary>
        /// Loads an existing instance from the database.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static new DocumentPack Load(Guid ID)
        {
            DocumentPack obj = new DocumentPack();
            obj.Fetch(ID);
            return obj;
        }


        /// <summary>
        /// Loads all instances from the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ModularException"></exception>
        public static new List<DocumentPack> LoadList()
        {
            List<DocumentPack> AllDocumentPacks = new List<DocumentPack>();

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
                                    DocumentPack obj = GetOrdinals(DataReader);
                                    while (DataReader.Read())
                                    {
                                        AllDocumentPacks.Add(obj);
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
                                    DocumentPack obj = GetOrdinals(DataReader);

                                    while (DataReader.Read())
                                    {
                                        AllDocumentPacks.Add(obj);
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

            return AllDocumentPacks;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Name;
        }

        public override DocumentPack Clone()
        {
            return DocumentPack.Load(ID);
        }

        #endregion

        #region "  Data Methods  "

        protected static DocumentPack GetOrdinals(SqlDataReader DataReader)
        {
            DocumentPack obj = new DocumentPack();
            obj.SetFieldValues(Class.GetFields(), DataReader);
            return obj;
        }

        protected static DocumentPack GetOrdinals(SqliteDataReader DataReader)
        {
            DocumentPack obj = new DocumentPack();
            obj.SetFieldValues(Class.GetFields(), DataReader);
            return obj;
        }

        #endregion

    }
}
