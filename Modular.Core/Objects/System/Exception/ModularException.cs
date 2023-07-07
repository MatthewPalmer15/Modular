namespace Modular.Core
{
    public class ModularException : Exception
    {

        public ModularException()
        {
        }

        private ModularException(string message) : base(message)
        {
        }

        public ModularException(ExceptionType Type, string Message)
        {
            ExceptionLog objExceptionLog = ExceptionLog.Create();
            objExceptionLog.Message = Message;
            objExceptionLog.Type = Type;
            objExceptionLog.StackTrace = !string.IsNullOrEmpty(StackTrace) ? StackTrace : "Unknown";
            objExceptionLog.Source = !string.IsNullOrEmpty(Source) ? Source : "Unknown";
            objExceptionLog.Target = TargetSite != null && !string.IsNullOrEmpty(TargetSite.Name) ? TargetSite.Name : "Unknown";
            objExceptionLog.Save();

            throw new ModularException($"{Type}: {Message}");
        }
    }
}
