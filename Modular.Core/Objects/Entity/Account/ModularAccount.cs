using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Modular.Core.Databases;
using Modular.Core.Security;
using Modular.Core.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;

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

        protected static readonly new string MODULAR_DATABASE_TABLE = "tbl_Modular_Account";
        protected static new readonly string MODULAR_DATABASE_STOREDPROCEDURE_PREFIX = "usp_Modular_Account";
        protected static new readonly Type MODULAR_OBJECTTYPE = typeof(Account);

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

        [Required]
        public Role Role
        {
            get
            {
                return Role.Load(_RoleID);
            }
            set
            {
                if (_RoleID != value.ID)
                {
                    _RoleID = value.ID;
                    OnPropertyChanged("RoleID");
                }
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
                objAccount.Contact.ID = ContactID;
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
        /// <param name="Contact">The Contact.</param>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static Account Create(Contact Contact, string Username, string Password)
        {
            Account obj = Load(Contact.ID);
            if (obj != null)
            {
                return obj;
            }
            else
            {
                obj = new Account()
                {
                    ID = Guid.Empty,
                    Contact = Contact,
                    Username = Username,
                    Password = HashPassword(Password),
                };
                obj.Save();

                return obj;
            }
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
                    obj.Fetch(Class.GetField("Email"), Credentials);
                    break;

                case LoginMethodType.Username:
                    obj.Fetch(Class.GetField("Username"), Credentials);
                    break;
            }

            return obj;
        }


        /// <summary>
        /// Loads all instances from the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ModularException"></exception>
        public static new List<Account> LoadList()
        {
            List<Account> AllAccounts = new List<Account>();

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
                                    Account obj = GetOrdinals(DataReader);
                                    while (DataReader.Read())
                                    {
                                        AllAccounts.Add(obj);
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
                                    Account obj = GetOrdinals(DataReader);

                                    while (DataReader.Read())
                                    {
                                        AllAccounts.Add(obj);
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

            return AllAccounts;
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
            byte[] hashedPassword = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedPassword);
        }

        protected static bool Authenticate(string Credentials, string InputPassword, LoginMethodType LoginType)
        {
            bool IsAuthenticationSuccessful = false;

            if (Database.CheckDatabaseConnection())
            {
                PropertyInfo[] AllProperties = Class.GetProperties();

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

        protected static Account GetOrdinals(SqlDataReader DataReader)
        {
            Account obj = new Account();
            obj.SetFieldValues(Class.GetFields(), DataReader);
            return obj;
        }

        protected static Account GetOrdinals(SqliteDataReader DataReader)
        {
            Account obj = new Account();
            obj.SetFieldValues(Class.GetFields(), DataReader);
            return obj;
        }

        #endregion

    }
}