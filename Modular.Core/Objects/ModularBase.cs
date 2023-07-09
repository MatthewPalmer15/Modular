using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlTypes;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Modular.Core
{
    public class ModularBase : ICloneable, INotifyPropertyChanged
    {

        #region "  Constructors  "

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <exception cref="ModularException">You cannot create a new instance of the Base Class.</exception>
        public ModularBase()
        {
            if (this.GetType() == typeof(ModularBase))
            {
                throw new ModularException(ExceptionType.BaseClassAccess, "Access denied to base class.");
            }
        }

        #endregion

        #region "  Constants  "

        protected static readonly string MODULAR_DATABASE_TABLE = "";
        protected static readonly string MODULAR_DATABASE_STOREDPROCEDURE_PREFIX = "";

        #endregion

        #region "  Variables  "

        private Guid _ID;

        private DateTime _CreatedDate;

        private Guid _CreatedBy;

        private DateTime _ModifiedDate;

        private Guid _ModifiedBy;

        private bool _IsDeleted;

        //private DateTime _DeletedDate;
        //
        //private DateTime _DeletedBy;

        private bool _IsFlagged;

        //private DateTime _FlaggedDate;
        //
        //private Guid _FlaggedBy;

        #endregion

        #region "  Properties  "

        [Key]
        [Unique(ErrorMessage = "ID must be unique.")]
        [Required(ErrorMessage = "ID cannot be null.")]
        [Display(Name = "ID")]
        public Guid ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (_ID != value)
                {
                    _ID = value;
                    OnPropertyChanged("ID");
                }
            }
        }


        [Required(ErrorMessage = "Created Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Date must be in format DD/MM/YYYY.")]
        [Display(Name = "Created Date"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate
        {
            get
            {
                return _CreatedDate;
            }
            set
            {
                if (_CreatedDate != value)
                {
                    _CreatedDate = value;
                    OnPropertyChanged("CreatedDate");
                }
            }
        }


        [Required(ErrorMessage = "Created By is required.")]
        [Display(Name = "Created By")]
        public Guid CreatedBy
        {
            get
            {
                return _CreatedBy;
            }
            set
            {
                if (_CreatedBy != value)
                {
                    _CreatedBy = value;
                    OnPropertyChanged("CreatedBy");
                }
            }
        }


        [Required(ErrorMessage = "Modified Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Date must be in format DD/MM/YYYY.")]
        [Display(Name = "Updated Date"), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ModifiedDate
        {
            get
            {
                return _ModifiedDate;
            }
            set
            {
                if (_ModifiedDate != value)
                {
                    _ModifiedDate = value;
                    OnPropertyChanged("ModifiedDate");
                }
            }
        }


        [Required(ErrorMessage = "Modified By is required.")]
        [Display(Name = "Modified By")]
        public Guid ModifiedBy
        {
            get
            {
                return _ModifiedBy;
            }
            set
            {
                if (_ModifiedBy != value)
                {
                    _ModifiedBy = value;
                    OnPropertyChanged("ModifedBy");
                }
            }
        }

        [Ignore]
        [DefaultValue(false)]
        public bool IsDeleted
        {
            get
            {
                return _IsDeleted;
            }
            set
            {
                if (_IsDeleted != value)
                {
                    _IsDeleted = value;
                    OnPropertyChanged("IsDeleted");
                }
            }
        }

        // TODO: Add support for database triggers to set this value.
        // It should check if the record is orphaned, and if so, set this value to true.
        // This should help identify orphaned records, and allow them to be cleaned up.
        [SystemManaged]
        public bool IsFlagged
        {
            get
            {
                return _IsFlagged;
            }
        }

        [Ignore]
        [DefaultValue(false)]
        public bool IsNew { get; set; }

        [Ignore]
        [DefaultValue(false)]
        public bool IsModified { get; set; }

        // This gets all the properties, and checks if the data annotation restrictions are met.
        public bool IsValid
        {
            get
            {
                var properties = GetType().GetProperties();
                return properties.All(prop =>
                {
                    var attrs = prop.GetCustomAttributes(true);
                    return attrs.OfType<ValidationAttribute>().All(attr =>
                    {
                        var value = prop.GetValue(attr);
                        return attr.IsValid(value);
                    });
                });
            }
        }

        public string ValidationMessage
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                IEnumerable<string> AllErrors = ValidationErrors;
                foreach (var Error in AllErrors)
                {
                    sb.Append(Error);
                    sb.Append(Environment.NewLine);
                }

                return sb.ToString();
            }
        }

        private IEnumerable<string> ValidationErrors
        {
            get
            {
                var properties = GetType().GetProperties();
                var errorMessages = new List<string>();

                foreach (var prop in properties)
                {
                    var attrs = prop.GetCustomAttributes(true);

                    foreach (var attr in attrs.OfType<ValidationAttribute>())
                    {
                        var value = prop.GetValue(this);

                        if (!attr.IsValid(value) && !String.IsNullOrEmpty(attr.ErrorMessage))
                        {
                            errorMessages.Add(attr.ErrorMessage);
                        }
                    }
                }

                return errorMessages;
            }
        }

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Creates a new instance of the object (With Default Values).
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ModularException"></exception>
        protected static ModularBase Create()
        {
            throw new ModularException(ExceptionType.BaseClassAccess, "Access denied to base class.");
        }

        /// <summary>
        /// Loads all instances of the object.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ModularException"></exception>
        protected static List<ModularBase> LoadInstances()
        {
            throw new ModularException(ExceptionType.BaseClassAccess, "Access denied to base class.");
        }

        /// <summary>
        /// Loads an instance of the object.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ModularException"></exception>
        protected static ModularBase Load(Guid ID)
        {
            throw new ModularException(ExceptionType.BaseClassAccess, "Access denied to base class.");
        }

        /// <summary>
        /// Checks to see if instance already exists
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool Exists(Guid ID)
        {
            object obj = Load(ID);
            return obj != null;
        }


        public virtual ModularBase Clone()
        {
            throw new ModularException(ExceptionType.BaseClassAccess, "Access denied to base class.");
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        #endregion

        #region "  Instance Methods  "

        /// <summary>
        /// Saves the current instance. It will either insert, or update it into the database.
        /// </summary>
        public void Save()
        {
            if (IsModified && IsValid)
            {
                if (IsNew)
                {
                    CreatedDate = DateTime.Now;
                    CreatedBy = ModularSystem.Context.Identity.ID;
                    ModifiedDate = DateTime.Now;
                    ModifiedBy = ModularSystem.Context.Identity.ID;

                    Insert();
                    IsModified = false;
                    IsNew = false;
                }
                else
                {
                    ModifiedDate = DateTime.Now;
                    ModifiedBy = ModularSystem.Context.Identity.ID;

                    Update();
                    IsModified = false;
                }
            }
        }

        /// <summary>
        /// Marks the current instance for deletion.
        /// </summary>
        public virtual void MarkForDeletion()
        {
            if (!IsNew)
            {
                IsDeleted = true;
            }
        }

        /// <summary>
        /// Reloads the current instance from the database.
        /// </summary>
        public void Reload()
        {
            Fetch(ID);
            IsModified = false;
            IsNew = false;
        }

        #endregion

        #region "  Data Methods  "

        /// <summary>
        /// Function which fetches the data from the database, using the specified ID.
        /// </summary>
        /// <param name="ID"></param>
        public void Fetch(Guid ID)
        {
            // Check if the database can be connected to.
            if (Database.CheckDatabaseConnection())
            {
                PropertyInfo[] AllProperties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

                // If table does not exist within the database, create it.
                if (!Database.CheckDatabaseTableExists(MODULAR_DATABASE_TABLE))
                {
                    DatabaseUtils.CreateDatabaseTable(MODULAR_DATABASE_TABLE, AllProperties);
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
                                DatabaseUtils.CreateStoredProcedure(DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE, GetProperty("ID")));
                            }

                            using (SqlCommand Command = new SqlCommand())
                            {

                                Command.Connection = Connection;

                                // If stored procedures are enabled, use the stored procedure, otherwise use a query.
                                Command.CommandType = Database.EnableStoredProcedures ? CommandType.StoredProcedure : CommandType.Text;
                                Command.CommandText = Database.EnableStoredProcedures ? StoredProcedureName : DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE, GetProperty("ID"));

                                Command.Parameters.Add(new SqlParameter($"@ID", ID));

                                SqlDataReader DataReader = Command.ExecuteReader();

                                // If data was returned, set the property values.
                                if (DataReader.HasRows)
                                {
                                    DataReader.Read();
                                    SetPropertyValues(AllProperties, DataReader);
                                }
                            }

                            Connection.Close();
                        }
                        break;

                    // If the database is a local database, connect to it.
                    case Database.DatabaseConnectivityMode.Local:
                        using (SqliteConnection Connection = new SqliteConnection(Database.ConnectionString))
                        {
                            Connection.Open();

                            using (SqliteCommand Command = new SqliteCommand())
                            {
                                Command.Connection = Connection;

                                // Stored procedures are not supported in SQLite, so use a query.
                                Command.CommandType = CommandType.Text;
                                Command.CommandText = DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE, GetProperty("ID"));

                                Command.Parameters.Add(new SqliteParameter($"@ID", ID));

                                SqliteDataReader DataReader = Command.ExecuteReader();

                                // If data was returned, set the property values.
                                if (DataReader.HasRows)
                                {
                                    DataReader.Read();
                                    SetPropertyValues(AllProperties, DataReader);
                                }
                            }

                            Connection.Close();
                        }
                        break;

                    // If the database connection mode was not defined, throw an exception.
                    default:
                        throw new ModularException(ExceptionType.DatabaseConnectivityNotDefined, "Database Connection Mode was not defined.");
                }
            }
            else
            {
                throw new ModularException(ExceptionType.DatabaseConnectionError, $"There was an issue trying to connect to the database.");
            }
        }

        /// <summary>
        /// Function which fetches the data from the database, using the specified ID.
        /// </summary>
        /// <param name="Value"></param>
        public void Fetch(string Value)
        {
            // Check if the database can be connected to.
            if (Database.CheckDatabaseConnection())
            {
                PropertyInfo[] AllProperties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

                // Gets the property which has the Identifier attribute.
                PropertyInfo Property = AllProperties.FirstOrDefault(Property => Property.GetCustomAttributes(typeof(IdentifierAttribute), false).Length > 0);
                if (Property == null)
                {
                    return;
                }

                // If table does not exist within the database, create it.
                if (!Database.CheckDatabaseTableExists(MODULAR_DATABASE_TABLE))
                {
                    DatabaseUtils.CreateDatabaseTable(MODULAR_DATABASE_TABLE, AllProperties);
                }

                switch (Database.ConnectionMode)
                {
                    // If the database is a remote database, connect to it.
                    case Database.DatabaseConnectivityMode.Remote:
                        using (SqlConnection Connection = new SqlConnection(Database.ConnectionString))
                        {
                            Connection.Open();

                            string StoredProcedureName = $"{MODULAR_DATABASE_STOREDPROCEDURE_PREFIX}_FetchBy{Property.Name}";

                            // If stored procedures are enabled, and the stored procedure does not exist, create it.
                            if (Database.EnableStoredProcedures && !Database.CheckStoredProcedureExists(StoredProcedureName))
                            {
                                DatabaseUtils.CreateStoredProcedure(DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE, Property));
                            }

                            using (SqlCommand Command = new SqlCommand())
                            {

                                Command.Connection = Connection;

                                // If stored procedures are enabled, use the stored procedure, otherwise use a query.
                                Command.CommandType = Database.EnableStoredProcedures ? CommandType.StoredProcedure : CommandType.Text;
                                Command.CommandText = Database.EnableStoredProcedures ? StoredProcedureName : DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE, Property);

                                Command.Parameters.Add(new SqlParameter($"@{Property.Name}", Value));

                                SqlDataReader DataReader = Command.ExecuteReader();

                                // If data was returned, set the property values.
                                if (DataReader.HasRows)
                                {
                                    DataReader.Read();
                                    SetPropertyValues(AllProperties, DataReader);
                                }
                            }

                            Connection.Close();
                        }
                        break;

                    // If the database is a local database, connect to it.
                    case Database.DatabaseConnectivityMode.Local:
                        using (SqliteConnection Connection = new SqliteConnection(Database.ConnectionString))
                        {
                            Connection.Open();

                            using (SqliteCommand Command = new SqliteCommand())
                            {
                                Command.Connection = Connection;

                                // Stored procedures are not supported in SQLite, so use a query.
                                Command.CommandType = CommandType.Text;
                                Command.CommandText = DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE, GetProperty("ID"));

                                Command.Parameters.Add(new SqliteParameter($"@ID", ID));

                                SqliteDataReader DataReader = Command.ExecuteReader();

                                // If data was returned, set the property values.
                                if (DataReader.HasRows)
                                {
                                    DataReader.Read();
                                    SetPropertyValues(AllProperties, DataReader);
                                }
                            }

                            Connection.Close();
                        }
                        break;

                    // If the database connection mode was not defined, throw an exception.
                    default:
                        throw new ModularException(ExceptionType.DatabaseConnectivityNotDefined, "Database Connection Mode was not defined.");
                }
            }
            else
            {
                throw new ModularException(ExceptionType.DatabaseConnectionError, $"There was an issue trying to connect to the database.");
            }
        }

        /// <summary>
        /// Function which fetches the data from the database, using the specified property and value.
        /// </summary>
        /// <param name="Property"></param>
        /// <param name="Value"></param>
        /// <exception cref="ModularException"></exception>
        public void Fetch(PropertyInfo Property, string Value)
        {
            // Check if the database can be connected to.
            if (Database.CheckDatabaseConnection())
            {
                PropertyInfo[] AllProperties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

                // If table does not exist within the database, create it.
                if (!Database.CheckDatabaseTableExists(MODULAR_DATABASE_TABLE))
                {
                    DatabaseUtils.CreateDatabaseTable(MODULAR_DATABASE_TABLE, AllProperties);
                }

                switch (Database.ConnectionMode)
                {
                    // If the database is a remote database, connect to it.
                    case Database.DatabaseConnectivityMode.Remote:
                        using (SqlConnection Connection = new SqlConnection(Database.ConnectionString))
                        {
                            Connection.Open();

                            string StoredProcedureName = $"{MODULAR_DATABASE_STOREDPROCEDURE_PREFIX}_FetchBy{Property.Name}";

                            // If stored procedures are enabled, and the stored procedure does not exist, create it.
                            if (Database.EnableStoredProcedures && !Database.CheckStoredProcedureExists(StoredProcedureName))
                            {
                                DatabaseUtils.CreateStoredProcedure(DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE, Property));
                            }

                            using (SqlCommand Command = new SqlCommand())
                            {

                                Command.Connection = Connection;

                                // If stored procedures are enabled, use the stored procedure, otherwise use a query.
                                Command.CommandType = Database.EnableStoredProcedures ? CommandType.StoredProcedure : CommandType.Text;
                                Command.CommandText = Database.EnableStoredProcedures ? StoredProcedureName : DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE, Property);

                                Command.Parameters.Add(new SqlParameter($"@{Property.Name}", Value));

                                SqlDataReader DataReader = Command.ExecuteReader();

                                // If data was returned, set the property values.
                                if (DataReader.HasRows)
                                {
                                    DataReader.Read();
                                    SetPropertyValues(AllProperties, DataReader);
                                }
                            }

                            Connection.Close();
                        }
                        break;

                    // If the database is a local database, connect to it.
                    case Database.DatabaseConnectivityMode.Local:
                        using (SqliteConnection Connection = new SqliteConnection(Database.ConnectionString))
                        {
                            Connection.Open();

                            using (SqliteCommand Command = new SqliteCommand())
                            {
                                Command.Connection = Connection;

                                // Stored procedures are not supported in SQLite, so use a query.
                                Command.CommandType = CommandType.Text;
                                Command.CommandText = DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE, Property);

                                Command.Parameters.Add(new SqliteParameter($"@{Property.Name}", Value));

                                SqliteDataReader DataReader = Command.ExecuteReader();

                                // If data was returned, set the property values.
                                if (DataReader.HasRows)
                                {
                                    DataReader.Read();
                                    SetPropertyValues(AllProperties, DataReader);
                                }
                            }

                            Connection.Close();
                        }
                        break;

                    // If the database connection mode was not defined, throw an exception.
                    default:
                        throw new ModularException(ExceptionType.DatabaseConnectivityNotDefined, "Database Connection Mode was not defined.");
                }
            }
            else
            {
                throw new ModularException(ExceptionType.DatabaseConnectionError, $"There was an issue trying to connect to the database.");
            }

        }

        /// <summary>
        /// Function which inserts a record into the database, using the object.
        /// </summary>
        /// <exception cref="ModularException"></exception>
        protected virtual void Insert()
        {
            // Check if the database can be connected to.
            if (Database.CheckDatabaseConnection())
            {
                PropertyInfo[] AllProperties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

                // If table does not exist within the database, create it.
                if (!Database.CheckDatabaseExists(MODULAR_DATABASE_TABLE))
                {
                    DatabaseUtils.CreateDatabaseTable(MODULAR_DATABASE_TABLE, AllProperties);
                }

                switch (Database.ConnectionMode)
                {
                    // If the database is a remote database, connect to it.
                    case Database.DatabaseConnectivityMode.Remote:
                        using (SqlConnection Connection = new SqlConnection(Database.ConnectionString))
                        {
                            Connection.Open();

                            string StoredProcedureName = $"{MODULAR_DATABASE_STOREDPROCEDURE_PREFIX}_Insert";

                            // If stored procedures are enabled, and the stored procedure does not exist, create it.
                            if (Database.EnableStoredProcedures && !Database.CheckStoredProcedureExists(StoredProcedureName))
                            {
                                DatabaseUtils.CreateStoredProcedure(DatabaseQueryUtils.CreateInsertQuery(MODULAR_DATABASE_TABLE, AllProperties));
                            }

                            using (SqlCommand Command = new SqlCommand())
                            {
                                Command.Connection = Connection;

                                // If stored procedures are enabled, use the stored procedure, otherwise use a query.
                                Command.CommandType = Database.EnableStoredProcedures ? CommandType.StoredProcedure : CommandType.Text;
                                Command.CommandText = Database.EnableStoredProcedures ? StoredProcedureName : DatabaseQueryUtils.CreateInsertQuery(MODULAR_DATABASE_TABLE, AllProperties);

                                // Add all the properties as parameters.
                                foreach (PropertyInfo Property in AllProperties)
                                {
                                    if (Property.CanWrite)
                                    {
                                        SqlParameter Parameter = new SqlParameter($"@{Property.Name}", Property.GetValue(this));
                                        Command.Parameters.Add(Parameter);
                                    }
                                }

                                Command.ExecuteNonQuery();
                            }

                            Connection.Close();
                        }
                        break;

                    // If the database is a local database, connect to it.
                    case Database.DatabaseConnectivityMode.Local:
                        using (SqliteConnection Connection = new SqliteConnection(Database.ConnectionString))
                        {
                            Connection.Open();

                            using (SqliteCommand Command = new SqliteCommand())
                            {
                                Command.Connection = Connection;

                                // Stored procedures are not supported in SQLite, so use a query.
                                Command.CommandType = CommandType.Text;
                                Command.CommandText = DatabaseQueryUtils.CreateInsertQuery(MODULAR_DATABASE_TABLE, AllProperties);

                                // Add all the properties as parameters.
                                foreach (PropertyInfo Property in AllProperties)
                                {
                                    if (Property.CanWrite)
                                    {
                                        SqlParameter Parameter = new SqlParameter($"@{Property.Name}", Property.GetValue(this));
                                        Command.Parameters.Add(Parameter);
                                    }
                                }

                                Command.ExecuteNonQuery();
                            }

                            Connection.Close();
                        }
                        break;

                    // If the database connection mode was not defined, throw an exception.
                    default:
                        throw new ModularException(ExceptionType.DatabaseConnectivityNotDefined, "Database Connection Mode was not defined.");
                }
            }
            else
            {
                throw new ModularException(ExceptionType.DatabaseConnectionError, $"There was an issue trying to connect to the database.");
            }
        }


        /// <summary>
        /// Function which updates a record in the database, using the object.
        /// </summary>
        /// <exception cref="ModularException"></exception>
        protected virtual void Update()
        {
            // Check if the database can be connected to.
            if (Database.CheckDatabaseConnection())
            {
                PropertyInfo[] AllProperties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

                // If table does not exist within the database, create it.
                if (!Database.CheckDatabaseTableExists(MODULAR_DATABASE_TABLE))
                {
                    DatabaseUtils.CreateDatabaseTable(MODULAR_DATABASE_TABLE, AllProperties);
                }

                switch (Database.ConnectionMode)
                {
                    // If the database is a remote database, connect to it.
                    case Database.DatabaseConnectivityMode.Remote:
                        using (SqlConnection Connection = new SqlConnection(Database.ConnectionString))
                        {
                            Connection.Open();

                            string StoredProcedureName = $"{MODULAR_DATABASE_STOREDPROCEDURE_PREFIX}_Update";

                            // If stored procedures are enabled, and the stored procedure does not exist, create it.
                            if (Database.EnableStoredProcedures && !Database.CheckStoredProcedureExists(StoredProcedureName))
                            {
                                DatabaseUtils.CreateStoredProcedure(DatabaseQueryUtils.CreateUpdateQuery(MODULAR_DATABASE_TABLE, AllProperties));
                            }

                            using (SqlCommand Command = new SqlCommand())
                            {
                                Command.Connection = Connection;

                                // If stored procedures are enabled, use the stored procedure, otherwise use a query.
                                Command.CommandType = Database.EnableStoredProcedures ? CommandType.StoredProcedure : CommandType.Text;
                                Command.CommandText = Database.EnableStoredProcedures ? StoredProcedureName : DatabaseQueryUtils.CreateUpdateQuery(MODULAR_DATABASE_TABLE, AllProperties);

                                // Add all the properties as parameters.
                                foreach (PropertyInfo Property in AllProperties)
                                {
                                    if (Property.CanWrite)
                                    {
                                        SqlParameter Parameter = new SqlParameter($"@{Property.Name}", Property.GetValue(this));
                                        Command.Parameters.Add(Parameter);
                                    }
                                }

                                Command.ExecuteNonQuery();
                            }

                            Connection.Close();
                        }
                        break;

                    // If the database is a local database, connect to it.
                    case Database.DatabaseConnectivityMode.Local:
                        using (SqliteConnection Connection = new SqliteConnection(Database.ConnectionString))
                        {
                            Connection.Open();

                            using (SqliteCommand Command = new SqliteCommand())
                            {
                                Command.Connection = Connection;

                                // Stored procedures are not supported in SQLite, so use a query.
                                Command.CommandType = CommandType.Text;
                                Command.CommandText = DatabaseQueryUtils.CreateUpdateQuery(MODULAR_DATABASE_TABLE, AllProperties);

                                // Add all the properties as parameters.
                                foreach (PropertyInfo Property in AllProperties)
                                {
                                    if (Property.CanWrite)
                                    {
                                        SqliteParameter Parameter = new SqliteParameter($"@{Property.Name}", Property.GetValue(this));
                                        Command.Parameters.Add(Parameter);
                                    }
                                }

                                Command.ExecuteNonQuery();
                            }

                            Connection.Close();
                        }
                        break;

                    // If the database connection mode was not defined, throw an exception.
                    default:
                        throw new ModularException(ExceptionType.DatabaseConnectivityNotDefined, "Database Connection Mode was not defined.");
                }
            }
            else
            {
                throw new ModularException(ExceptionType.DatabaseConnectionError, $"There was an issue trying to connect to the database.");
            }
        }

        /// <summary>
        /// Function which updates a record in the database, using the object.
        /// </summary>
        /// <exception cref="ModularException"></exception>
        public virtual void Delete()
        {
            // Check if the instance is not new. If it is new, it means it has not been saved to the database yet.
            if (!IsNew)
            {
                if (Database.CheckDatabaseConnection())
                {
                    PropertyInfo[] AllProperties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

                    // If table does not exist within the database, create it.
                    if (!Database.CheckDatabaseTableExists(MODULAR_DATABASE_TABLE))
                    {
                        DatabaseUtils.CreateDatabaseTable(MODULAR_DATABASE_TABLE, AllProperties);
                    }

                    switch (Database.ConnectionMode)
                    {
                        // If the database is a remote database, connect to it.
                        case Database.DatabaseConnectivityMode.Remote:
                            using (SqlConnection Connection = new SqlConnection(Database.ConnectionString))
                            {
                                Connection.Open();

                                string StoredProcedureName = $"{MODULAR_DATABASE_STOREDPROCEDURE_PREFIX}_Delete";

                                // If stored procedures are enabled, and the stored procedure does not exist, create it.
                                if (Database.EnableStoredProcedures && !Database.CheckStoredProcedureExists(StoredProcedureName))
                                {
                                    DatabaseUtils.CreateStoredProcedure(DatabaseQueryUtils.CreateDeleteQuery(MODULAR_DATABASE_TABLE, GetProperty("ID")));
                                }

                                using (SqlCommand Command = new SqlCommand())
                                {
                                    Command.Connection = Connection;

                                    // If stored procedures are enabled, use the stored procedure, otherwise use a query.
                                    Command.CommandType = Database.EnableStoredProcedures ? CommandType.StoredProcedure : CommandType.Text;
                                    Command.CommandText = Database.EnableStoredProcedures ? StoredProcedureName : DatabaseQueryUtils.CreateDeleteQuery(MODULAR_DATABASE_TABLE, GetProperty("ID"));

                                    Command.Parameters.AddWithValue("@ID", ID);

                                    Command.ExecuteNonQuery();
                                }

                                Connection.Close();
                            }
                            break;

                        // If the database is a local database, connect to it.  
                        case Database.DatabaseConnectivityMode.Local:
                            using (SqliteConnection Connection = new SqliteConnection(Database.ConnectionString))
                            {
                                Connection.Open();

                                using (SqliteCommand Command = new SqliteCommand())
                                {
                                    Command.Connection = Connection;

                                    // Stored procedures are not supported in SQLite, so use a query.
                                    Command.CommandType = CommandType.Text;
                                    Command.CommandText = DatabaseQueryUtils.CreateDeleteQuery(MODULAR_DATABASE_TABLE, GetProperty("ID"));

                                    Command.Parameters.AddWithValue("@ID", ID);

                                    Command.ExecuteNonQuery();
                                }

                                Connection.Close();
                            }
                            break;

                        // If the database connection mode was not defined, throw an exception.
                        default:
                            throw new ModularException(ExceptionType.DatabaseConnectivityNotDefined, "Database Connection Mode was not defined.");
                    }
                }
                else
                {
                    throw new ModularException(ExceptionType.DatabaseConnectionError, $"There was an issue trying to connect to the database.");
                }
            }
        }

        #endregion

        #region "  Object Methods  "

        /// <summary>
        /// Sets all properties within the class to the default values
        /// </summary>
        public virtual void SetDefaultValues()
        {
            SetDefaultValues(false);
        }

        /// <summary>
        /// Sets all properties within the class to the default values
        /// </summary>
        /// <param name="Override">
        /// Overrides any current data within the object.
        /// </param>
        public virtual void SetDefaultValues(bool Override)
        {
            // Get all Properties within the class that are instance-based and public
            PropertyInfo[] AllProperties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo Property in AllProperties)
            {
                // If the current field is null, or properties are allowed to be override, we set a default value
                if ((Equals(Property.GetValue(this), null) || Override) && Property.CanWrite)
                {
                    // DataType: Enums
                    if (Property.PropertyType.IsEnum)
                    {
                        Property.SetValue(this, Enum.ToObject(Property.PropertyType, 0));
                    }

                    // DataType: XML Document
                    else if (Property.PropertyType == typeof(XmlDocument))
                    {
                        Property.SetValue(this, new XmlDocument());
                    }

                    // DataType: XML
                    else if (Property.PropertyType == typeof(SqlXml))
                    {
                        Property.SetValue(this, new SqlXml());
                    }

                    // DataType: String
                    else if (Property.PropertyType == typeof(string))
                    {
                        Property.SetValue(this, string.Empty);
                    }

                    // DataType: Character
                    else if (Property.PropertyType == typeof(char))
                    {
                        Property.SetValue(this, char.MinValue);
                    }

                    // DataType: Long (Int64) Signed & Unsigned
                    else if (Property.PropertyType == typeof(long) || Property.PropertyType == typeof(ulong))
                    {
                        Property.SetValue(this, 0);
                    }

                    // DataType: Integer (Int32) Signed & Unsigned
                    else if (Property.PropertyType == typeof(int) || Property.PropertyType == typeof(uint))
                    {
                        Property.SetValue(this, 0);
                    }

                    // DataType: Short (Int16) Signed & Unsigned
                    else if (Property.PropertyType == typeof(short) || Property.PropertyType == typeof(ushort))
                    {
                        Property.SetValue(this, 0);
                    }

                    // DataType: Byte (Int8) Signed & Unsigned
                    else if (Property.PropertyType == typeof(byte) || Property.PropertyType == typeof(sbyte))
                    {
                        Property.SetValue(this, 0);
                    }

                    // DataType: Float (32-bit)
                    else if (Property.PropertyType == typeof(float))
                    {
                        Property.SetValue(this, 0);
                    }

                    // DataType: Double (64-bit)
                    else if (Property.PropertyType == typeof(double))
                    {
                        Property.SetValue(this, 0);
                    }

                    // DataType: Decimal (128-bit)
                    else if (Property.PropertyType == typeof(decimal))
                    {
                        Property.SetValue(this, 0);
                    }

                    // DataType: Guid
                    else if (Property.PropertyType == typeof(Guid))
                    {
                        bool HasKeyAttributes = Property.GetCustomAttributes(typeof(KeyAttribute), false).Length > 0;
                        if (HasKeyAttributes)
                        {
                            if (IsNew)
                            {
                                Property.SetValue(this, Guid.NewGuid());
                            }
                        }
                        else
                        {
                            Property.SetValue(this, Guid.Empty);
                        }
                    }

                    // DataType: DateTime
                    else if (Property.PropertyType == typeof(DateTime))
                    {
                        Property.SetValue(this, DateTime.MinValue);
                    }

                    // DataType: DateOnly
                    else if (Property.PropertyType == typeof(DateOnly))
                    {
                        Property.SetValue(this, DateOnly.MinValue);
                    }

                    // DataType: TimeOnly
                    else if (Property.PropertyType == typeof(TimeOnly))
                    {
                        Property.SetValue(this, TimeOnly.MinValue);
                    }

                    // DataType: TimeSpan
                    else if (Property.PropertyType == typeof(TimeSpan))
                    {
                        Property.SetValue(this, TimeSpan.MinValue);
                    }

                    // DataType: Boolean
                    else if (Property.PropertyType == typeof(bool))
                    {
                        Property.SetValue(this, false);
                    }

                    else
                    {
                        throw new ModularException(ExceptionType.DataTypeNotSupported, $"DataType {Property.PropertyType} is not supported.");
                    }
                }

            }
        }

        protected void SetPropertyValues(PropertyInfo[] AllProperties, SqlDataReader DataReader)
        {
            foreach (PropertyInfo Property in AllProperties)
            {
                int PropertyOrdinal = DataReader.GetOrdinal(Property.Name);

                if (!DataReader.IsDBNull(PropertyOrdinal))
                {
                    // DataType: Enums
                    if (Property.PropertyType.IsEnum)
                    {
                        Type EnumType = Property.PropertyType;
                        Property.SetValue(this, Enum.ToObject(EnumType, DataReader.GetInt32(PropertyOrdinal)));
                    }

                    // DataType: XML Document
                    else if (Property.PropertyType == typeof(XmlDocument))
                    {
                        SqlXml SQLXML = DataReader.GetSqlXml(PropertyOrdinal);
                        XmlDocument XMLDocument = new XmlDocument();
                        XMLDocument.Load(SQLXML.CreateReader());
                        Property.SetValue(this, XMLDocument);
                    }

                    // DataType: XML
                    else if (Property.PropertyType == typeof(SqlXml))
                    {
                        Property.SetValue(this, DataReader.GetSqlXml(PropertyOrdinal));
                    }

                    // DataType: String
                    else if (Property.PropertyType == typeof(string))
                    {
                        Property.SetValue(this, DataReader.GetString(PropertyOrdinal));
                    }

                    // DataType: Character
                    else if (Property.PropertyType == typeof(char))
                    {
                        Property.SetValue(this, DataReader.GetChar(PropertyOrdinal));
                    }

                    // DataType: Long (Int64) Signed
                    else if (Property.PropertyType == typeof(long))
                    {
                        Property.SetValue(this, DataReader.GetInt64(PropertyOrdinal));
                    }

                    // DataType: Long (Int64) Unsigned
                    else if (Property.PropertyType == typeof(ulong))
                    {
                        Property.SetValue(this, DataReader.GetFieldValue<ulong>(PropertyOrdinal));
                    }

                    // DataType: Integer (Int32) Signed
                    else if (Property.PropertyType == typeof(int))
                    {
                        Property.SetValue(this, DataReader.GetInt32(PropertyOrdinal));
                    }

                    // DataType: Integer (Int32) Unsigned
                    else if (Property.PropertyType == typeof(uint))
                    {
                        Property.SetValue(this, DataReader.GetFieldValue<uint>(PropertyOrdinal));
                    }

                    // DataType: Short (Int16) Signed
                    else if (Property.PropertyType == typeof(short))
                    {
                        Property.SetValue(this, DataReader.GetInt16(PropertyOrdinal));
                    }

                    // DataType: Short (Int16) Unsigned
                    else if (Property.PropertyType == typeof(ushort))
                    {
                        Property.SetValue(this, DataReader.GetFieldValue<ushort>(PropertyOrdinal));
                    }

                    // DataType: Byte (Int8) Signed
                    else if (Property.PropertyType == typeof(byte))
                    {
                        Property.SetValue(this, DataReader.GetByte(PropertyOrdinal));
                    }

                    // DataType: Byte (Int8) Unsigned
                    else if (Property.PropertyType == typeof(sbyte))
                    {
                        Property.SetValue(this, DataReader.GetFieldValue<sbyte>(PropertyOrdinal));
                    }

                    // DataType: Float (32-bit)
                    else if (Property.PropertyType == typeof(float))
                    {
                        Property.SetValue(this, DataReader.GetFloat(PropertyOrdinal));
                    }

                    // DataType: Double (64-bit)
                    else if (Property.PropertyType == typeof(double))
                    {
                        Property.SetValue(this, DataReader.GetDouble(PropertyOrdinal));
                    }

                    // DataType: Decimal (128-bit)
                    else if (Property.PropertyType == typeof(decimal))
                    {
                        Property.SetValue(this, DataReader.GetDecimal(PropertyOrdinal));
                    }

                    // DataType: Guid
                    else if (Property.PropertyType == typeof(Guid))
                    {
                        Property.SetValue(this, DataReader.GetGuid(PropertyOrdinal));
                    }

                    // DataType: DateTime
                    else if (Property.PropertyType == typeof(DateTime))
                    {
                        Property.SetValue(this, DataReader.GetDateTime(PropertyOrdinal));
                    }

                    // DataType: DateOnly
                    else if (Property.PropertyType == typeof(DateOnly))
                    {
                        Property.SetValue(this, DataReader.GetFieldValue<DateOnly>(PropertyOrdinal));
                    }

                    // DataType: TimeOnly
                    else if (Property.PropertyType == typeof(TimeOnly))
                    {
                        Property.SetValue(this, DataReader.GetFieldValue<TimeOnly>(PropertyOrdinal));
                    }

                    // DataType: TimeSpan
                    else if (Property.PropertyType == typeof(TimeSpan))
                    {
                        Property.SetValue(this, DataReader.GetFieldValue<TimeSpan>(PropertyOrdinal));
                    }

                    // DataType: Boolean
                    else if (Property.PropertyType == typeof(bool))
                    {
                        Property.SetValue(this, DataReader.GetBoolean(PropertyOrdinal));
                    }

                    else
                    {
                        Property.SetValue(this, DataReader.GetValue(PropertyOrdinal));
                    }
                }
            }
        }

        protected void SetPropertyValues(PropertyInfo[] AllProperties, SqliteDataReader DataReader)
        {
            foreach (PropertyInfo Property in AllProperties)
            {
                int PropertyOrdinal = DataReader.GetOrdinal(Property.Name);

                if (!DataReader.IsDBNull(PropertyOrdinal))
                {
                    // DataType: Enums
                    if (Property.PropertyType.IsEnum)
                    {
                        Type EnumType = Property.PropertyType;
                        Property.SetValue(this, Enum.ToObject(EnumType, DataReader.GetInt32(PropertyOrdinal)));
                    }

                    // DataType: XML Document
                    else if (Property.PropertyType == typeof(XmlDocument))
                    {
                        SqlXml SQLXML = new SqlXml(new XmlTextReader(DataReader.GetString(DataReader.GetOrdinal(Property.Name)), XmlNodeType.Document, null));
                        XmlDocument XMLDocument = new XmlDocument();
                        XMLDocument.Load(SQLXML.CreateReader());
                        Property.SetValue(this, XMLDocument);
                    }

                    // DataType: XML
                    else if (Property.PropertyType == typeof(SqlXml))
                    {
                        Property.SetValue(this, new SqlXml(new XmlTextReader(DataReader.GetString(DataReader.GetOrdinal(Property.Name)), XmlNodeType.Document, null)));
                    }

                    // DataType: String
                    else if (Property.PropertyType == typeof(string))
                    {
                        Property.SetValue(this, DataReader.GetString(PropertyOrdinal));
                    }

                    // DataType: Character
                    else if (Property.PropertyType == typeof(char))
                    {
                        Property.SetValue(this, DataReader.GetChar(PropertyOrdinal));
                    }

                    // DataType: Long (Int64) Signed
                    else if (Property.PropertyType == typeof(long))
                    {
                        Property.SetValue(this, DataReader.GetInt64(PropertyOrdinal));
                    }

                    // DataType: Long (Int64) Unsigned
                    else if (Property.PropertyType == typeof(ulong))
                    {
                        Property.SetValue(this, DataReader.GetFieldValue<ulong>(PropertyOrdinal));
                    }

                    // DataType: Integer (Int32) Signed
                    else if (Property.PropertyType == typeof(int))
                    {
                        Property.SetValue(this, DataReader.GetInt32(PropertyOrdinal));
                    }

                    // DataType: Integer (Int32) Unsigned
                    else if (Property.PropertyType == typeof(uint))
                    {
                        Property.SetValue(this, DataReader.GetFieldValue<uint>(PropertyOrdinal));
                    }

                    // DataType: Short (Int16) Signed
                    else if (Property.PropertyType == typeof(short))
                    {
                        Property.SetValue(this, DataReader.GetInt16(PropertyOrdinal));
                    }

                    // DataType: Short (Int16) Unsigned
                    else if (Property.PropertyType == typeof(ushort))
                    {
                        Property.SetValue(this, DataReader.GetFieldValue<ushort>(PropertyOrdinal));
                    }

                    // DataType: Byte (Int8) Signed
                    else if (Property.PropertyType == typeof(byte))
                    {
                        Property.SetValue(this, DataReader.GetByte(PropertyOrdinal));
                    }

                    // DataType: Byte (Int8) Unsigned
                    else if (Property.PropertyType == typeof(sbyte))
                    {
                        Property.SetValue(this, DataReader.GetFieldValue<sbyte>(PropertyOrdinal));
                    }

                    // DataType: Float (32-bit)
                    else if (Property.PropertyType == typeof(float))
                    {
                        Property.SetValue(this, DataReader.GetFloat(PropertyOrdinal));
                    }

                    // DataType: Double (64-bit)
                    else if (Property.PropertyType == typeof(double))
                    {
                        Property.SetValue(this, DataReader.GetDouble(PropertyOrdinal));
                    }

                    // DataType: Decimal (128-bit)
                    else if (Property.PropertyType == typeof(decimal))
                    {
                        Property.SetValue(this, DataReader.GetDecimal(PropertyOrdinal));
                    }

                    // DataType: Guid
                    else if (Property.PropertyType == typeof(Guid))
                    {
                        Property.SetValue(this, DataReader.GetGuid(PropertyOrdinal));
                    }

                    // DataType: DateTime
                    else if (Property.PropertyType == typeof(DateTime))
                    {
                        Property.SetValue(this, DataReader.GetDateTime(PropertyOrdinal));
                    }

                    // DataType: DateOnly
                    else if (Property.PropertyType == typeof(DateOnly))
                    {
                        Property.SetValue(this, DataReader.GetFieldValue<DateOnly>(PropertyOrdinal));
                    }

                    // DataType: TimeOnly
                    else if (Property.PropertyType == typeof(TimeOnly))
                    {
                        Property.SetValue(this, DataReader.GetFieldValue<TimeOnly>(PropertyOrdinal));
                    }

                    // DataType: TimeSpan
                    else if (Property.PropertyType == typeof(TimeSpan))
                    {
                        Property.SetValue(this, DataReader.GetFieldValue<TimeSpan>(PropertyOrdinal));
                    }

                    // DataType: Boolean
                    else if (Property.PropertyType == typeof(bool))
                    {
                        Property.SetValue(this, DataReader.GetBoolean(PropertyOrdinal));
                    }

                    else
                    {
                        Property.SetValue(this, DataReader.GetValue(PropertyOrdinal));
                    }
                }
            }
        }

        #endregion

        #region "  System Methods  "

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string Property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Property));
            IsModified = true;
        }

        #endregion

        #region "  Other Methods  "

        public static Type GetClassType()
        {
            return MethodBase.GetCurrentMethod()?.DeclaringType;
        }

        public static PropertyInfo GetProperty(string Name)
        {
            Type ClassType = GetClassType();
            if (ClassType != null)
            {
                PropertyInfo[] AllProperties = ClassType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (PropertyInfo Property in AllProperties)
                {
                    if (Property.Name.ToUpper() == Name.Trim().ToUpper())
                    {
                        return Property;
                    }
                }
            }

            throw new ModularException(ExceptionType.NullObjectReturned, "Property returned is null");
        }

        public static PropertyInfo[] GetProperties()
        {
            Type ClassType = GetClassType();
            if (ClassType != null)
            {
                return ClassType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            }
            else
            {
                return Array.Empty<PropertyInfo>();
            }
        }

        #endregion

    }
}
