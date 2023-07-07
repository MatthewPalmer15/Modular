using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlTypes;
using System.Reflection;
using System.Xml;

namespace Modular.Core
{
    public class ModularReadOnlyBase : ModularBindableBase
    {

        #region "  Constructors  "

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

        #endregion

        #region "  Variables  "

        private Guid _ID;

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
                }
            }
        }

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Loads all instances of the object.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ModularException"></exception>
        protected static List<ModularReadOnlyBase> Load()
        {
            throw new ModularException(ExceptionType.BaseClassAccess, "Access denied to base class.");
        }

        /// <summary>
        /// Loads an instance of the object.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ModularException"></exception>
        protected static ModularReadOnlyBase Load(Guid ID)
        {
            throw new ModularException(ExceptionType.BaseClassAccess, "Access denied to base class.");
        }

        protected static ModularReadOnlyBase Load(string Name)
        {
            throw new ModularException(ExceptionType.BaseClassAccess, "Access denied to base class.");
        }

        #endregion

        #region "  Data Methods  "

        public void Fetch(Guid ID)
        {
            PropertyInfo? Property = GetType().GetProperty("ID");
            if (Property != null)
            {
                Fetch(Property, ID.ToString());
            }
        }

        public void Fetch(PropertyInfo Property, string Value)
        {
            Dictionary<PropertyInfo, string> Parameters = new Dictionary<PropertyInfo, string>
            {
                { Property, Value }
            };
            Fetch(Parameters);
        }

        public void Fetch(Dictionary<PropertyInfo, string> Parameters)
        {
            // Check if the database can be connected to.
            if (Database.CheckDatabaseConnection())
            {
                Database.DatabaseConnectivityMode DatabaseConnectionMode = Database.ConnectionMode;
                PropertyInfo[] AllProperties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

                switch (DatabaseConnectionMode)
                {

                    case Database.DatabaseConnectivityMode.Remote:
                        using (SqlConnection Connection = new SqlConnection(Database.ConnectionString))
                        {
                            Connection.Open();


                            // If Database Table does not exist, and Dynamic Database is active, create the table.
                            if (!Database.CheckDatabaseTableExists(MODULAR_DATABASE_TABLE))
                            {
                                DatabaseQueryUtils.CreateNewTableQuery(MODULAR_DATABASE_TABLE, AllProperties);
                            }


                            using (SqlCommand Command = new SqlCommand())
                            {
                                Command.CommandType = CommandType.Text;
                                Command.CommandText = DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE, ID);

                                SqlDataReader DataReader = Command.ExecuteReader();
                                DataReader.Read();
                                SetPropertyValues(AllProperties, DataReader);
                            }

                            Connection.Close();
                        }
                        break;

                    case Database.DatabaseConnectivityMode.Local:
                        using (SqliteConnection Connection = new SqliteConnection(Database.ConnectionString))
                        {
                            Connection.Open();

                            if (!Database.CheckDatabaseTableExists(MODULAR_DATABASE_TABLE))
                            {
                                DatabaseUtils.CreateDatabaseTable(MODULAR_DATABASE_TABLE, AllProperties);
                            }

                            using (SqliteCommand Command = new SqliteCommand())
                            {
                                Command.CommandType = CommandType.Text;
                                Command.CommandText = DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE, ID);

                                SqliteDataReader DataReader = Command.ExecuteReader();
                                DataReader.Read();
                                SetPropertyValues(AllProperties, DataReader);

                            }

                            Connection.Close();
                        }
                        break;
                }
            }
            else
            {
                throw new ModularException(ExceptionType.DatabaseConnectionError, $"There was an issue trying to connect to the database.");
            }

        }

        #endregion

        #region "  Object Methods  "

        protected void SetPropertyValues(PropertyInfo[] AllProperties, SqlDataReader DataReader)
        {
            foreach (PropertyInfo Property in AllProperties)
            {
                // DataType: Enums
                if (Property.PropertyType.IsEnum)
                {
                    Type EnumType = Property.PropertyType;
                    Property.SetValue(this, Enum.ToObject(EnumType, DataReader.GetInt32(DataReader.GetOrdinal(Property.Name))));
                }

                // DataType: XML Document
                else if (Property.PropertyType == typeof(XmlDocument))
                {
                    SqlXml SQLXML = DataReader.GetSqlXml(DataReader.GetOrdinal(Property.Name));
                    XmlDocument XMLDocument = new XmlDocument();
                    XMLDocument.Load(SQLXML.CreateReader());
                    Property.SetValue(this, XMLDocument);
                }

                // DataType: XML
                else if (Property.PropertyType == typeof(SqlXml))
                {
                    Property.SetValue(this, DataReader.GetSqlXml(DataReader.GetOrdinal(Property.Name)));
                }

                // DataType: String
                else if (Property.PropertyType == typeof(string))
                {
                    Property.SetValue(this, DataReader.GetString(DataReader.GetOrdinal(Property.Name)));
                }

                // DataType: Integer
                else if (Property.PropertyType == typeof(int))
                {
                    Property.SetValue(this, DataReader.GetInt32(DataReader.GetOrdinal(Property.Name)));
                }

                // DataType: Float
                else if (Property.PropertyType == typeof(float))
                {
                    Property.SetValue(this, DataReader.GetFloat(DataReader.GetOrdinal(Property.Name)));
                }

                // DataType: Double
                else if (Property.PropertyType == typeof(double))
                {
                    Property.SetValue(this, DataReader.GetDouble(DataReader.GetOrdinal(Property.Name)));
                }

                // DataType: Decimal
                else if (Property.PropertyType == typeof(decimal))
                {
                    Property.SetValue(this, DataReader.GetDecimal(DataReader.GetOrdinal(Property.Name)));
                }

                // DataType: Guid
                else if (Property.PropertyType == typeof(Guid))
                {
                    Property.SetValue(this, DataReader.GetGuid(DataReader.GetOrdinal(Property.Name)));
                }

                // DataType: DateTime
                else if (Property.PropertyType == typeof(DateTime))
                {
                    Property.SetValue(this, DataReader.GetDateTime(DataReader.GetOrdinal(Property.Name)));
                }

                // DataType: Boolean
                else if (Property.PropertyType == typeof(bool))
                {
                    Property.SetValue(this, DataReader.GetBoolean(DataReader.GetOrdinal(Property.Name)));
                }

                // DataType: Byte
                else if (Property.PropertyType == typeof(byte))
                {
                    Property.SetValue(this, DataReader.GetByte(DataReader.GetOrdinal(Property.Name)));
                }

                // DataType: Byte[]
                else if (Property.PropertyType == typeof(byte[]))
                {
                    Property.SetValue(this, DataReader.GetValue(DataReader.GetOrdinal(Property.Name)));
                }

            }
        }

        protected void SetPropertyValues(PropertyInfo[] AllProperties, SqliteDataReader DataReader)
        {
            foreach (PropertyInfo Property in AllProperties)
            {
                // DataType: Enums
                if (Property.PropertyType.IsEnum)
                {
                    Type EnumType = Property.PropertyType;
                    Property.SetValue(this, Enum.ToObject(EnumType, DataReader.GetInt32(DataReader.GetOrdinal(Property.Name))));
                }

                // DataType: XML Document
                // else if (Property.PropertyType == typeof(XmlDocument))
                // {
                //     SqlXml SQLXML = DataReader.GetString(DataReader.GetOrdinal(Property.Name));
                //     XmlDocument XMLDocument = new XmlDocument();
                //     XMLDocument.Load(SQLXML.CreateReader());
                //     Property.SetValue(this, XMLDocument);
                // }
                //TODO: XML DOCUMENT NOT SUPPORTED WITH SQLITE

                // DataType: XML
                else if (Property.PropertyType == typeof(SqlXml))
                {
                    Property.SetValue(this, DataReader.GetString(DataReader.GetOrdinal(Property.Name)));
                }

                // DataType: String
                else if (Property.PropertyType == typeof(string))
                {
                    Property.SetValue(this, DataReader.GetString(DataReader.GetOrdinal(Property.Name)));
                }

                // DataType: Integer
                else if (Property.PropertyType == typeof(int))
                {
                    Property.SetValue(this, DataReader.GetInt32(DataReader.GetOrdinal(Property.Name)));
                }

                // DataType: Float
                else if (Property.PropertyType == typeof(float))
                {
                    Property.SetValue(this, DataReader.GetFloat(DataReader.GetOrdinal(Property.Name)));
                }

                // DataType: Double
                else if (Property.PropertyType == typeof(double))
                {
                    Property.SetValue(this, DataReader.GetDouble(DataReader.GetOrdinal(Property.Name)));
                }

                // DataType: Decimal
                else if (Property.PropertyType == typeof(decimal))
                {
                    Property.SetValue(this, DataReader.GetDecimal(DataReader.GetOrdinal(Property.Name)));
                }

                // DataType: Guid
                else if (Property.PropertyType == typeof(Guid))
                {
                    Property.SetValue(this, DataReader.GetGuid(DataReader.GetOrdinal(Property.Name)));
                }

                // DataType: DateTime
                else if (Property.PropertyType == typeof(DateTime))
                {
                    Property.SetValue(this, DataReader.GetDateTime(DataReader.GetOrdinal(Property.Name)));
                }

                // DataType: Boolean
                else if (Property.PropertyType == typeof(bool))
                {
                    Property.SetValue(this, DataReader.GetBoolean(DataReader.GetOrdinal(Property.Name)));
                }

                // DataType: Byte
                else if (Property.PropertyType == typeof(byte))
                {
                    Property.SetValue(this, DataReader.GetByte(DataReader.GetOrdinal(Property.Name)));
                }

                // DataType: Byte[]
                else if (Property.PropertyType == typeof(byte[]))
                {
                    Property.SetValue(this, DataReader.GetValue(DataReader.GetOrdinal(Property.Name)));
                }

            }
        }

        #endregion

        #region "  Other Methods  "

        public static Type? GetClassType()
        {
            MethodBase? MethodBase = MethodBase.GetCurrentMethod();
            if (MethodBase != null && MethodBase.DeclaringType != null)
            {
                return MethodBase.DeclaringType;
            }
            else
            {
                return null;
            }
        }

        public static PropertyInfo? GetProperty(string Name)
        {
            Type? ClassType = GetClassType();
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
            return null;
        }

        public static PropertyInfo[]? GetProperties()
        {
            Type? ClassType = GetClassType();
            return ClassType?.GetProperties(BindingFlags.Instance | BindingFlags.Public);
        }

        #endregion
    }
}
