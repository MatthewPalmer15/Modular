using Modular.Core;

namespace Modular.Schooling
{
    public class CourseCategory : ModularBase
    {

        #region "  Constructors  "

        public CourseCategory()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Course_Category";

        #endregion

        #region "  Variables  "

        private string _Name = string.Empty;

        private string _Description = string.Empty;

        #endregion

        #region "  Properties  "

        public string Name
        {
            get
            {
                return _Name;
            }
            private set
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
            private set
            {
                if (_Description != value)
                {
                    _Description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        #endregion

        #region "  Static Methods  "

        public static new CourseCategory Create()
        {
            CourseCategory obj = new CourseCategory();
            obj.SetDefaultValues();
            return obj;
        }

        public static new CourseCategory Load(Guid ID)
        {
            CourseCategory obj = new CourseCategory();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Name;
        }

        #endregion

    }
}