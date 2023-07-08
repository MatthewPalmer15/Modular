namespace Modular.Core
{

    /// <summary>
    /// Attribute which will not allow a property to be the same as another property in the database.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class UniqueAttribute : Attribute
    {
        public string ErrorMessage { get; set; } = string.Empty;
    }

    /// <summary>
    /// Attribute that will ignore a property when saving to the database.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IgnoreAttribute : Attribute
    {
        public string ErrorMessage { get; set; } = string.Empty;
    }

    /// <summary>
    /// Attribute which indicates that a property is system managed and should not be changed by the user.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SystemManagedAttribute : Attribute
    {
        public string ErrorMessage { get; set; } = string.Empty;
    }
}


