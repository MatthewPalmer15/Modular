using System.ComponentModel.DataAnnotations;

namespace Modular.Core
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

        public override bool IsValid(object value)
        {
            // TODO: Get the database table name from the property, and then check the database to see if the value is unique
            return true;
        }

    }

    //  TODO: Refractor this attribute. This attribute should be used to check if a property is not the default value for that type.
    //  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    //  public class NotDefaultAttribute : ValidationAttribute
    //  {
    //  
    //      public NotDefaultAttribute()
    //      {
    //      }
    //  
    //      public override bool IsValid(object value)
    //      {
    //          
    //      }
    //  }

    //  TODO: Refractor this attribute
    //  [AttributeUsage(AttributeTargets.Property)]
    //  public class CustomRequiredIfAttribute : ValidationAttribute
    //  {
    //      private readonly string dependentProperty;
    //      private readonly object targetValue;
    //  
    //      public CustomRequiredIfAttribute(string dependentProperty, object targetValue)
    //      {
    //          this.dependentProperty = dependentProperty;
    //          this.targetValue = targetValue;
    //      }
    //  
    //      protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //      {
    //          var dependentPropertyValue = validationContext.ObjectType.GetProperty(dependentProperty)?.GetValue(validationContext.ObjectInstance);
    //  
    //          if (dependentPropertyValue != null && dependentPropertyValue.Equals(targetValue))
    //          {
    //              if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
    //              {
    //                  return new ValidationResult(ErrorMessage);
    //              }
    //          }
    //  
    //          return ValidationResult.Success;
    //      }
    //  }
    //
    //  IMPLEMENTATION
    //  public bool IsTermsAndConditionsAccepted { get; set; }
    //
    //  [CustomRequiredIf("IsTermsAndConditionsAccepted", true, ErrorMessage = "Please provide your username.")]
    //  public string Username { get; set; }


}


