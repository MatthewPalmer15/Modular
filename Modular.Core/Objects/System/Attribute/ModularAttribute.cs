namespace Modular.Core.Attributes
{

    /// <summary>
    /// Attribute that will ignore a property when saving to the database.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class IgnoreAttribute : Attribute
    {
    }

    /// <summary>
    /// Attribute which indicates that a property is system managed and should not be changed by the user.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SystemManagedAttribute : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IdentifierAttribute : Attribute
    {
        // This is a marker attribute that indicates that a property is significant and should be included in the search index.
    }

}


