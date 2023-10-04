using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Modular.Core.Databases;
using Modular.Core.Utility;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;

namespace Modular.Core.Credits
{
    [Serializable]
    public class CreditPayment : ModularBase
    {

        #region "  Constructors  "

        public CreditPayment()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_CreditPayment";
        protected static new readonly string MODULAR_DATABASE_STOREDPROCEDURE_PREFIX = "usp_Modular_CreditPayment";
        protected static new readonly Type MODULAR_OBJECTTYPE = typeof(CreditPayment);

        #endregion

        #region "  Variables  "

        private Guid _CreditID;

        private string _Reference = string.Empty;

        private DateTime _PaymentDate;

        private EnumUtils.PaymentMethodType _PaymentMethod;

        private decimal _Amount;

        #endregion

        #region "  Properties  "

        [Required(ErrorMessage = "Credit is required.")]
        [Display(Name = "Credit")]
        public Credit Credit
        {
            get
            {
                return Credit.Load(_CreditID);
            }
            set
            {
                if (_CreditID != value.ID)
                {
                    _CreditID = value.ID;
                    OnPropertyChanged("CreditID");
                }
            }
        }


        [Display(Name = "Reference")]
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


        [Required(ErrorMessage = "Payment Date is required.")]
        [Display(Name = "Payment Date")]
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


        [Display(Name = "Payment Method")]
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


        [Required(ErrorMessage = "Amount is required.")]
        [Display(Name = "Amount")]
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

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="CreditID"></param>
        /// <returns></returns>
        public static CreditPayment Create(Guid CreditID)
        {
            return CreditPayment.Create(Credit.Load(CreditID));
        }


        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="Credit"></param>
        /// <returns></returns>
        public static CreditPayment Create(Credit Credit)
        {
            CreditPayment obj = new CreditPayment();
            obj.SetDefaultValues();
            obj.Credit = Credit;
            return obj;
        }


        /// <summary>
        /// Load an existing instance.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static new CreditPayment Load(Guid ID)
        {
            CreditPayment obj = new CreditPayment();
            obj.Fetch(ID);
            return obj;
        }


        /// <summary>
        /// Loads all instances from the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ModularException"></exception>
        public static new List<CreditPayment> LoadList()
        {
            List<CreditPayment> AllCreditPayments = new List<CreditPayment>();

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
                                    CreditPayment obj = GetOrdinals(DataReader);
                                    while (DataReader.Read())
                                    {
                                        AllCreditPayments.Add(obj);
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
                                    CreditPayment obj = GetOrdinals(DataReader);

                                    while (DataReader.Read())
                                    {
                                        AllCreditPayments.Add(obj);
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

            return AllCreditPayments;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Reference;
        }

        public override CreditPayment Clone()
        {
            return CreditPayment.Load(ID);
        }

        #endregion

        #region "  Data Methods  "

        protected static CreditPayment GetOrdinals(SqlDataReader DataReader)
        {
            CreditPayment obj = new CreditPayment();
            obj.SetFieldValues(Class.GetFields(), DataReader);
            return obj;
        }

        protected static CreditPayment GetOrdinals(SqliteDataReader DataReader)
        {
            CreditPayment obj = new CreditPayment();
            obj.SetFieldValues(Class.GetFields(), DataReader);
            return obj;
        }

        #endregion

    }
}
