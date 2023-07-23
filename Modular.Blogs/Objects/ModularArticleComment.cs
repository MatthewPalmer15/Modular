using Modular.Core;
using Modular.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Blogs
{
    [Serializable]
    public class ArticleComment : ModularBase
    {

        #region "  Constructors  "

        public ArticleComment()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Article_Comment";

        #endregion

        #region "  Variables  "

        private Guid _ArticleID;

        private Guid _ContactID;

        private string _Message = string.Empty;

        #endregion

        #region "  Properties  "

        public Guid ArticleID
        {
            get
            {
                return _ArticleID;
            }
            set
            {
                if (_ArticleID != value)
                {
                    _ArticleID = value;
                    OnPropertyChanged("ArticleID");
                }
            }
        }

        public Article Article
        {
            get
            {
                return Article.Load(ArticleID);
            }
        }

        public Guid ContactID
        {
            get
            {
                return _ContactID;
            }
            set
            {
                if (_ContactID != value)
                {
                    _ContactID = value;
                    OnPropertyChanged("ContactID");
                }
            }
        }

        public Core.Entity.Contact Contact
        {
            get
            {
                return Core.Entity.Contact.Load(ContactID);
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
                    OnPropertyChanged("Message");
                }
            }
        }

        #endregion

        #region "  Static Methods  "

        public static new ArticleCategory Create()
        {
            ArticleCategory obj = new ArticleCategory();
            obj.SetDefaultValues();
            return obj;
        }

        public static new ArticleCategory Load(Guid ID)
        {
            ArticleCategory obj = new ArticleCategory();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Message;
        }

        #endregion

    }
}
