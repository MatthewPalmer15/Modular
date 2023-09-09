using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;
using Modular.Core.Databases;
using Modular.Core.Geo;

namespace Modular.Core.Entity
{
    public class Contact : ModularBase
    {
        #region "  Constructors  "

        public Contact()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Contact";
        protected static new readonly string MODULAR_DATABASE_STOREDPROCEDURE_PREFIX = "usp_Modular_Contact";
        protected static new readonly Type MODULAR_OBJECTTYPE = typeof(Contact);

        #endregion

        #region "  Enums  "

        public enum GenderType
        {
            Unknown = 0,
            Male = 1,
            Female = 2,
            Other = 3,
            PreferNotToSay = 4

        }

        public enum TitleType
        {
            Unknown = 0,
            Mr = 1,
            Mrs = 2,
            Ms = 3,
            Miss = 4,
            Dr = 5
        }

        #endregion

        #region "  Variables  "

        private TitleType _Title;

        private string _Forename = string.Empty;

        private string _Surname = string.Empty;

        private DateTime _DateOfBirth;

        private GenderType _Gender;

        private string _AddressLine1 = string.Empty;

        private string _AddressLine2 = string.Empty;

        private string _AddressLine3 = string.Empty;

        private string _AddressCity = string.Empty;

        private string _AddressCounty = string.Empty;

        private Guid _AddressCountryID;

        private string _AddressPostcode = string.Empty;

        private string _Email = string.Empty;

        private string _SecondaryEmail = string.Empty;

        private string _Phone = string.Empty;

        private string _Mobile = string.Empty;

        private string _FacebookLink = string.Empty;

        private string _InstagramLink = string.Empty;

        private string _TwitterLink = string.Empty;

        private string _LinkedInLink = string.Empty;

        private string _WebsiteLink = string.Empty;

        private Guid _OccupationID;

        private Guid _DepartmentID;

        private Guid _OrganisationID;

        private bool _IsVerified;

        private bool _IsBanned;

        #endregion

        #region "  Properties  "

