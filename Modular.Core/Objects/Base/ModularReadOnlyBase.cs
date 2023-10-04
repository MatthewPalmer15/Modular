using Microsoft.SqlServer.Types;
using Modular.Core.Audit;
using Modular.Core.Databases;
using Modular.Core.Attributes;
using Modular.Core.Utility;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlTypes;
using System.Reflection;
using System.Text;
using System.Xml;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;

namespace Modular.Core
{
    public class ModularReadOnlyBase
    {

        #region "  Constructors  "

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <exception cref="ModularException">You cannot create a new instance of the Base Class.</exception>
        public ModularReadOnlyBase()
        {
            if (this.GetType() == typeof(ModularReadOnlyBase))
            {
                throw new ModularException(ExceptionType.BaseClassAccess, "Access denied to base class.");
            }
        }

        #endregion

        #region "  Constants  "

        protected static readonly string MODULAR_DATABASE_TABLE = "";
        protected static readonly string MODULAR_DATABASE_STOREDPROCEDURE_PREFIX = "";
        protected static readonly Type MODULAR_OBJECTTYPE = typeof(ModularReadOnlyBase);

        #endregion

        #region "  Properties  "

        [Display(Name = "ID")]
        public Guid ID { get; private set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; private set; }

        [Display(Name = "Created By")]
        public Guid CreatedBy { get; private set; }

        [Display(Name = "Updated Date")]
        public DateTime ModifiedDate { get; private set; }

        [Display(Name = "Modified By")]
        public Guid ModifiedBy { get; private set; }

        public bool IsDeleted { get; private set; }

        public bool IsFlagged { get; private set; }

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Loads all instances of the object.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ModularException"></exception>
        protected static List<ModularBase> LoadList()
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

        #endregion

        #region "  Instance Methods  "

        /// <summary>
        /// Reloads the current instance from the database.
        /// </summary>
        public void Reload()
        {
            Fetch(ID);
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
                PropertyInfo[] AllProperties = GetType().GetProperties(BindingFlags.Instance).Where(Property => !Property.IsDefined(typeof(IgnoreAttribute), false)).ToArray();

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
                                DatabaseUtils.CreateStoredProcedure(DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE, AllProperties.SingleOrDefault(x => x.Name.Equals("ID"))), StoredProcedureName);
                            }

                            using (SqlCommand Command = new SqlCommand())
                            {
                                Command.Connection = Connection;

                                // If stored procedures are enabled, use the stored procedure, otherwise use a text query.
                                Command.CommandType = Database.EnableStoredProcedures ? CommandType.StoredProcedure : CommandType.Text;
                                Command.CommandText = Database.EnableStoredProcedures ? StoredProcedureName : DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE, AllProperties.SingleOrDefault(x => x.Name.Equals("ID")));

                                Command.Parameters.Add(new SqlParameter($"@ID", ID));

                                using (SqlDataReader DataReader = Command.ExecuteReader())
                                {
                                    // If data was returned, set the field values.
                                    if (DataReader.HasRows)
                                    {
                                        DataReader.Read();
                                        SetPropertyValues(AllProperties, DataReader);
                                    }
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
                                Command.CommandText = DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE, AllProperties.SingleOrDefault(x => x.Name.Equals("ID")));

                                Command.Parameters.Add(new SqliteParameter($"@ID", ID));

