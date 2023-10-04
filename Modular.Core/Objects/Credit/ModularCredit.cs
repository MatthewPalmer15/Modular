using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Modular.Core.Databases;
using Modular.Core.Attributes;
using Modular.Core.Utility;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;

namespace Modular.Core.Credits
{
    [Serializable]
    public class Credit : ModularBase
    {

        #region "  Constructors  "

        public Credit()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Credit";
        protected static new readonly string MODULAR_DATABASE_STOREDPROCEDURE_PREFIX = "usp_Modular_Credit";
        protected static new readonly Type MODULAR_OBJECTTYPE = typeof(Credit);

        #endregion

        #region "  Enums  "

        public enum CreditStatusType
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

        private CreditStatusType _CreditStatus;

        private int _CreditNumber;

        private DateTime _CreditDate;

        private DateTime _PaidDate;

        [Ignore]
        private List<CreditItem> _Items = new List<CreditItem>();

        [Ignore]
        private DateTime _LastRetrievedItems = DateTime.MinValue;

        [Ignore]
        private List<CreditPayment> _Payments = new List<CreditPayment>();

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
        public CreditStatusType Status
        {
            get
            {
                return _CreditStatus;
            }
            set
            {
                if (_CreditStatus != value)
                {
                    _CreditStatus = value;
                    OnPropertyChanged("CreditStatus");
                }
            }
        }


        [Required(ErrorMessage = "Credit Number is required.")]
        [Display(Name = "Credit Number", ShortName = "Credit No.")]
        public int CreditNumber
        {
            get
            {
                return _CreditNumber;
            }
            set
            {
                if (_CreditNumber != value)
                {
                    _CreditNumber = value;
                    OnPropertyChanged("CreditNumber");
                }
            }
        }


        [Required(ErrorMessage = "Credit Date is required.")]
        [Display(Name = "Credit Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreditDate
        {
            get
            {
                return _CreditDate;
            }
            set
            {
                if (_CreditDate != value)
                {
                    _CreditDate = value;
                    OnPropertyChanged("CreditDate");
                }
            }
        }


        [Display(Name = "Paid Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PaidDate
        {
            get
            {
                return _PaidDate;
            }
            set
            {
                if (_PaidDate != value)
                {
                    _PaidDate = value;
                    OnPropertyChanged("PaidDate");
                }
            }
        }


        [Display(Name = "Items")]
        public List<CreditItem> Items
        {
            get
            {
                if (_Items.Count == 0 || _LastRetrievedItems.AddMinutes(5) < DateTime.Now)
                {
                    _Items = CreditItem.LoadList().Where(CreditItem => CreditItem.Credit.ID == ID).ToList();
                    _LastRetrievedItems = DateTime.Now;
                }
                return _Items;
            }
        }


        [Display(Name = "Payments")]
        public List<CreditPayment> Payments
        {
            get
            {
                if (_Payments.Count == 0 || _LastRetrievedPayments.AddMinutes(5) < DateTime.Now)
                {
                    _Payments = CreditPayment.LoadList().Where(CreditPayment => CreditPayment.Credit.ID == ID).ToList();
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
                foreach (CreditItem Item in Items)
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
                foreach (CreditItem Item in Items)
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
                foreach (CreditPayment Payment in Payments)
                {
                    Total += Payment.Amount;
                }
                return Total;
            }
        }

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <returns>A new instance</returns>
        public static new Credit Create()
        {
            Credit obj = new Credit();
            obj.SetDefaultValues();
            return obj;
        }


        /// <summary>
        /// Loads an existing instance from the database.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static new Credit Load(Guid ID)
        {
            Credit obj = new Credit();
            obj.Fetch(ID);
            return obj;
        }

        /// <summary>
        /// Loads all instances from the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ModularException"></exception>
        public static new List<Credit> LoadList()
        {
            List<Credit> AllCredits = new List<Credit>();

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
                                    Credit obj = GetOrdinals(DataReader);
                                    while (DataReader.Read())
                                    {
                                        AllCredits.Add(obj);
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
                                    Credit obj = GetOrdinals(DataReader);

                                    while (DataReader.Read())
                                    {
                                        AllCredits.Add(obj);
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

            return AllCredits;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return $"Credit #{CreditNumber}";
        }

        public override Credit Clone()
        {
            return Credit.Load(ID);
        }

        #endregion

        #region "  Data Methods  "

        protected static Credit GetOrdinals(SqlDataReader DataReader)
        {
            Credit obj = new Credit();
            obj.SetFieldValues(Class.GetFields(), DataReader);
            return obj;
        }

        protected static Credit GetOrdinals(SqliteDataReader DataReader)
        {
            Credit obj = new Credit();
            obj.SetFieldValues(Class.GetFields(), DataReader);
            return obj;
        }

        #endregion

    }
}
