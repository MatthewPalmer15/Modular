using Modular.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular
{
    public class MDFile : ModularBase
    {

        #region "  Constructors  "
        
        public MDFile()
        {
        }

        #endregion

        #region "  Variables  "

        private string _FilePath;

        private byte[] _FileBytes;
        
        private string _FileName;

        private string _FileExtension;

        #endregion

        #region "  Properties  "

        public string FilePath
        {
            get
            {
                return _FilePath;
            }
            set
            {
                if (_FilePath != value)
                {
                    _FileBytes = Array.Empty<byte>();
                    _FilePath = value;
                    OnPropertyChanged("FilePath");
                    OnPropertyChanged("FileBytes");
                }
            }
        }
        
        public byte[] FileBytes
        {
            get
            {
                return _FileBytes;
            }
            set
            {
                if (_FileBytes != value)
                {
                    _FilePath = string.Empty;
                    _FileBytes = value;
                    OnPropertyChanged("FilePath");
                    OnPropertyChanged("FileBytes");
                }
            }
        }

        public string FileName
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
                    OnPropertyChanged("FileName");
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

        #endregion
    }
}
