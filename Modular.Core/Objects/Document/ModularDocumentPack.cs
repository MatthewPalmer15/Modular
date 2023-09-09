namespace Modular.Core.Documents
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

        private Guid _ContactID;

        private string _Name = string.Empty;

        private List<Document> _Documents = new List<Document>();

        #endregion

        #region "  Properties  "

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
                LoadDocuments();
                return _Documents;
            }
        }

        #endregion

        #region "  Static Methods  "

        public static new List<DocumentPack> LoadList()
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

        public void LoadDocuments()
        {
            _Documents = Document.LoadList().Where(Document => Document.DocumentPackID == ID).ToList();
        }

        public override string ToString()
        {
            return Name;
        }

        #endregion

    }
}