                                using (SqliteDataReader DataReader = Command.ExecuteReader())
                                {
                                    // If data was returned, set the field values.
                                    if (DataReader.HasRows)
                                    {
                                        DataReader.Read();
                                        SetPropertyValues(AllProperties, DataReader);
                                    }
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
        public void Fetch(PropertyInfo Property, string Value)
        {
            // Check if the database can be connected to.
            if (Database.CheckDatabaseConnection())
            {
                PropertyInfo[] AllProperties = GetType().GetProperties(BindingFlags.Instance).Where(Property => !Property.IsDefined(typeof(IgnoreAttribute), false)).ToArray();
                string PropertyName = Property.Name.Trim().Replace("_", "");

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

                            string StoredProcedureName = $"{MODULAR_DATABASE_STOREDPROCEDURE_PREFIX}_FetchBy{PropertyName}";

                            // If stored procedures are enabled, and the stored procedure does not exist, create it.
                            if (Database.EnableStoredProcedures && !Database.CheckStoredProcedureExists(StoredProcedureName))
                            {
                                DatabaseUtils.CreateStoredProcedure(DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE, Property), StoredProcedureName);
                            }

                            using (SqlCommand Command = new SqlCommand())
                            {
                                Command.Connection = Connection;

                                // If stored procedures are enabled, use the stored procedure, otherwise use a query.
                                Command.CommandType = Database.EnableStoredProcedures ? CommandType.StoredProcedure : CommandType.Text;
                                Command.CommandText = Database.EnableStoredProcedures ? StoredProcedureName : DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE, Property);

                                Command.Parameters.Add(new SqlParameter($"@{PropertyName}", Value));

                                using (SqlDataReader DataReader = Command.ExecuteReader())
                                {
                                    // If data was returned, set the field values.
                                    if (DataReader.HasRows)
                                    {
                                        DataReader.Read();
                                        SetPropertyValues(AllProperties, DataReader);
                                    }
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

                                Command.Parameters.Add(new SqliteParameter($"@ID", ID));

                                using (SqliteDataReader DataReader = Command.ExecuteReader())
                                {
                                    // If data was returned, set the field values.
                                    if (DataReader.HasRows)
                                    {
                                        DataReader.Read();
                                        SetPropertyValues(AllProperties, DataReader);
                                    }
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

        #endregion

        #region "  System Methods  "

        /// <summary>
        /// Sets all properties within the class to the default values
        /// </summary>
        /// <param name="OverrideCurrentValues">
        /// Overrides any current data within the object.
        /// </param>
        public void SetDefaultValues(bool OverrideCurrentValues = false)
        {
            // Get all Properties within the class that are instance-based and public
            PropertyInfo[] AllProperties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo Property in AllProperties)
            {
                // If the current field is null, or properties are allowed to be override, we set a default value
                if ((Equals(Property.GetValue(this), null) || OverrideCurrentValues) && Property.CanWrite)
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

                    // DataType: Geography
                    else if (Property.PropertyType == typeof(SqlGeography))
                    {
                        Property.SetValue(this, SqlGeography.Point(0, 0, 0));
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
                        Property.SetValue(this, Guid.Empty);
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

                    // DataType: Geography
                    else if (Property.PropertyType == typeof(SqlGeography))
                    {
                        Property.SetValue(this, (SqlGeography)DataReader.GetValue(PropertyOrdinal));
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

                    // DataType: Geography
                    else if (Property.PropertyType == typeof(SqlGeography))
                    {
                        Property.SetValue(this, (SqlGeography)DataReader.GetValue(PropertyOrdinal));
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

        #region "  Other Methods  "

        public static class Class
        {

            public readonly static string Name = MODULAR_OBJECTTYPE.Name;

            public readonly static string Namespace = MODULAR_OBJECTTYPE.Namespace;

            public readonly static string FullName = MODULAR_OBJECTTYPE.FullName;

            public static FieldInfo? GetField(string Name)
            {
                string FieldName = Name.Trim().StartsWith('_') ? Name.Trim() : $"_{Name.Trim()}";
                FieldInfo Field = MODULAR_OBJECTTYPE.GetField(FieldName, BindingFlags.Instance);
                return Field ?? null;
            }

            public static PropertyInfo? GetProperty(string Name)
            {
                PropertyInfo Property = MODULAR_OBJECTTYPE.GetProperty(Name, BindingFlags.Instance);
                return Property ?? null;
            }

            public static FieldInfo[] GetFields()
            {
                return MODULAR_OBJECTTYPE
                    .GetFields(BindingFlags.Instance)
                    .Where(Field => !Field.IsDefined(typeof(IgnoreAttribute), false))
                    .ToArray();
            }

            public static PropertyInfo[] GetProperties()
            {
                return MODULAR_OBJECTTYPE
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .Where(Property => !Property.IsDefined(typeof(IgnoreAttribute), false))
                    .ToArray();
            }

        }
        #endregion


    }
}
