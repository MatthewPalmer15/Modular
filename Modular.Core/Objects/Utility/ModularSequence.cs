namespace Modular.Core
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
            set
            {
                if (value != _Count)
                {
                    _Count = value;
                    OnPropertyChanged("Count");
                }
            }
        }

        #endregion




    }
}
