using Modular.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Blogs
{
    [Serializable]
    public class ArticleCategory : ModularBase
    {

        #region "  Constructors  "

        public ArticleCategory()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Article_Category";

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
            return Name;
        }

        #endregion

    }
}
