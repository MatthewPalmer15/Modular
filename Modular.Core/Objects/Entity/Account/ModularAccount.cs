using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using Modular.Core.Databases;
using Modular.Core.System.Attributes;
using Modular.Core.Security;

namespace Modular.Core.Entity
{
    [Serializable]
    public class Account : ModularBase, IIdentity
    {

        #region "  Constructor  "

        public Account()
        {
        }

        #endregion

        #region "  Constants  "

        protected static readonly new string MODULAR_DATABASE_TABLE = "tbl_Modular_Sequence";

        #endregion

        #region "  Enums  "

        public enum LoginMethodType
        {
            Username,
            Email,
            Admin,
        }

        #endregion

        #region "  Variables  "

        private Guid _ContactID;

        private Guid _RoleID;

        private string _Username = String.Empty;

        private string _Password = String.Empty;

        #endregion

        #region "  Properties  "

        [Required]
        [Unique(ErrorMessage = "Contact already has an account.")]
        public Guid ContactID
        {
            get
            {
                return _ContactID;
            }
            set
            {
                if (_ContactID != value)
                {
                    _ContactID = value;
                    OnPropertyChanged("ContactID");
                }
            }
        }

        [Required]
        public Guid RoleID
        {
            get
            {
                return _RoleID;
            }
            set
            {
                if (_RoleID != value)
                {
                    _RoleID = value;
                    OnPropertyChanged("RoleID");
                }
            }
        }

        public Role Role
        {
            get
            {
                return Role.Load(RoleID);
            }
        }

        public Contact Contact
        {
            get
            {
                return Contact.Load(_ContactID);
            }
        }

        public string Name
        {
            get
            {
                return Contact.FullName;
            }
        }

