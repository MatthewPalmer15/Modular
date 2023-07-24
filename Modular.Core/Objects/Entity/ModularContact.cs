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
        [MinLength(2, ErrorMessage = "Forename must be more than 2 characters.")]
        [MaxLength(100, ErrorMessage = "Forename must be less than 100 characters.")]
        [Display(Name = "Forename")] //DisplayFormat(NullDisplayText = "Please enter your first name")]
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
        [MinLength(2, ErrorMessage = "Surname must be more than 2 characters.")]
        [MaxLength(100, ErrorMessage = "Surname must be less than 100 characters.")]
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


        [MinLength(2, ErrorMessage = "Address Line must be more than 2 characters.")]
        [MaxLength(100, ErrorMessage = "Address Line must be less than 100 characters.")]
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


        [MinLength(2, ErrorMessage = "Address Line must be more than 2 characters.")]
        [MaxLength(100, ErrorMessage = "Address Line must be less than 100 characters.")]
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


        [MinLength(2, ErrorMessage = "Address Line must be more than 2 characters.")]
        [MaxLength(100, ErrorMessage = "Address Line must be less than 100 characters.")]
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


        [MinLength(2, ErrorMessage = "City must be more than 2 characters.")]
        [MaxLength(100, ErrorMessage = "City must be less than 100 characters.")]
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


        [MinLength(2, ErrorMessage = "County must be more than 2 characters.")]
        [MaxLength(100, ErrorMessage = "County must be less than 100 characters.")]
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


        public Guid AddressCountryID
        {
            get
            {
                return _AddressCountryID;
            }
            set
            {
                if (_AddressCountryID != value)
                {
                    _AddressCountryID = value;
                    OnPropertyChanged("AddressCountryID");
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
        }


        [DataType(DataType.PostalCode, ErrorMessage = "Your Postcode is incorrect, please try again.")]
        [MinLength(2, ErrorMessage = "Postcode must be more than 2 characters.")]
        [MaxLength(25, ErrorMessage = "Postcode must be less than 25 characters.")]
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
        [MinLength(5, ErrorMessage = "Email must be more than 5 characters.")]
        [MaxLength(250, ErrorMessage = "Email must be less than 250 characters.")]
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
        [MaxLength(250, ErrorMessage = "Email must be less than 250 characters.")]
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
        [MinLength(5, ErrorMessage = "Phone Number must be more than 5 characters.")]
        [MaxLength(25, ErrorMessage = "Phone Number must be less than 25 characters.")]
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
        [MinLength(5, ErrorMessage = "Phone Number must be more than 5 characters.")]
        [MaxLength(25, ErrorMessage = "Phone Number must be less than 25 characters.")]
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

        public Guid OccupationID
        {
            get
            {
                return _OccupationID;
            }
            set
            {
                if (_OccupationID != value)
                {
                    _OccupationID = value;
                    OnPropertyChanged("OccupationID");
                }
            }
        }


        public Guid DepartmentID
        {
            get
            {
                return _DepartmentID;
            }
            set
            {
                if (_DepartmentID != value)
                {
                    _DepartmentID = value;
                    OnPropertyChanged("DepartmentID");
                }
            }
        }


        public Guid OrganisationID
        {
            get
            {
                return _OrganisationID;
            }
            set
            {
                if (_OrganisationID != value)
                {
                    _OrganisationID = value;
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
        /// Creates a new instance with default values
        /// </summary>
        /// <returns>A new instance</returns>
        public static new Contact Create()
        {
            Contact obj = new Contact();
            obj.SetDefaultValues();
            return obj;
        }

        public static new List<Contact> LoadAll()
        {
            return FetchAll();
        }

        public static new Contact Load(Guid ID)
        {
            Contact obj = new Contact();
            obj.Fetch(ID);
            return obj;
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

        /// <summary>
        /// Fetches all the contacts from the database
        /// </summary>
        /// <returns></returns>
        protected static List<Contact> FetchAll()
        {
            List<Contact> AllObjects = new List<Contact>();

            if (Database.CheckDatabaseConnection())
            {
                Database.DatabaseConnectivityMode DatabaseConnectionMode = Database.ConnectionMode;
                PropertyInfo[] AllProperties = GetProperties();
                if (AllProperties != null)
                {
                    switch (DatabaseConnectionMode)
                    {

                        case Database.DatabaseConnectivityMode.Remote:
                            using (SqlConnection Connection = new SqlConnection(Database.ConnectionString))
                            {
                                Connection.Open();

                                if (Database.CheckDatabaseTableExists(MODULAR_DATABASE_TABLE))
                                {
                                    DatabaseUtils.CreateDatabaseTable(MODULAR_DATABASE_TABLE, AllProperties);
                                }

                                using (SqlCommand Command = new SqlCommand())
                                {
                                    Command.Connection = Connection;
                                    Command.CommandType = CommandType.Text;
                                    Command.CommandText = DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE);

                                    using (SqlDataReader DataReader = Command.ExecuteReader())
                                    {
                                        Contact obj = GetOrdinals(DataReader);

                                        while (DataReader.Read())
                                        {
                                            AllObjects.Add(obj);
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

                                if (Database.CheckDatabaseTableExists(MODULAR_DATABASE_TABLE))
                                {
                                    using (SqliteCommand Command = new SqliteCommand())
                                    {

                                        Command.Connection = Connection;
                                        Command.CommandType = CommandType.Text;
                                        Command.CommandText = DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE);

                                        using (SqliteDataReader DataReader = Command.ExecuteReader())
                                        {
                                            Contact obj = GetOrdinals(DataReader);

                                            while (DataReader.Read())
                                            {
                                                AllObjects.Add(obj);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    DatabaseUtils.CreateDatabaseTable(MODULAR_DATABASE_TABLE, AllProperties);
                                }
                                Connection.Close();
                            }
                            break;
                    }
                }
            }
            else
            {
                throw new ModularException(ExceptionType.DatabaseConnectionError, "There was an issue trying to connect to the database.");
            }

            return AllObjects;
        }

        protected static Contact GetOrdinals(SqlDataReader DataReader)
        {
            Contact obj = new Contact();

            PropertyInfo[] AllProperties = GetProperties();
            if (AllProperties != null)
            {
                obj.SetPropertyValues(AllProperties, DataReader);
            }

            return obj;
        }

        protected static Contact GetOrdinals(SqliteDataReader DataReader)
        {
            Contact obj = new Contact();

            PropertyInfo[] AllProperties = GetProperties();
            if (AllProperties != null)
            {
                obj.SetPropertyValues(AllProperties, DataReader);
            }

            return obj;
        }

        #endregion

    }
}
