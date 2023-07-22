using Modular.Core;
using Modular.Core.Entity;

namespace Modular.Blogs
{
    [Serializable]
    public class Article : ModularBase
    {

        #region "  Constructors  "

        public Article()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Article";

        #endregion

        #region "  Variables  "

        private string _Title = string.Empty;

        private string _Body = string.Empty;

        private string _Summary = string.Empty;

        private Guid _AuthorID;

        private Guid _CategoryID;

        private bool _IsPublished;

        private bool _AllowComments;

        #endregion

        #region "  Properties  "

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

        public string Body
        {
            get
            {
                return _Body;
            }
            set
            {
                if (_Body != value)
                {
                    _Body = value;
                    OnPropertyChanged("Body");
                }
            }
        }

        public string Summary
        {
            get
            {
                return _Summary;
            }
            set
            {
                if (_Summary != value)
                {
                    _Summary = value;
                    OnPropertyChanged("Summary");
                }
            }
        }

        public Guid AuthorID
        {
            get
            {
                return _AuthorID;
            }
            set
            {
                if (_AuthorID != value)
                {
                    _AuthorID = value;
                    OnPropertyChanged("AuthorID");
                }
            }
        }

        public Account Author
        {
            get
            {
                return Account.Load(AuthorID);
            }
        }

        public Guid CategoryID
        {
            get
            {
                return _CategoryID;
            }
            set
            {
                if (_CategoryID != value)
                {
                    _CategoryID = value;
                    OnPropertyChanged("CategoryID");
                }
            }
        }

        public bool IsPublished
        {
            get
            {
                return _IsPublished;
            }
            set
            {
                if (_IsPublished != value)
                {
                    _IsPublished = value;
                    OnPropertyChanged("IsPublished");
                }
            }
        }

        public bool AllowComments
        {
            get
            {
                return _AllowComments;
            }
            set
            {
                if (_AllowComments != value)
                {
                    _AllowComments = value;
                    OnPropertyChanged("AllowComments");
                }
            }
        }

        #endregion

        #region "  Static Methods  "

        public static new Article Create()
        {
            Article obj = new Article();
            obj.SetDefaultValues();
            return obj;
        }

        public static new Article Load(Guid ID)
        {
            Article obj = new Article();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Title;
        }

        #endregion

    }
}
