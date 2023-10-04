using Modular.Core.Attributes;

namespace Modular.Core.Sequences
{
    public class Sequence : ModularBase
    {

        #region "  Constructors  "

        public Sequence()
        {
        }

        #endregion

        #region "  Constructors  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Sequence";

        #endregion

        #region "  Variables  "

        private string _Name = string.Empty;

        private string _Description = string.Empty;

        private int _Count;

        #endregion

        #region "  Properties  "

        [Unique]
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

        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        public int Count
        {
            get
            {
                return _Count;
            }
            private set
            {
                if (value != _Count)
                {
                    _Count = value;
                    OnPropertyChanged("Count");
                }
            }
        }

        #endregion

        #region "  Static Methods  "

        public static new Sequence Create()
        {
            Sequence obj = new Sequence();
            obj.SetDefaultValues();
            return obj;
        }

        public static new Sequence Load(Guid ID)
        {
            Sequence obj = new Sequence();
            obj.Fetch(ID);
            return obj;
        }

        public static Sequence Load(string Name)
        {
            Sequence obj = new Sequence();
            obj.Fetch(Class.GetField("Key"), Name);
            return obj;
        }

        public static int GetNextNumber(string Name)
        {
            Sequence obj = new Sequence();
            obj.Fetch(Class.GetField("Key"), Name);
            obj.Count++;
            obj.Save();

            return obj.Count;
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
