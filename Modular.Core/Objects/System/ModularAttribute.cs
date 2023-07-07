namespace Modular.Core
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class UniqueAttribute : Attribute
    {
        public string ErrorMessage { get; set; } = string.Empty;
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IgnoreAttribute : Attribute
    {
        public string ErrorMessage { get; set; } = string.Empty;
    }
}


