using System.ComponentModel.DataAnnotations;

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

        public override bool IsValid(object value)
        {
            // TODO: Get the database table name from the property, and then check the database to see if the value is unique
            return true;
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


