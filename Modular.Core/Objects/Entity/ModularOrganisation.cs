using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Reflection;
using Modular.Core.Databases;
using Modular.Core.Geo;

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


        public Guid OwnerID
        {
            get
            {
                return _OwnerID;
            }
            set
            {
                if (_OwnerID != value)
                {
                    _OwnerID = value;
                    OnPropertyChanged("OwnerID");
                }
            }
        }


        public Contact Owner
        {
            get
            {
                return Contact.Load(_OwnerID);
            }
        }

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

        public Country AddressCountry
        {
            get
            {
                return Country.Load(AddressCountryID);
            }
        }

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

        public int EmployeeCount
        {
            get
            {
                return Contact.LoadList().Count(Contact => Contact.OrganisationID == ID);
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

        public static new List<Organisation> LoadList()
        {
            return FetchAll();
        }

        public static new Organisation Load(Guid ID)
        {
            Organisation obj = new Organisation();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Name;
        }

        #endregion

        #region "  Data Methods  "

        /// <summary>
        /// Fetches all the contacts from the database
        /// </summary>
        /// <returns></returns>
        protected static List<Organisation> FetchAll()
        {
            List<Organisation> AllObjects = new List<Organisation>();

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
                                        Organisation obj = GetOrdinals(DataReader);

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
                                            Organisation obj = GetOrdinals(DataReader);

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

        protected static Organisation GetOrdinals(SqlDataReader DataReader)
        {
            Organisation obj = new Organisation();

            PropertyInfo[] AllProperties = GetProperties();
            if (AllProperties != null)
            {
                obj.SetPropertyValues(AllProperties, DataReader);
            }

            return obj;
        }

        protected static Organisation GetOrdinals(SqliteDataReader DataReader)
        {
            Organisation obj = new Organisation();

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
