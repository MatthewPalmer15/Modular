using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Core
{
    public static class DocumentTemplate
    {

        #region "  Variables  "

        private readonly static string _TemplatePath = AppConfig.GetValue("FolderPath:DocumentTemplate");

        #endregion

        #region "  Properties  "

        public static DirectoryInfo TemplateFolder
        {
            get
            {
                return new DirectoryInfo(_TemplatePath);
            }
        }

        #endregion

    }
}
