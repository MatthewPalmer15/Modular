using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Modular.Core.Databases;
using Modular.Core.Attributes;
using Modular.Core.Utility;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;

namespace Modular.Core.Invoicing
{
    [Serializable]
    public class Invoice : ModularBase
    {

        #region "  Constructors  "

        public Invoice()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Invoice";
        protected static new readonly string MODULAR_DATABASE_STOREDPROCEDURE_PREFIX = "usp_Modular_Invoice";
        protected static new readonly Type MODULAR_OBJECTTYPE = typeof(Invoice);

        #endregion

        #region "  Enums  "

        public enum InvoiceStatusType
        {
            Unknown = 0,
            Open = 1,
            Closed = 2,
            Completed = 3,
            Cancelled = 4,
            Deleted = 5
        }

        #endregion

        #region "  Variables  "

        private Guid _ContactID;

        private ObjectTypes.ObjectType _ObjectType;

        private Guid _ObjectID;

        private InvoiceStatusType _InvoiceStatus;

        private int _InvoiceNumber;

        private DateTime _InvoiceDate;

        private DateTime _DueDate;

        [Ignore]
        private List<InvoiceItem> _Items = new List<InvoiceItem>();


        [Ignore]
        private DateTime _LastRetrievedItems = DateTime.MinValue;

        [Ignore]
        private List<InvoicePayment> _Payments = new List<InvoicePayment>();

        [Ignore]
        private DateTime _LastRetrievedPayments = DateTime.MinValue;

        private bool _IsPrinted;

        private DateTime _PrintedDate;

        private string _PONumber = string.Empty;

        private string _Notes = string.Empty;

        #endregion

        #region "  Properties  "

        [Required(ErrorMessage = "Contact is required.")]
        [Display(Name = "Contact")]
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


        [Display(Name = "Type")]
        public ObjectTypes.ObjectType ObjectType
        {
            get
            {
                return _ObjectType;
            }
            set
            {
                if (_ObjectType != value)
                {
                    _ObjectType = value;
                    OnPropertyChanged("ObjectType");
                }
            }
        }


        public Guid ObjectID
        {
            get
            {
                return _ObjectID;
            }
            set
            {
                if (_ObjectID != value)
                {
                    _ObjectID = value;
                    OnPropertyChanged("ObjectID");
                }
            }
        }


        [Required(ErrorMessage = "Status is required.")]
        [Display(Name = "Status")]
        public InvoiceStatusType InvoiceStatus
        {
            get
            {
                return _InvoiceStatus;
            }
            set
            {
                if (_InvoiceStatus != value)
                {
                    _InvoiceStatus = value;
                    OnPropertyChanged("InvoiceStatus");
                }
            }
        }


        [Required(ErrorMessage = "Invoice Number is required.")]
        [Display(Name = "Invoice Number", ShortName = "Invoice No.")]
        public int InvoiceNumber
        {
            get
            {
                return _InvoiceNumber;
            }
            set
            {
                if (_InvoiceNumber != value)
                {
                    _InvoiceNumber = value;
                    OnPropertyChanged("InvoiceNumber");
                }
            }
        }


