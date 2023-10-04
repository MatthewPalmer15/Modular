using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Modular.Core.Databases;
using Modular.Core.Utility;
using System.Data;
using System.Reflection;

namespace Modular.Core.Invoicing
{
    [Serializable]
    public class InvoicePayment : ModularBase
    {

        #region "  Constructors  "

        private InvoicePayment()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_InvoicePayment";
        protected static new readonly string MODULAR_DATABASE_STOREDPROCEDURE_PREFIX = "usp_Modular_InvoicePayment";
        protected static new readonly Type MODULAR_OBJECTTYPE = typeof(InvoicePayment);

        #endregion

        #region "  Variables  "

        private Guid _InvoiceID;

        private string _Reference = string.Empty;

        private DateTime _PaymentDate;

        private EnumUtils.PaymentMethodType _PaymentMethod;

        private decimal _Amount;

        private bool _IsSuccessful;

        private string _Notes = string.Empty;

        #endregion

        #region "  Properties  "

        public Invoice Invoice
        {
            get
            {
                return Invoice.Load(_InvoiceID);
            }
            set
            {
                if (_InvoiceID != value.ID)
                {
                    _InvoiceID = value.ID;
                    OnPropertyChanged("InvoiceID");
                }
            }
        }

        public string Reference
        {
            get
            {
                return _Reference;
            }
            set
            {
                if (_Reference != value)
                {
                    _Reference = value;
                    OnPropertyChanged("Reference");
                }
            }
        }

        public DateTime PaymentDate
        {
            get
            {
                return _PaymentDate;
            }
            set
            {
                if (_PaymentDate != value)
                {
                    _PaymentDate = value;
                    OnPropertyChanged("PaymentDate");
                }
            }
        }

        public EnumUtils.PaymentMethodType PaymentMethod
        {
            get
            {
                return _PaymentMethod;
            }
            set
            {
                if (_PaymentMethod != value)
                {
                    _PaymentMethod = value;
                    OnPropertyChanged("PaymentMethod");
                }
            }
        }

        public decimal Amount
        {
            get
            {
                return _Amount;
            }
            set
            {
                if (_Amount != value)
                {
                    _Amount = value;
                    OnPropertyChanged("Amount");
                }
            }
        }

        public bool IsSuccessful
        {
            get
            {
                return _IsSuccessful;
            }
            set
            {
                if (_IsSuccessful != value)
                {
                    _IsSuccessful = value;
                    OnPropertyChanged("IsSuccessful");
                }
            }
        }

        public string Notes
        {
            get
            {
                return _Notes;
            }
            set
            {
                if (_Notes != value)
                {
                    _Notes = value;
                    OnPropertyChanged("Notes");
                }
            }
        }

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="InvoiceID"></param>
        /// <returns></returns>
        public static InvoicePayment Create(Guid InvoiceID)
        {
            return InvoicePayment.Create(Invoice.Load(InvoiceID));
        }


        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="Invoice"></param>
        /// <returns></returns>
        public static InvoicePayment Create(Invoice Invoice)
        {
            InvoicePayment obj = new InvoicePayment();
            obj.SetDefaultValues();
            obj.Invoice = Invoice;
            return obj;
        }


        /// <summary>
        /// Load an existing instance.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static new InvoicePayment Load(Guid ID)
        {
            InvoicePayment obj = new InvoicePayment();
            obj.Fetch(ID);
            return obj;
        }


        /// <summary>
        /// Loads all instances from the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ModularException"></exception>
        public static new List<InvoicePayment> LoadList()
        {
            List<InvoicePayment> AllInvoicePayments = new List<InvoicePayment>();

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
                                    InvoicePayment obj = GetOrdinals(DataReader);
                                    while (DataReader.Read())
                                    {
                                        AllInvoicePayments.Add(obj);
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
                                    InvoicePayment obj = GetOrdinals(DataReader);

                                    while (DataReader.Read())
                                    {
                                        AllInvoicePayments.Add(obj);
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

            return AllInvoicePayments;
        }

        #endregion

        #region "  Data Methods  "

        protected static InvoicePayment GetOrdinals(SqlDataReader DataReader)
        {
            InvoicePayment obj = new InvoicePayment();
            obj.SetFieldValues(Class.GetFields(), DataReader);
            return obj;
        }

        protected static InvoicePayment GetOrdinals(SqliteDataReader DataReader)
        {
            InvoicePayment obj = new InvoicePayment();
            obj.SetFieldValues(Class.GetFields(), DataReader);
            return obj;
        }

        #endregion
    }
}
