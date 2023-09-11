using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Modular.Core.Databases;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;

namespace Modular.Core.Credits
{
    [Serializable]
    public class CreditItem : ModularBase
    {

        #region "  Constructors  "

        public CreditItem()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_CreditItem";
        protected static new readonly string MODULAR_DATABASE_STOREDPROCEDURE_PREFIX = "usp_Modular_CreditItem";
        protected static new readonly Type MODULAR_OBJECTTYPE = typeof(CreditItem);

        #endregion

        #region "  Enums  "

        public enum InvoiceType
        {
            Unknown = 0,
            Invoice = 1,
            Credit = 2,
            Quote = 3
        }

        #endregion

        #region "  Variables  "

        private Guid _CreditID;

        [MaxLength(255)]
        private string _Name = string.Empty;

        [MaxLength(2047)]
        private string _Description = string.Empty;

        private decimal _UnitPrice;

        private decimal _UnitPriceVAT;

        private decimal _Quantity;

        #endregion

        #region "  Properties  "

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


        [Required(ErrorMessage = "Please enter a name.")]
        [MaxLength(255, ErrorMessage = "Name should be less than 255 Characters.")]
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


        [MaxLength(2047, ErrorMessage = "Description should be less than 2048 Characters.")]
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    OnPropertyChanged("Description");
                }
            }
        }


        [Required(ErrorMessage = "Please enter Unit Price.")]
        [Display(Name = "Unit Price")]
        public decimal UnitPriceExcVAT
        {
            get
            {
                return _UnitPrice;
            }
            set
            {
                if (_UnitPrice != value)
                {
                    _UnitPrice = value;
                    OnPropertyChanged("UnitPrice");
                }
            }
        }


        [Required(ErrorMessage = "Please enter Unit Price VAT.")]
        [Display(Name = "Unit Price VAT")]
        public decimal UnitPriceVAT
        {
            get
            {
                return _UnitPriceVAT;
            }
            set
            {
                if (_UnitPriceVAT != value)
                {
                    _UnitPriceVAT = value;
                    OnPropertyChanged("UnitPriceVAT");
                }
            }
        }


        [Display(Name = "Unit Price Inc VAT")]
        public decimal UnitPriceIncVAT
        {
            get
            {
                return UnitPriceExcVAT + UnitPriceVAT;
            }
        }


        [Display(Name = "Quantity")]
        public decimal Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                if (_Quantity != value)
                {
                    _Quantity = value;
                    OnPropertyChanged("Quantity");
                }
            }
        }


        [Display(Name = "Total Price Exc VAT")]
        public decimal TotalPriceeExcVAT
        {
            get
            {
                return UnitPriceExcVAT * Quantity;

            }
        }


        [Display(Name = "Total Price VAT")]
        public decimal TotalPriceVAT
        {
            get
            {
                return UnitPriceIncVAT * Quantity;
            }
        }


        [Display(Name = "Total Price Inc VAT")]
        public decimal TotalPriceIncVAT
        {
            get
            {
                return TotalPriceeExcVAT + TotalPriceVAT;
            }
        }

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="CreditID"></param>
        /// <returns></returns>
        public static CreditItem Create(Guid CreditID)
        {
            return CreditItem.Create(Credit.Load(CreditID));
        }


        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="Credit"></param>
        /// <returns></returns>
        public static CreditItem Create(Credit Credit)
        {
            CreditItem obj = new CreditItem();
            obj.SetDefaultValues();
            obj.Credit = Credit;
            return obj;
        }


        /// <summary>
        /// Loads an existing instance from the database.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static new CreditItem Load(Guid ID)
        {
            CreditItem obj = new CreditItem();
            obj.Fetch(ID);
            return obj;
        }


        /// <summary>
        /// Loads all instances from the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ModularException"></exception>
        public static new List<CreditItem> LoadList()
        {
            List<CreditItem> AllCreditItems = new List<CreditItem>();

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
                                    CreditItem obj = GetOrdinals(DataReader);
                                    while (DataReader.Read())
                                    {
                                        AllCreditItems.Add(obj);
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
                                    CreditItem obj = GetOrdinals(DataReader);

                                    while (DataReader.Read())
                                    {
                                        AllCreditItems.Add(obj);
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

            return AllCreditItems;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Name;
        }

        public override CreditItem Clone()
        {
            return CreditItem.Load(ID);
        }

        #endregion

        #region "  Data Methods  "

        protected static CreditItem GetOrdinals(SqlDataReader DataReader)
        {
            CreditItem obj = new CreditItem();
            obj.SetFieldValues(CurrentClass.GetFields(), DataReader);
            return obj;
        }

        protected static CreditItem GetOrdinals(SqliteDataReader DataReader)
        {
            CreditItem obj = new CreditItem();
            obj.SetFieldValues(CurrentClass.GetFields(), DataReader);
            return obj;
        }

        #endregion

    }
}
