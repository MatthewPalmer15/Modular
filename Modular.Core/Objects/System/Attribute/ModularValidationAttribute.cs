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

        public override bool IsValid(object? value)
        {
            // TODO: Get the database table name from the property, and then check the database to see if the value is unique
            return true;
        }

    }

}


