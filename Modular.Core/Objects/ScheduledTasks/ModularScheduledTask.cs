using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Core.Tasks
{
    public class ScheduledTask : ModularBase
    {

        #region "  Constructors  "

        public ScheduledTask() 
        { 
        }

        #endregion

        #region "  Enums  "

        public enum ScheduledTaskType
        {
            None,
            File,
            StoredProcedure
        }

        #endregion

        #region "  Variables  "

        private string _Name = string.Empty;

        private string _Description = string.Empty;

        private TimeOnly _Time;

        private string _FileName;

        private string _StoredProcedureName;

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

        public TimeOnly Time
        {
            get
            {
                return _Time;
            }
            set
            {
                if (_Time != value)
                {
                    _Time = value;
                    OnPropertyChanged("Time");
                }
            }
        }

        public ScheduledTaskType Mode
        {
            get
            {
                if (FileName.Trim().Length > 0)
                {
                    return ScheduledTaskType.File;
                }
                else if (StoredProcedureName.Trim().Length > 0)
                {
                    return ScheduledTaskType.StoredProcedure;
                }
                else
                {
                    return ScheduledTaskType.None;
                }
            }
        }

        public string FileName
        {
            get
            {
                return _FileName;
            }
            set
            {
                if (_FileName != value)
                {
                    _FileName = value;
                    OnPropertyChanged("FileName");
                }
                _StoredProcedureName = string.Empty;
            }
        }

        public string StoredProcedureName
        {
            get
            {
                return _StoredProcedureName;
            }
            set
            {
                if (_StoredProcedureName != value)
                {
                    _StoredProcedureName = value;
                    OnPropertyChanged("StoredProcedureName");
                }
                _FileName = string.Empty;
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

    }
}
