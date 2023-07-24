using Modular.Core;
using System.ComponentModel;
using System.Reflection;

namespace Modular.Schooling
{
    [Serializable]
    public class CourseModule : ModularBase
    {

        #region "  Constructors  "

        public CourseModule()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_CourseModule";

        #endregion

        #region "  Enums  "

        public enum ModuleType
        {
            Unknown = 0,
            OnSite = 1,
            Online = 2,
            Both = 3
        }

        #endregion

        #region "  Variables  "

        private int _ModuleNumber;

        private Guid _CourseID;

        private string _Name = string.Empty;

        private string _Description = string.Empty;

        private ModuleType _CourseModuleType;

        private List<CourseQuestion> _Questions = new List<CourseQuestion>();

        private List<CourseContent> _Contents = new List<CourseContent>();

        private bool _AskQuestionsAtEnd;

        #endregion

        #region "  Properties  "

        public int ModuleNumber
        {
            get
            {
                return _ModuleNumber;
            }
            set
            {
                if (_ModuleNumber != value)
                {
                    _ModuleNumber = value;
                    OnPropertyChanged("ModuleNumber");
                }
            }
        }

        public Guid CourseID
        {
            get
            {
                return _CourseID;
            }
            set
            {
                if (_CourseID != value)
                {
                    _CourseID = value;
                    OnPropertyChanged("CourseID");
                }
            }
        }

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

        public ModuleType Type
        {
            get
            {
                return _CourseModuleType;
            }
            set
            {
                if (_CourseModuleType != value)
                {
                    _CourseModuleType = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        public List<CourseQuestion> Questions
        {
            get
            {
                LoadCourseQuestions();
                return _Questions;
            }
        }

        public List<CourseContent> Contents
        {
            get
            {
                LoadCourseContents();
                return _Contents;
            }
        }

        [DefaultValue(true)]
        public bool AskQuestionsAtEnd
        {
            get
            {
                return _AskQuestionsAtEnd;
            }
            set
            {
                if (_AskQuestionsAtEnd != value)
                {
                    _AskQuestionsAtEnd = value;
                    OnPropertyChanged("AskQuestionsAtEnd");
                }
            }
        }

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Creates a new instance with default values
        /// </summary>
        /// <returns>A new instance</returns>
        public static new CourseModule Create()
        {
            CourseModule obj = new CourseModule();
            obj.SetDefaultValues();
            return obj;
        }

        public static new CourseModule Load(Guid ID)
        {
            CourseModule obj = new CourseModule();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        private void LoadCourseQuestions()
        {
            _Questions = new List<CourseQuestion>();

        }

        private void LoadCourseContents()
        {
            _Contents = new List<CourseContent>();
            // return CourseModule.LoadByCourseID(ID);
        }

        public override string ToString()
        {
            return $"Module #{_ModuleNumber}";
        }

        public override Course Clone()
        {
            return Course.Load(ID);
        }

        #endregion

    }
}