        [Unique]
        public string Email
        {
            get
            {
                if (Contact != null)
                {
                    return Contact.Email;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        [Unique]
        [Required(ErrorMessage = "Username is required.")]
        [MinLength(3, ErrorMessage = "Username must be more than 3 characters.")]
        [MaxLength(50, ErrorMessage = "Username must be less than 50 characters.")]
        [Display(Name = "Username")]
        public string Username
        {
            get
            {
                return _Username;
            }
            set
            {
                if (_Username != value)
                {
                    _Username = value;
                    OnPropertyChanged("Username");
                }
            }
        }


        [Required(ErrorMessage = "Username is required.")]
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                string HashedValue = HashPassword(value);

                if (_Password != HashedValue)
                {
                    _Password = HashedValue;
                    OnPropertyChanged("Password");
                }
            }
        }

        public string AuthenticationType
        {
            get
            {
                return "ModularSecurity";
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return ID.Equals(Guid.Empty);
            }
        }

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Creates a new account, linked to the Contact.
        /// </summary>
        /// <param name="ContactID">The ID of the Contact.</param>
        /// <returns></returns>
        public static Account Create(Guid ContactID)
        {
            Account objAccount = Load(ContactID);
            if (objAccount != null)
            {
                return objAccount;
            }
            else
            {
                objAccount = new Account();
                objAccount.SetDefaultValues();
                objAccount.ContactID = ContactID;
                objAccount.Save();

                return objAccount;
            }
        }

        public static new Account Create()
        {
            Account objAccount = new Account();
            objAccount.SetDefaultValues();
            return objAccount;
        }

        /// <summary>
        /// Creates a new account, linked to the Contact.
        /// </summary>
        /// <param name="ContactID">The ID of the Contact.</param>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static Account Create(Guid ContactID, string Username, string Password)
        {
            Account obj = Load(ContactID);
            if (obj != null)
            {
                return obj;
            }
            else
            {
                obj = new Account()
                {
                    ID = Guid.Empty,
                    ContactID = ContactID,
                    Username = Username,
                    Password = HashPassword(Password),
                };
                obj.Save();

                return obj;
            }
        }

        public static new List<Account> LoadAll()
        {
            return FetchAll();
        }

        public static new Account Load(Guid ID)
        {
            Account obj = new Account();
            obj.Fetch(ID);
            return obj;
        }

        public static Account Load(string Credentials, LoginMethodType LoginMethod)
        {
            Account obj = new Account();

            switch (LoginMethod)
            {

                case LoginMethodType.Email:
                    obj.Fetch(GetProperty("Email"), Credentials);
                    break;

                case LoginMethodType.Username:
                    obj.Fetch(GetProperty("Username"), Credentials);
                    break;
            }

            return obj;
        }

        public static void Login(string Credentials, string Password)
        {
            bool isAuthenticated;
            Account objAccount = new Account();

            if (IsValidEmail(Credentials))
            {
                isAuthenticated = Authenticate(Credentials, Password, LoginMethodType.Email);
                if (isAuthenticated)
                {
                    objAccount = Load(Credentials, LoginMethodType.Email);
                }
            }
            else
            {
                isAuthenticated = Authenticate(Credentials, Password, LoginMethodType.Username);
                if (isAuthenticated)
                {
                    objAccount = Load(Credentials, LoginMethodType.Username);
                }
            }
            var principle = new GenericPrincipal(objAccount, null);
            Thread.CurrentPrincipal = principle;
        }

        public static void Logout()
        {
            Thread.CurrentPrincipal = new GenericPrincipal(new Account(), null);
        }


        // This is not a perfect solution, but will cover 99.9% of cases.
        public static bool IsValidEmail(string Email)
        {
            if (!String.IsNullOrEmpty(Email))
            {
                try
                {
                    var EmailRegexPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                    var match = Regex.Match(Email, EmailRegexPattern, RegexOptions.IgnoreCase, TimeSpan.FromSeconds(60));
                    return match.Success;
                }
                catch (RegexMatchTimeoutException)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static string HashPassword(string password)
        {
            SHA256 HashingAlgorithm = SHA256.Create();
            byte[] hashedPassword = HashingAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedPassword);
        }

        protected static bool Authenticate(string Credentials, string InputPassword, LoginMethodType LoginType)
        {
            bool IsAuthenticationSuccessful = false;

            if (Database.CheckDatabaseConnection())
            {
                PropertyInfo[] AllProperties = GetClassType()?.GetProperties(BindingFlags.Instance | BindingFlags.Public) ?? throw new ModularException(ExceptionType.NullObjectReturned, "Error getting properties");

                switch (Database.ConnectionMode)
                {
                    case Database.DatabaseConnectivityMode.Remote:
                        using (SqlConnection Connection = new SqlConnection(Database.ConnectionString))
                        {
                            Connection.Open();

                            if (!Database.CheckDatabaseExists(MODULAR_DATABASE_TABLE))
                            {
                                DatabaseQueryUtils.CreateNewTableQuery(MODULAR_DATABASE_TABLE, AllProperties);
                            }

                            using (SqlCommand Command = new SqlCommand())
                            {
                                Command.Connection = Connection;

                                switch (LoginType)
                                {
                                    case LoginMethodType.Email:
                                        if (Database.EnableStoredProcedures)
                                        {
                                            Command.CommandType = CommandType.StoredProcedure;
                                            Command.CommandText = "usp_Modular_Contact_Account_Login_ByEmail";
                                            Command.Parameters.AddWithValue("@Email", Credentials);
                                        }
                                        else
                                        {
                                            Command.CommandType = CommandType.Text;
                                            Command.CommandText = DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE, AllProperties);
                                            Command.Parameters.AddWithValue("@Email", Credentials);
                                        }
                                        break;

                                    case LoginMethodType.Username:
                                        if (Database.EnableStoredProcedures)
                                        {
                                            Command.CommandType = CommandType.StoredProcedure;
                                            Command.CommandText = "usp_Modular_Contact_Account_Login_ByUsername";
                                            Command.Parameters.AddWithValue("@Username", Credentials);
                                        }
                                        else
                                        {
                                            Command.CommandType = CommandType.Text;
                                            Command.CommandText = DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE, AllProperties);
                                            Command.Parameters.AddWithValue("@Username", Credentials);
                                        }
                                        break;
                                }

                                SqlDataReader DataReader = Command.ExecuteReader();
                                if (DataReader.HasRows)
                                {
                                    DataReader.Read();

                                    string AccountPassword = DataReader.GetString("Password");
                                    IsAuthenticationSuccessful = !string.IsNullOrEmpty(AccountPassword) && HashPassword(InputPassword) == AccountPassword;
                                }
                            }

                            Connection.Close();
                        }
                        break;


                    case Database.DatabaseConnectivityMode.Local:
                        break;


                }

                return IsAuthenticationSuccessful;
            }
            else
            {
                throw new ModularException(ExceptionType.DatabaseConnectionError, "Error connecting to database");
            }
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Username;
        }

        public bool IsInRole(string RoleName)
        {
            return Role != null && Role.Name.Trim().ToUpper() == RoleName.ToUpper();
        }

        #endregion

        #region "  Data Methods  "

        /// <summary>
        /// Fetches all the contacts from the database
        /// </summary>
        /// <returns></returns>
        protected static List<Account> FetchAll()
        {
            List<Account> AllObjects = new List<Account>();

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
                                        Account obj = GetOrdinals(DataReader);

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
                                            Account obj = GetOrdinals(DataReader);

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

        protected static Account GetOrdinals(SqlDataReader DataReader)
        {
            Account obj = new Account();

            PropertyInfo[] AllProperties = GetProperties();
            if (AllProperties != null)
            {
                obj.SetPropertyValues(AllProperties, DataReader);
            }

            return obj;
        }

        protected static Account GetOrdinals(SqliteDataReader DataReader)
        {
            Account obj = new Account();

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