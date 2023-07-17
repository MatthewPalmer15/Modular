using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Core.Objects.Template
{
    public struct FileTemplateItem
    {

        public FileTemplateItem()
        {
            FileName = "Unknown";
            FileExtension = "Unknown";
            FilePath = "Unknown";
        }

        public FileTemplateItem(FileInfo File)
        {
            FileName = File.Name;
            FileExtension = File.Extension;
            FilePath = File.FullName;
        }

        public string FileName { get; set; }

        public string FileExtension { get; set; }

        public string FilePath { get; set; }

    }
}
