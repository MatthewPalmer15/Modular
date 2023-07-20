using System.Runtime.Serialization;
using Modular.Core.Utility;

namespace Modular.Core
{

    [Serializable]
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
            objExceptionLog.DeviceInformation = ModularUtils.GetDeviceSummary();
            objExceptionLog.Save();

            throw new ModularException($"{Type}: {Message}");
        }

        protected ModularException(SerializationInfo info, StreamingContext context): base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
