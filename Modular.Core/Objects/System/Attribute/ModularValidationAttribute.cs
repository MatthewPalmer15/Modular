using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;
using Modular.Core.Databases;

namespace Modular.Core.System.Attributes
{

    /// <summary>
    /// Attribute which will not allow a property to be the same as another property in the database.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class UniqueAttribute : ValidationAttribute
    {

        public UniqueAttribute()
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                int Count = 0;

                if (Database.CheckDatabaseConnection())
                {
                    switch (Database.ConnectionMode)
                    {
                        case Database.DatabaseConnectivityMode.Remote:
                            using (SqlConnection Connection = new SqlConnection(Database.ConnectionString))
                            {
                                Connection.Open();
                                using (SqlCommand Command = new SqlCommand())
                                {
                                    Command.Connection = Connection;
                                    Command.CommandType = CommandType.Text;
                                    Command.CommandText = $"SELECT COUNT(*) FROM tbl_{SystemApplication.Name}_{validationContext.ObjectType} WHERE {validationContext.MemberName} = @Value";
                                    Command.Parameters.AddWithValue("@Value", validationContext.ObjectType.GetProperty(validationContext.MemberName).GetValue(validationContext.ObjectInstance));

                                    Count = (int)Command.ExecuteScalar();
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
                                    Command.CommandType = CommandType.Text;
                                    Command.CommandText = $"SELECT COUNT(*) FROM tbl_{SystemApplication.Name}_{validationContext.ObjectType} WHERE {validationContext.MemberName} = @Value";
                                    Command.Parameters.AddWithValue("@Value", validationContext.ObjectType.GetProperty(validationContext.MemberName).GetValue(validationContext.ObjectInstance));

                                    Count = (int)Command.ExecuteScalar();
                                }

                                Connection.Close();
                            }
                            break;
                    }

                    if (Count > 0)
                    {
                        return new ValidationResult("This value already exists in the database.");
                    }
                    else
                    {
                        return ValidationResult.Success;
                    }

                }
                else
                {
                    return new ValidationResult("Database connection is not available.");
                }

            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }


    [AttributeUsage(AttributeTargets.Property)]
    public class CustomRequiredIfAttribute : ValidationAttribute
    {
        private readonly string DependentProperty;
        private readonly object Value;
      
        public CustomRequiredIfAttribute(string DependentProperty, object Value)
        {
            this.DependentProperty = DependentProperty;
            this.Value = Value;
        }
      
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var DependentPropertyValue = validationContext.ObjectType.GetProperty(DependentProperty)?.GetValue(validationContext.ObjectInstance);
      
            if (DependentPropertyValue != null && DependentPropertyValue.Equals(Value))
            {
                if (value == null || string.IsNullOrEmpty(value.ToString()))
                {
                    return new ValidationResult(ErrorMessage);
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
      
            return ValidationResult.Success;
        }
    }
    
    //  IMPLEMENTATION
    //  public bool IsTermsAndConditionsAccepted { get; set; }
    //
    //  [CustomRequiredIf("IsTermsAndConditionsAccepted", true, ErrorMessage = "Please provide your username.")]
    //  public string Username { get; set; }


}