        [Required(ErrorMessage = "Invoice Date is required.")]
        [Display(Name = "Invoice Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime InvoiceDate
        {
            get
            {
                return _InvoiceDate;
            }
            set
            {
                if (_InvoiceDate != value)
                {
                    _InvoiceDate = value;
                    OnPropertyChanged("InvoiceDate");
                }
            }
        }


        [Required(ErrorMessage = "Due Date is required.")]
        [Display(Name = "Due Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DueDate
        {
            get
            {
                return _DueDate;
            }
            set
            {
                if (_DueDate != value)
                {
                    _DueDate = value;
                    OnPropertyChanged("DueDate");
                }
            }
        }


        [Display(Name = "Items")]
        public List<InvoiceItem> Items
        {
            get
            {
                if (_Items.Count == 0 || _LastRetrievedItems.AddMinutes(5) > DateTime.Now)
                {
                    _Items = InvoiceItem.LoadList().Where(InvoiceItem => InvoiceItem.Invoice.ID == ID).ToList();
                    _LastRetrievedItems = DateTime.Now;
                }
                return _Items;
            }
        }


        [Display(Name = "Payments")]
        public List<InvoicePayment> Payments
        {
            get
            {
                if (_Payments.Count == 0 || _LastRetrievedPayments.AddMinutes(5) > DateTime.Now)
                {
                    _Payments = InvoicePayment.LoadList().Where(InvoicePayment => InvoicePayment.Invoice.ID == ID).ToList();
                    _LastRetrievedPayments = DateTime.Now;
                }
                return _Payments;
            }
        }


        [Display(Name = "Paid")]
        public bool IsPaid
        {
            get
            {
                return (TotalPriceIncVAT - TotalPaid) == 0;
            }
        }


        [Display(Name = "Printed")]
        public bool IsPrinted
        {
            get
            {
                return _IsPrinted;
            }
            set
            {
                if (_IsPrinted != value)
                {
                    _IsPrinted = value;
                    OnPropertyChanged("IsPrinted");
                }
            }
        }


        [Display(Name = "Printed Date")]
        public DateTime PrintedDate
        {
            get
            {
                return _PrintedDate;
            }
            set
            {
                if (_PrintedDate != value)
                {
                    _PrintedDate = value;
                    OnPropertyChanged("PrintedDate");
                }
            }
        }


        [Required(ErrorMessage = "PO Number is required.")]
        [Display(Name = "PO Number")]
        public string PONumber
        {
            get
            {
                return _PONumber;
            }
            set
            {
                if (_PONumber != value)
                {
                    _PONumber = value;
                    OnPropertyChanged("PONumber");
                }
            }
        }


        [Display(Name = "Notes")]
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


        [Display(Name = "Total Price Exc VAT")]
        public decimal TotalPriceExcVAT
        {
            get
            {
                decimal Total = 0;
                foreach (InvoiceItem Item in Items)
                {
                    Total += Item.UnitPriceExcVAT;
                }
                return Total;
            }
        }


        [Display(Name = "Total Price VAT")]
        public decimal TotalPriceVAT
        {
            get
            {
                decimal Total = 0;
                foreach (InvoiceItem Item in Items)
                {
                    Total += Item.UnitPriceVAT;
                }
                return Total;
            }
        }


        [Display(Name = "Total Price Inc VAT")]
        public decimal TotalPriceIncVAT
        {
            get
            {
                return TotalPriceExcVAT + TotalPriceVAT;
            }
        }


        [Display(Name = "Total Paid")]
        public decimal TotalPaid
        {
            get
            {
                decimal Total = 0;
                foreach (InvoicePayment Payment in Payments)
                {
                    Total += Payment.Amount;
                }
                return Total;
            }
        }


        #endregion

        #region "  Static Methods  "

        public static new Invoice Create()
        {
            Invoice obj = new Invoice();
            obj.SetDefaultValues();
            return obj;
        }

        public static new Invoice Load(Guid ID)
        {
            Invoice obj = new Invoice();
            obj.Fetch(ID);
            return obj;
        }

        /// <summary>
        /// Loads all instances from the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ModularException"></exception>
        public static new List<Invoice> LoadList()
        {
            List<Invoice> AllInvoices = new List<Invoice>();

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
                                    Invoice obj = GetOrdinals(DataReader);
                                    while (DataReader.Read())
                                    {
                                        AllInvoices.Add(obj);
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
                                    Invoice obj = GetOrdinals(DataReader);

                                    while (DataReader.Read())
                                    {
                                        AllInvoices.Add(obj);
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

            return AllInvoices;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return $"Invoice #{_InvoiceNumber}";
        }

        #endregion

        #region "  Data Methods  "

        protected static Invoice GetOrdinals(SqlDataReader DataReader)
        {
            Invoice obj = new Invoice();
            obj.SetFieldValues(Class.GetFields(), DataReader);
            return obj;
        }

        protected static Invoice GetOrdinals(SqliteDataReader DataReader)
        {
            Invoice obj = new Invoice();
            obj.SetFieldValues(Class.GetFields(), DataReader);
            return obj;
        }

        #endregion

    }
}
