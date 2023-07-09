namespace Modular.Core
{
    public class DocumentPack : ModularBase
    {

        #region "  Constructors  "

        public DocumentPack()
        {
        }

        #endregion

        #region "  Constants  "

        protected static readonly new string MODULAR_DATABASE_TABLE = "tbl_Modular_DocumentPack";

        #endregion

        #region "  Variables  "

        private string _Name = string.Empty;

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

        public List<Document> Documents
        {
            get
            {
                return Document.LoadInstances().Where(RolePermission => RolePermission.DocumentPackID == ID).ToList();
            }
        }

        #endregion

        #region "  Static Methods  "

        public static new List<DocumentPack> LoadInstances()
        {
            return new List<DocumentPack>();
        }

        public static new DocumentPack Create()
        {
            DocumentPack obj = new DocumentPack();
            obj.SetDefaultValues();
            return obj;
        }

        public static new DocumentPack Load(Guid ID)
        {
            DocumentPack obj = new DocumentPack();
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
