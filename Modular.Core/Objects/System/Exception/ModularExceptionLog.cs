namespace Modular.Core
{
    public class ExceptionLog : ModularBase
    {

        #region "  Constructors  "

        public ExceptionLog()
        {
        }

        #endregion

        #region "  Constants  "

        protected static readonly new string MODULAR_DATABASE_TABLE = "tbl_Modular_ExceptionLog";

        #endregion

        #region "  Variables  "

        private ExceptionType _ExceptionType;

        private string _Message = string.Empty;

        private string _StackTrace = string.Empty;

        private string _Source = string.Empty;

        private string _Target = string.Empty;

        #endregion

        #region "  Properties  "

        public ExceptionType Type
        {
            get
            {
                return _ExceptionType;
            }
            set
            {
                if (_ExceptionType != value)
                {
                    _ExceptionType = value;
                    OnPropertyChanged("ExceptionType");
                }
            }
        }

        public string Message
        {
            get
            {
                return _Message;
            }
            set
            {
                if (_Message != value)
                {
                    _Message = value;
                }
            }
        }

        public string StackTrace
        {
            get
            {
                return _StackTrace;
            }
            set
            {
                if (_StackTrace != value)
                {
                    _StackTrace = value;

                }
            }
        }

        public string Source
        {
            get
            {
                return _Source;
            }
            set
            {
                if (_Source != value)
                {
                    _Source = value;
                    OnPropertyChanged("Source");
                }

            }
        }

        public string Target
        {
            get
            {
                return _Target;
            }
            set
            {
                if (_Target != value)
                {
                    _Target = value;
                }
            }
        }

        #endregion

        #region "  Static Methods  "

        public static new ExceptionLog Create()
        {
            ExceptionLog objExceptionLog = new ExceptionLog();
            objExceptionLog.SetDefaultValues();
            return objExceptionLog;
        }

        public static new List<ExceptionLog> LoadAll()
        {
            return FetchAll();
        }

        public static new ExceptionLog Load(Guid ID)
        {
            ExceptionLog objExceptionLog = new ExceptionLog();
            objExceptionLog.Fetch(ID);
            return objExceptionLog;
        }

        #endregion

        public static List<ExceptionLog> FetchAll()
        {
            List<ExceptionLog> objExceptionLogs = new List<ExceptionLog>();
            return objExceptionLogs;
        }
    }
}
