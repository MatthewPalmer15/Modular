using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Modular.Core.Databases;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;

namespace Modular.Core.Invoicing
{
    [Serializable]
    public class InvoiceItem : ModularBase
    {

        #region "  Constructors  "

        public InvoiceItem()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_InvoiceItem";
        protected static new readonly string MODULAR_DATABASE_STOREDPROCEDURE_PREFIX = "usp_Modular_InvoiceItem";
        protected static new readonly Type MODULAR_OBJECTTYPE = typeof(InvoiceItem);


        #endregion

        #region "  Variables  "

        private Guid _InvoiceID;

        [MaxLength(255)]
        private string _Name = string.Empty;

        [MaxLength(2047)]
        private string _Description = string.Empty;

        private decimal _UnitPrice;

        private decimal _UnitPriceVAT;

        private decimal _Quantity;

        #endregion

        #region "  Properties  "

        [Display(Name = "Invoice")]
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
        /// <param name="InvoiceID"></param>
        /// <returns></returns>
        public static InvoiceItem Create(Guid InvoiceID)
        {
            return InvoiceItem.Create(Invoice.Load(InvoiceID));
        }


        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="Invoice"></param>
        /// <returns></returns>
        public static InvoiceItem Create(Invoice Invoice)
        {
            InvoiceItem obj = new InvoiceItem();
            obj.SetDefaultValues();
            obj.Invoice = Invoice;
            return obj;
        }


        /// <summary>
        /// Loads an existing instance from the database.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static new InvoiceItem Load(Guid ID)
        {
            InvoiceItem obj = new InvoiceItem();
            obj.Fetch(ID);
            return obj;
        }


        /// <summary>
        /// Loads all instances from the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ModularException"></exception>
        public static new List<InvoiceItem> LoadList()
        {
            List<InvoiceItem> AllInvoiceItems = new List<InvoiceItem>();

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
                                    InvoiceItem obj = GetOrdinals(DataReader);
                                    while (DataReader.Read())
                                    {
                                        AllInvoiceItems.Add(obj);
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
                                    InvoiceItem obj = GetOrdinals(DataReader);

                                    while (DataReader.Read())
                                    {
                                        AllInvoiceItems.Add(obj);
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

            return AllInvoiceItems;
        }


        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Name;
        }

        public override InvoiceItem Clone()
        {
            return InvoiceItem.Load(ID);
        }

        #endregion

        #region "  Data Methods  "

        protected static InvoiceItem GetOrdinals(SqlDataReader DataReader)
        {
            InvoiceItem obj = new InvoiceItem();
            obj.SetFieldValues(Class.GetFields(), DataReader);
            return obj;
        }

        protected static InvoiceItem GetOrdinals(SqliteDataReader DataReader)
        {
            InvoiceItem obj = new InvoiceItem();
            obj.SetFieldValues(Class.GetFields(), DataReader);
            return obj;
        }

        #endregion

    }
}
