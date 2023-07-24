using Modular.Core;

namespace Modular.Schooling
{
    [Serializable]
    public class CourseContent : ModularBase
    {

        #region "  Constructors  "

        public CourseContent()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_CourseContent";

        #endregion

        #region "  Variables  "

        private Guid _CourseModuleID;

        private string _Title = string.Empty;

        private string _Content = string.Empty;

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

        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                if (_Title != value)
                {
                    _Title = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        public string Content
        {
            get
            {
                return _Content;
            }
            set
            {
                if (_Content != value)
                {
                    _Content = value;
                    OnPropertyChanged("Content");
                }
            }
        }

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Creates a new instance with default values
        /// </summary>
        /// <returns>A new instance</returns>
        public static new CourseContent Create()
        {
            CourseContent obj = new CourseContent();
            obj.SetDefaultValues();
            return obj;
        }

        public static new CourseContent Load(Guid ID)
        {
            CourseContent obj = new CourseContent();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Title;
        }

        public override CourseContent Clone()
        {
            return CourseContent.Load(ID);
        }

        #endregion

    }
}