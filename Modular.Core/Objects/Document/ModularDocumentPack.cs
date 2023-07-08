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

    }
}