        [Display(Name = "Title")]
        public TitleType Title
        {
            get
            {
                return _Title;
            }
            set
            {
                if (_Title != value)
                {
                    _Title = value;
                    OnPropertyChanged("Title");
                }
            }
        }


        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return $"{_Forename} {_Surname}";
            }
        }


        [Required(ErrorMessage = "Forename is required.")]
        [Display(Name = "Forename")]
        public string Forename
        {
            get
            {
                return _Forename;
            }
            set
            {
                if (_Forename != value)
                {
                    _Forename = value;
                    OnPropertyChanged("Forename");
                }
            }
        }


        [Required(ErrorMessage = "Surname is required.")]
        [Display(Name = "Surname")] // DisplayFormat(NullDisplayText = "Please enter your last name")]
        public string Surname
        {
            get
            {
                return _Surname;
            }
            set
            {
                if (_Surname != value)
                {
                    _Surname = value;
                    OnPropertyChanged("Surname");
                }
            }
        }


        [DataType(DataType.Date, ErrorMessage = "Date must be in format DD/MM/YYYY.")]
        [Display(Name = "Date of Birth", ShortName = "DoB"), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth
        {
            get
            {
                return _DateOfBirth;
            }
            set
            {
                if (_DateOfBirth != value)
                {
                    _DateOfBirth = value;
                    OnPropertyChanged("DateOfBirth");
                }
            }
        }


        [Display(Name = "Gender")]
        public GenderType Gender
        {
            get
            {
                return _Gender;
            }
            set
            {
                if (_Gender != value)
                {
                    _Gender = value;
                    OnPropertyChanged("Gender");
                }
            }
        }


        [Display(Name = "Age")]
        public int Age
        {
            get
            {
                try
                {
                    return DateTime.Now.Year - DateOfBirth.Year;
                }
                catch
                {
                    return 0;
                }
            }
        }

        [Display(Name = "Address")]
        public string FullAddressOneLine
        {
            get
            {
                return AddressLine1 + ", " + AddressLine2 + ", " + AddressLine3 + ", " + AddressCity + ", " + AddressCounty + ", " + AddressPostcode + ", " + AddressCountry.Name;
            }
        }

        [Display(Name = "Address")]
        public string FullAddressMultiLine
        {
            get
            {
                return AddressLine1 + Environment.NewLine + AddressLine2 + Environment.NewLine + AddressLine3 + Environment.NewLine +
                       AddressCity + Environment.NewLine + AddressCounty + Environment.NewLine + AddressPostcode + Environment.NewLine + AddressCountry.Name;
            }
        }


        [Display(Name = "Address Line 1")]
        public string AddressLine1
        {
            get
            {
                return _AddressLine1;
            }
            set
            {
                if (_AddressLine1 != value)
                {
                    _AddressLine1 = value;
                    OnPropertyChanged("AddressLine1");
                }
            }
        }


        [Display(Name = "Address Line 2")]
        public string AddressLine2
        {
            get
            {
                return _AddressLine2;
            }
            set
            {
                if (_AddressLine2 != value)
                {
                    _AddressLine2 = value;
                    OnPropertyChanged("AddressLine2");
                }
            }
        }


        [Display(Name = "Address Line 3")]
        public string AddressLine3
        {
            get
            {
                return _AddressLine3;
            }
            set
            {
                if (_AddressLine3 != value)
                {
                    _AddressLine3 = value;
                    OnPropertyChanged("AddressLine3");
                }
            }
        }


        [Display(Name = "City")]
        public string AddressCity
        {
            get
            {
                return _AddressCity;
            }
            set
            {
                if (_AddressCity != value)
                {
                    _AddressCity = value;
                    OnPropertyChanged("AddressCity");
                }
            }
        }


        [Display(Name = "County")]
        public string AddressCounty
        {
            get
            {
                return _AddressCounty;
            }
            set
            {
                if (_AddressCounty != value)
                {
                    _AddressCounty = value;
                    OnPropertyChanged("AddressCounty");
                }
            }
        }


        [Display(Name = "Country")]
        public Country AddressCountry
        {
            get
            {
                return Country.Load(_AddressCountryID);
            }
            set
            {
                if (_AddressCountryID != value.ID)
                {
                    _AddressCountryID = value.ID;
                    OnPropertyChanged("AddressCountryID");
                }
            }
        }

        [DataType(DataType.PostalCode, ErrorMessage = "Your Postcode is incorrect, please try again.")]
        [Display(Name = "Postcode")]
        public string AddressPostcode
        {
            get
            {
                return _AddressPostcode;
            }
            set
            {
                if (_AddressPostcode != value)
                {
                    _AddressPostcode = value;
                    OnPropertyChanged("AddressPostcode");
                }
            }
        }


        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Your Email is incorrect, please try again.")]
        [Display(Name = "Email Address", ShortName = "Email")]
        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                if (_Email != value)
                {
                    _Email = value;
                    OnPropertyChanged("Email");
                }
            }
        }


        [DataType(DataType.EmailAddress, ErrorMessage = "Your Email is incorrect, please try again.")]
        [Display(Name = "Email Address", ShortName = "Email")]
        public string SecondaryEmail
        {
            get
            {
                return _SecondaryEmail;
            }
            set
            {
                if (_SecondaryEmail != value)
                {
                    _SecondaryEmail = value;
                    OnPropertyChanged("SecondaryEmail");
                }
            }
        }


        [DataType(DataType.PhoneNumber, ErrorMessage = "Your Phone Number is incorrect, please try again.")]
        [Display(Name = "Phone Number", ShortName = "Phone")]
        public string Phone
        {
            get
            {
                return _Phone;
            }
            set
            {
                if (_Phone != value)
                {
                    _Phone = value;
                    OnPropertyChanged("Phone");
                }
            }
        }

        [DataType(DataType.PhoneNumber, ErrorMessage = "Your Phone Number is incorrect, please try again.")]
        [Display(Name = "Phone Number", ShortName = "Phone")]
        public string Mobile
        {
            get
            {
                return _Mobile;
            }
            set
            {
                if (_Mobile != value)
                {
                    _Mobile = value;
                    OnPropertyChanged("Mobile");
                }
            }
        }

        [Display(Name = "Facebook Link")]
        public string FacebookLink
        {
            get
            {
                return _FacebookLink;
            }
            set
            {
                if (_FacebookLink != value)
                {
                    _FacebookLink = value;
                    OnPropertyChanged("FacebookLink");
                }
            }
        }

        [Display(Name = "Instagram Link")]
        public string InstagramLink
        {
            get
            {
                return _InstagramLink;
            }
            set
            {
                if (_InstagramLink != value)
                {
                    _InstagramLink = value;
                    OnPropertyChanged("InstagramLink");
                }
            }
        }

        [Display(Name = "Twitter Link")]
        public string TwitterLink
        {
            get
            {
                return _TwitterLink;
            }
            set
            {
                if (_TwitterLink != value)
                {
                    _TwitterLink = value;
                    OnPropertyChanged("TwitterLink");
                }
            }
        }

        [Display(Name = "LinkedIn Link")]
        public string LinkedInLink
        {
            get
            {
                return _LinkedInLink;
            }
            set
            {
                if (_LinkedInLink != value)
                {
                    _LinkedInLink = value;
                    OnPropertyChanged("LinkedInLink");
                }
            }
        }

        [Display(Name = "Website Link")]
        public string WebsiteLink
        {
            get
            {
                return _WebsiteLink;
            }
            set
            {
                if (_WebsiteLink != value)
                {
                    _WebsiteLink = value;
                    OnPropertyChanged("WebsiteLink");
                }
            }
        }

        [Display(Name = "Occupation")]
        public Occupation Occupation
        {
            get
            {
                return Occupation.Load(_OccupationID);
            }
            set
            {
                if (_OccupationID != value.ID)
                {
                    _OccupationID = value.ID;
                    OnPropertyChanged("OccupationID");
                }
            }
        }

        [Display(Name = "Department")]
        public Department Department
        {
            get
            {
                return Department.Load(_DepartmentID);
            }
            set
            {
                if (_DepartmentID != value.ID)
                {
                    _DepartmentID = value.ID;
                    OnPropertyChanged("DepartmentID");
                }
            }
        }

        [Display(Name = "Organisation")]
        public Organisation Organisation
        {
            get
            {
                return Organisation.Load(_OrganisationID);
            }
            set
            {
                if (_OrganisationID != value.ID)
                {
                    _OrganisationID = value.ID;
                    OnPropertyChanged("OrganisationID");
                }
            }
        }


        [Required]
        [Display(Name = "Verified")]
        [DefaultValue(false)]
        public bool IsVerified
        {
            get
            {
                return _IsVerified;
            }
            set
            {
                if (_IsVerified != value)
                {
                    _IsVerified = value;
                    OnPropertyChanged("IsVerified");
                }
            }
        }


        [Required]
        [Display(Name = "Banned")]
        [DefaultValue(false)]
        public bool IsBanned
        {
            get
            {
                return _IsBanned;
            }
            set
            {
                if (_IsBanned != value)
                {
                    _IsBanned = value;
                    OnPropertyChanged("IsBanned");
                }
            }
        }

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <returns>A new instance</returns>
        public static new Contact Create()
        {
            Contact obj = new Contact();
            obj.SetDefaultValues();
            return obj;
        }


        /// <summary>
        /// Loads an existing instance from the database.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static new Contact Load(Guid ID)
        {
            Contact obj = new Contact();
            obj.Fetch(ID);
            return obj;
        }


        /// <summary>
        /// Loads all instances from the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ModularException"></exception>
        public static new List<Contact> LoadList()
        {
            List<Contact> AllContacts = new List<Contact>();

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
                                    Contact obj = GetOrdinals(DataReader);
                                    while (DataReader.Read())
                                    {
                                        AllContacts.Add(obj);
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
                                    Contact obj = GetOrdinals(DataReader);

                                    while (DataReader.Read())
                                    {
                                        AllContacts.Add(obj);
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

            return AllContacts;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return FullName;
        }

        public override Contact Clone()
        {
            return Contact.Load(ID);
        }

        #endregion

        #region "  Data Methods  "

        protected static Contact GetOrdinals(SqlDataReader DataReader)
        {
            Contact obj = new Contact();
            obj.SetFieldValues(CurrentClass.GetFields(), DataReader);
            return obj;
        }

        protected static Contact GetOrdinals(SqliteDataReader DataReader)
        {
            Contact obj = new Contact();
            obj.SetFieldValues(CurrentClass.GetFields(), DataReader);
            return obj;
        }

        #endregion

    }
}
