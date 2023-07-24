using Modular.Core;

namespace Modular.Schooling
{
    public class CourseQuestion : ModularBase
    {

        #region "  Constructors  "

        public CourseQuestion()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_CourseQuestion";

        #endregion

        #region "  Variables  "

        private Guid _CourseModuleID;

        private string _Question = string.Empty;

        private string _Answer = string.Empty;

        private int _Points;

        #endregion

        #region "  Properties  "

        public Guid CourseModuleID
        {
            get
            {
                return _CourseModuleID;
            }
            set
            {
                if (_CourseModuleID != value)
                {
                    _CourseModuleID = value;
                    OnPropertyChanged("CourseModuleID");
                }
            }
        }

        public string Question
        {
            get
            {
                return _Question;
            }
            set
            {
                if (_Question != value)
                {
                    _Question = value;
                    OnPropertyChanged("Question");
                }
            }
        }

        public string Answer
        {
            get
            {
                return _Answer;
            }
            set
            {
                if (_Answer != value)
                {
                    _Answer = value;
                    OnPropertyChanged("Answer");
                }
            }
        }

        public int Points
        {
            get
            {
                return _Points;
            }
            set
            {
                if (_Points != value)
                {
                    _Points = value;
                    OnPropertyChanged("Points");
                }
            }
        }

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Creates a new instance with default values
        /// </summary>
        /// <returns>A new instance</returns>
        public static new CourseQuestion Create()
        {
            CourseQuestion obj = new CourseQuestion();
            obj.SetDefaultValues();
            return obj;
        }

        public static new CourseQuestion Load(Guid ID)
        {
            CourseQuestion obj = new CourseQuestion();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return _Question;
        }

        public override CourseQuestion Clone()
        {
            return CourseQuestion.Load(ID);
        }

        #endregion

    }
}