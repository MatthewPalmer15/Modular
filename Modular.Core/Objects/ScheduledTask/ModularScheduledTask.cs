using Microsoft.Data.SqlClient;
using Modular.Core.Audit;
using Modular.Core.Databases;
using Modular.Core.Utility;
using System.Data;
using System.Globalization;

namespace Modular.Core.ScheduledTasks
{
    public class ScheduledTask : ModularBase
    {

        #region "  Constructors  "

        public ScheduledTask()
        {
        }

        #endregion

        #region "  Variables  "

        private string _Name = string.Empty;

        private string _Description = string.Empty;

        private int _TimeInSeconds;

        private DateTime _LastRunTime;

        private DateTime _NextRunTime;

        private string _StoredProcedure;

        private bool _Enabled;

        #endregion

        #region "  Properties  "

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        public int TimeInSeconds
        {
            get
            {
                return _TimeInSeconds;
            }
        }


        public DateTime LastRunTime
        {
            get
            {
                return _LastRunTime;
            }
            private set
            {
                if (_LastRunTime != value)
                {
                    _LastRunTime = value;
                    OnPropertyChanged("LastRunTime");
                }
            }
        }

        public DateTime NextRunTime
        {
            get
            {
                return _NextRunTime;
            }
            private set
            {
                if (_NextRunTime != value)
                {
                    _NextRunTime = value;
                    OnPropertyChanged("NextRunTime");
                }
            }
        }

        public string StoredProcedureName
        {
            get
            {
                return _StoredProcedure;
            }
            set
            {
                if (_StoredProcedure != value)
                {
                    _StoredProcedure = value;
                    OnPropertyChanged("StoredProcedureName");
                }
            }
        }

        public bool Enabled
        {
            get
            {
                return _Enabled;
            }
            set
            {
                if (_Enabled != value)
                {
                    _Enabled = value;
                    OnPropertyChanged("Enabled");
                }
            }
        }

        #endregion

        #region "  Public Methods  "

        public void SetTime(EnumUtils.TimeType TimeType, int value)
        {
            switch(TimeType)
            {
                case EnumUtils.TimeType.Seconds:
                    _TimeInSeconds = value;
                    break;

                case EnumUtils.TimeType.Minutes:
                    _TimeInSeconds = value * 60;
                    break;

                 case EnumUtils.TimeType.Hours:
                    _TimeInSeconds = (value * 60) * 60;
                    break;

                case EnumUtils.TimeType.Days:
                    _TimeInSeconds = ((value * 60) * 60) * 24;
                    break;

                case EnumUtils.TimeType.Weeks:
                    _TimeInSeconds = (((value * 60) * 60) * 24) * 7;
                    break;

                case EnumUtils.TimeType.Months:
                    _TimeInSeconds = ((((value * 60) * 60) * 24) * 7) * 4;
                    break;

                case EnumUtils.TimeType.Years:
                    _TimeInSeconds = (((((value * 60) * 60) * 24) * 7) * 4) * 12;
                    break;

                default:
                    throw new ModularException(ExceptionType.InvalidCast, "Time Type was not defined.");
            }
        }

        public void Execute()
        {
            if (Enabled)
            {
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
                                    Command.CommandText = StoredProcedureName;
                                    Command.ExecuteNonQuery();
                                }
                                Connection.Close();
                            }
                            
                            AuditLog.Create(ObjectTypes.ObjectType.ScheduledTask, ID, $"Scheduled Task: {Name} ran successfully at {DateTime.Now.ToString(ModularUtils.DateFormatString)}");
                            _LastRunTime = DateTime.Now;
                            _NextRunTime = DateTime.Now.AddSeconds(TimeInSeconds);
                            
                            break;

                        case Database.DatabaseConnectivityMode.Local:
                            throw new ModularException(ExceptionType.NotImplemented, "This has not been implemented for local databases yet.");

                        default:
                            throw new ModularException(ExceptionType.DatabaseConnectivityNotDefined, "Database Connection Mode was not defined.");

                    }
                }
                else
                {
                    throw new ModularException(ExceptionType.DatabaseConnectionError, $"There was an issue trying to connect to the database.");
                }
            }

        }

        #endregion

        #region "  Private Methods  "

        #endregion

    }
}
