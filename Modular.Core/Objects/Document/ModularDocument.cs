namespace Modular.Core
{
    public class Document : ModularBase
    {

        #region "  Constructors  "

        public Document()
        {
        }

        #endregion

        #region "  Constants  "

        protected static readonly new string MODULAR_DATABASE_TABLE = "tbl_Modular_Document";

        #endregion

        #region "  Enums  "

        public enum FileType
        {
            Unknown = 0,
            PDF = 1,
            DOC = 2,
            DOCX = 3,
            XLS = 4,
            XLSX = 5,
            PPT = 6,
            PPTX = 7,
            TXT = 8,
            MSG = 9,
        }

        #endregion

        #region "  Variables  "

        private Guid _DocumentPackID;

        private string _FileName = string.Empty;

        private string _FileExtension = string.Empty;

        private byte[] _FileData = Array.Empty<byte>();

        private int _Version;

        private DateTime _ValidFrom;

        private DateTime _ValidTo;

        private bool _IsStatic;

        #endregion

        #region "  Properties  "

        public Guid DocumentPackID
        {
            get
            {
                return _DocumentPackID;
            }
            set
            {
                if (_DocumentPackID != value)
                {
                    _DocumentPackID = value;
                    OnPropertyChanged("DocumentPackID");
                }
            }
        }

        public string Filename
        {
            get
            {
                return _FileName;
            }
            set
            {
                if (_FileName != value)
                {
                    _FileName = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public string FileExtension
        {
            get
            {
                return _FileExtension;
            }
            set
            {
                if (_FileExtension != value)
                {
                    _FileExtension = value;
                    OnPropertyChanged("FileExtension");
                }
            }
        }

        public string FullFilename
        {
            get
            {
                return $"{Filename}.{FileExtension}";
            }
        }

        public byte[] FileData
        {
            get
            {
                return _FileData;
            }
            set
            {
                if (_FileData != value)
                {
                    _FileData = value;
                    OnPropertyChanged("FileData");
                }
            }
        }

        public int Version
        {
            get
            {
                return _Version;
            }
            set
            {
                if (_Version != value)
                {
                    _Version = value;
                    OnPropertyChanged("Version");
                }
            }
        }

        public DateTime ValidFrom
        {
            get
            {
                return _ValidFrom;
            }
            set
            {
                if (_ValidFrom != value)
                {
                    _ValidFrom = value;
                    OnPropertyChanged("ValidFrom");
                }
            }
        }

        public DateTime ValidTo
        {
            get
            {
                return _ValidTo;
            }
            set
            {
                if (_ValidTo != value)
                {
                    _ValidTo = value;
                    OnPropertyChanged("ValidTo");
                }
            }
        }

        public bool IsStatic
        {
            get
            {
                return _IsStatic;
            }
            set
            {
                if (_IsStatic != value)
                {
                    _IsStatic = value;
                    OnPropertyChanged("IsStatic");
                }
            }
        }

        #endregion

        #region "  Static Methods  "

        public static new List<Document> LoadInstances()
        {
            return new List<Document>();
        }

        public static new Document Create()
        {
            Document obj = new Document();
            obj.SetDefaultValues();
            return obj;
        }

        public static new Document Load(Guid ID)
        {
            Document obj = new Document();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Filename;
        }

        #endregion

    }
}
