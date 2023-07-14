using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Core
{
    public struct DataExporterItem
    {

        public enum ExportType
        {
            Unknown,
            DOCX,
            XLSX,
            PPTX,
            PDF,
            CSV,
            TXT,
            XML,
            JSON,
            HTML
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public ExportType Format { get; set; }


    }
}
