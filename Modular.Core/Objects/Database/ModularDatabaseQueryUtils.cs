using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using Modular.Core.System.Attributes;

namespace Modular.Core.Databases
{
    /// <summary>
    /// Dynamic Database Functionality
    /// </summary>
    public static class DatabaseQueryUtils
    {

        #region "  Methods (Table)  "

        /// <summary>
        /// Creates a query to create a new table in the database.
        /// </summary>
        /// <param name="DatabaseTableName"></param>
        /// <param name="AllProperties"></param>
        /// <returns></returns>
        public static string CreateNewTableQuery(string DatabaseTableName, PropertyInfo[] AllProperties)
        {
            bool IsFirstIteration = true;

            StringBuilder StrBuilder = new StringBuilder();
            StrBuilder.Clear(); // Its a new instance, but to make sure its blank.

            StrBuilder.AppendLine($"CREATE TABLE {DatabaseTableName} (");

            foreach (PropertyInfo Property in AllProperties)
            {
                bool HasIgnoreAttribute = Property.GetCustomAttributes(typeof(IgnoreAttribute), false).Length > 0;

                if (Property.CanRead && Property.CanWrite && !HasIgnoreAttribute)
                {
                    if (IsFirstIteration)
                    {
                        StrBuilder.AppendLine($"{Property.Name} {GetDatabaseAttributes(Property)}");
                        IsFirstIteration = false;
                    }
                    else
                    {
                        StrBuilder.AppendLine($", {Property.Name} {GetDatabaseAttributes(Property)}");
                    }
                }
            }

            StrBuilder.AppendLine(");");
            return StrBuilder.ToString();
        }

        /// <summary>
        /// Creates a query to alter an existing table in the database.
        /// </summary>
        /// <param name="DatabaseTableName"></param>
        /// <param name="AllProperties"></param>
        /// <returns></returns>
        public static string CreateAlterTableQuery(string DatabaseTableName, PropertyInfo[] AllProperties)
        {
            bool IsFirstIteration = true;

            StringBuilder StrBuilder = new StringBuilder();
            StrBuilder.Clear(); // Its a new instance, but to make sure its blank.

            StrBuilder.AppendLine($"ALTER TABLE {DatabaseTableName} (");

            foreach (PropertyInfo Property in AllProperties)
            {
                bool HasIgnoreAttribute = Property.GetCustomAttributes(typeof(IgnoreAttribute), false).Length > 0;

                if (Property.CanRead && Property.CanWrite && !HasIgnoreAttribute)
                {
                    if (IsFirstIteration)
                    {
                        StrBuilder.AppendLine($"{Property.Name} {GetDatabaseAttributes(Property)}");
                        IsFirstIteration = false;
                    }
                    else
                    {
                        StrBuilder.AppendLine($", {Property.Name} {GetDatabaseAttributes(Property)}");
                    }
                }
            }

            StrBuilder.AppendLine(");");
            return StrBuilder.ToString();
        }

        #endregion

        #region "  Methods (Triggers)  "

        #endregion

        #region "  Methods (Fetch)  "

        public static string CreateFetchQuery(string DatabaseTableName)
        {
            StringBuilder StrBuilder = new StringBuilder();
            StrBuilder.Clear(); // Its a new instance, but to make sure its blank.

            StrBuilder.AppendLine($"SELECT * FROM {DatabaseTableName}");

            return StrBuilder.ToString();
        }

        public static string CreateFetchQuery(string DatabaseTableName, Guid ID)
        {
            StringBuilder StrBuilder = new StringBuilder();
            StrBuilder.Clear(); // Its a new instance, but to make sure its blank.

            StrBuilder.AppendLine($"SELECT * FROM {DatabaseTableName}");
            StrBuilder.AppendLine($"WHERE ID = '{ID}'");
            return StrBuilder.ToString();
        }

        public static string CreateFetchQuery(string DatabaseTableName, PropertyInfo Property)
        {
            PropertyInfo[] AllProperties = { Property };
            return CreateFetchQuery(DatabaseTableName, AllProperties);
        }

        public static string CreateFetchQuery(string DatabaseTableName, PropertyInfo[] AllProperties)
        {
            bool IsFirstIteration = true;

            StringBuilder StrBuilder = new StringBuilder();
            StrBuilder.Clear(); // Its a new instance, but to make sure its blank.

            StrBuilder.AppendLine($"SELECT * FROM {DatabaseTableName}");
            StrBuilder.AppendLine("WHERE");

            foreach (string PropertyName in AllProperties.Select(Property => Property.Name))
            {
                if (IsFirstIteration)
                {
                    StrBuilder.AppendLine($"{PropertyName} = @{PropertyName}");
                    IsFirstIteration = false;
                }
                else
                {
                    StrBuilder.AppendLine($"AND {PropertyName} = @{PropertyName}");
                }

            }

            return StrBuilder.ToString();
        }

        #endregion

        #region "  Methods (Insert)  "

