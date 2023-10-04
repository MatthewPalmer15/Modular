using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Modular.Core.Databases;
using Modular.Core.Geo;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;

namespace Modular.Core.Entity
{
    public class Organisation : ModularBase
    {

        #region "  Constructors  "

        public Organisation()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Organisation";
        protected static new readonly string MODULAR_DATABASE_STOREDPROCEDURE_PREFIX = "usp_Modular_Organisation";
        protected static new readonly Type MODULAR_OBJECTTYPE = typeof(Organisation);

        #endregion

        #region "  Variables  "

        private string _Name = string.Empty;

        private string _Description = string.Empty;

        private string _RegistrationNumber = string.Empty;

        private Guid _OwnerID;

        private string _Email = string.Empty;

        private string _PhoneNumber = string.Empty;

        private string _Website = string.Empty;

        private string _FacebookLink = string.Empty;

        private string _InstagramLink = string.Empty;

        private string _TwitterLink = string.Empty;

        private string _LinkedInLink = string.Empty;

        private string _AddressLine1 = string.Empty;

        private string _AddressLine2 = string.Empty;

        private string _AddressLine3 = string.Empty;

        private string _AddressCity = string.Empty;

        private string _AddressCounty = string.Empty;

        private Guid _AddressCountryID;

        private string _AddressPostcode = string.Empty;

        #endregion

        #region "  Properties  "

        [Required(ErrorMessage = "Please enter a name for this organisation")]
        [Display(Name = "Name")]
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


        [Display(Name = "Description")]
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


        [Required(ErrorMessage = "Please enter your registration number")]
        [Display(Name = "Registration Number")]
        public string RegistrationNumber
        {
            get
            {
                return _RegistrationNumber;
            }
            set
            {
                if (_RegistrationNumber != value)
                {
                    _RegistrationNumber = value;
                    OnPropertyChanged("RegistrationNumber");
                }
            }
        }


        [Required(ErrorMessage = "Please select an owner for this organisation")]
        [Display(Name = "Owner")]
        public Contact Owner
        {
            get
            {
                return Contact.Load(_OwnerID);
            }
            set
            {
                if (_OwnerID != value.ID)
                {
                    _OwnerID = value.ID;
                    OnPropertyChanged("Owner");
                }
            }
        }


        [Display(Name = "Email")]
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


        [Display(Name = "Phone")]
        public string PhoneNumber
        {
            get
            {
                return _PhoneNumber;
            }
            set
            {
                if (_PhoneNumber != value)
                {
                    _PhoneNumber = value;
                    OnPropertyChanged("PhoneNumber");
                }
            }
        }


        [Display(Name = "Website")]
        public string Website
        {
            get
            {
                return _Website;
            }
            set
            {
                if (_Website != value)
                {
                    _Website = value;
                    OnPropertyChanged("Website");
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
                    OnPropertyChanged("AddressCountry");
                }
            }
        }


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


        [Display(Name = "Employee Count")]
        public int EmployeeCount
        {
            get
            {
                return Contact.LoadList().Count(Contact => Contact.Organisation.ID == ID);
            }
        }

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Creates a new instance with default values
        /// </summary>
        /// <returns>A new instance</returns>
        public static new Organisation Create()
        {
            Organisation obj = new Organisation();
            obj.SetDefaultValues();
            return obj;
        }


        /// <summary>
        /// Loads an existing instance from the database
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static new Organisation Load(Guid ID)
        {
            Organisation obj = new Organisation();
            obj.Fetch(ID);
            return obj;
        }


        /// <summary>
        /// Loads all instances from the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ModularException"></exception>
        public static new List<Organisation> LoadList()
        {
            List<Organisation> AllOrganisations = new List<Organisation>();

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
                                    Organisation obj = GetOrdinals(DataReader);
                                    while (DataReader.Read())
                                    {
                                        AllOrganisations.Add(obj);
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
                                    Organisation obj = GetOrdinals(DataReader);

                                    while (DataReader.Read())
                                    {
                                        AllOrganisations.Add(obj);
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

            return AllOrganisations;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Name;
        }

        public override Organisation Clone()
        {
            return Organisation.Load(ID);
        }

        #endregion

        #region "  Data Methods  "

        protected static Organisation GetOrdinals(SqlDataReader DataReader)
        {
            Organisation obj = new Organisation();
            obj.SetFieldValues(Class.GetFields(), DataReader);
            return obj;
        }

        protected static Organisation GetOrdinals(SqliteDataReader DataReader)
        {
            Organisation obj = new Organisation();
            obj.SetFieldValues(Class.GetFields(), DataReader);
            return obj;
        }

        #endregion

    }
}