        public static string CreateInsertQuery(string DatabaseTableName, PropertyInfo[] AllProperties)
        {
            bool IsFirstIteration = true;

            StringBuilder StrBuilder = new StringBuilder();
            StrBuilder.Clear(); // Its a new instance, but to make sure its blank.

            StrBuilder.AppendLine($"INSERT INTO {DatabaseTableName} (");
            foreach (string PropertyName in AllProperties.Select(Property => Property.Name))
            {
                if (IsFirstIteration)
                {
                    StrBuilder.AppendLine($"{PropertyName}");
                    IsFirstIteration = false;
                }
                else
                {
                    StrBuilder.AppendLine($", {PropertyName}");
                }
            }

            StrBuilder.AppendLine(") VALUES (");
            IsFirstIteration = true;

            foreach (string PropertyName in AllProperties.Select(Property => Property.Name))
            {
                if (IsFirstIteration)
                {
                    StrBuilder.AppendLine($"@{PropertyName}");
                    IsFirstIteration = false;
                }
                else
                {
                    StrBuilder.AppendLine($", @{PropertyName}");
                }
            }

            StrBuilder.AppendLine(")");

            return StrBuilder.ToString();
        }

        #endregion

        #region "  Methods (Update)  "

        public static string CreateUpdateQuery(string DatabaseTableName, PropertyInfo[] AllProperties)
        {
            bool IsFirstIteration = true;

            StringBuilder StrBuilder = new StringBuilder();
            StrBuilder.Clear(); // Its a new instance, but to make sure its blank.

            StrBuilder.AppendLine($"UPDATE {DatabaseTableName} SET");

            foreach (string PropertyName in AllProperties.Select(Property => Property.Name))
            {
                if (IsFirstIteration)
                {
                    StrBuilder.AppendLine($"{PropertyName} = @{PropertyName}");
                    IsFirstIteration = false;
                }
                else
                {
                    StrBuilder.AppendLine($", {PropertyName} = @{PropertyName}");
                }
            }

            StrBuilder.AppendLine("WHERE ID = @ID");

            return StrBuilder.ToString();
        }

        #endregion

        #region "  Methods (Delete)  "

        public static string CreateDeleteQuery(string DatabaseTableName, PropertyInfo Property)
        {
            StringBuilder StrBuilder = new StringBuilder();
            StrBuilder.Clear(); // Its a new instance, but to make sure its blank.s
            StrBuilder.Append($"DELETE FROM {DatabaseTableName} WHERE {Property.Name} = @{Property.Name}");


            return StrBuilder.ToString();
        }

        #endregion

        #region "  Methods (Attributes)  "

        /// <summary>
        /// Gets the attributes for creating/altering a table.
        /// </summary>
        /// <param name="Property"></param>
        /// <returns></returns>
        /// <exception cref="InvalidCastException"></exception>
        private static string GetDatabaseAttributes(PropertyInfo Property)
        {
            // DataType: StringW
            if (Property.PropertyType == typeof(string))
            {
                MaxLengthAttribute MaxLengthAttribute = Property.GetCustomAttribute<MaxLengthAttribute>();
                return MaxLengthAttribute != null ? $"NVARCHAR({MaxLengthAttribute.Length})" : "NVARCHAR(MAX)";
            }

            // DataType: Character
            else if (Property.PropertyType == typeof(char))
            {
                return "CHAR";
            }

            // DataType: Long (Int64) Signed or Unsigned
            else if (Property.PropertyType == typeof(long) || Property.PropertyType == typeof(ulong))
            {
                return "BIGINT";
            }

            // DataType: Integer (Int32) Signed or Unsigned or Enum
            else if (Property.PropertyType == typeof(int) || Property.PropertyType == typeof(uint) || Property.PropertyType.IsEnum)
            {
                return "INT";
            }

            // DataType: Short (Int16) Signed or Unsigned
            else if (Property.PropertyType == typeof(short) || Property.PropertyType == typeof(ushort))
            {
                return "SMALLINT";
            }

            // DataType: Byte (Int8) 
            else if (Property.PropertyType == typeof(byte) || Property.PropertyType == typeof(sbyte))
            {
                return "TINYINT";
            }

            // DataType: Float (32-bit)
            else if (Property.PropertyType == typeof(float))
            {
                return "FLOAT";
            }

            // DataType: Double (64-bit)
            else if (Property.PropertyType == typeof(double))
            {
                return "DOUBLE";
            }

            // DataType: Decimal (128-bit)
            else if (Property.PropertyType == typeof(decimal))
            {
                // Decimal works like this: DECIMAL(precision, scale)
                return "DECIMAL";
            }

            // DataType: Guid
            else if (Property.PropertyType == typeof(Guid))
            {
                bool HasKeyAttributes = Property.GetCustomAttributes(typeof(KeyAttribute), false).Length > 0;
                return HasKeyAttributes ? "UNIQUEIDENTIFER PRIMARY KEY" : "UNIQUEIDENTIFER";
            }

            // DataType: DateTime
            else if (Property.PropertyType == typeof(DateTime) || Property.PropertyType == typeof(DateOnly) || Property.PropertyType == typeof(TimeOnly) || Property.PropertyType == typeof(TimeSpan))
            {
                return "DATETIME";
            }

            // DataType: Boolean
            else if (Property.PropertyType == typeof(bool))
            {
                return "BIT";
            }

            // DataType: Binary
            else if (Property.PropertyType == typeof(byte[]))
            {
                return "VARBINARY(MAX)";
            }

            else if (Property.PropertyType == typeof(object))
            {
                return "UNIQUEIDENTIFER";
            }

            // DataType: Unknown
            else
            {
                throw new ModularException(ExceptionType.DataTypeNotSupported, $"DataType {Property.PropertyType} is not supported.");
            }
        }

        #endregion

    }

}

