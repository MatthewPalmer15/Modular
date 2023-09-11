using Modular.Core.Configuration;

namespace Modular.Core.Templates
{
    public static class FileTemplate
    {

        #region "  Variables  "

        private readonly static string _TemplatePath = AppConfig.GetValue("FolderPath:FileTemplateFolder");

        private readonly static string _ExportPath = AppConfig.GetValue("FolderPath:FileExportFolder");

        #endregion

        #region "  Properties  "

        public static DirectoryInfo TemplateFolder
        {
            get
            {
                return new DirectoryInfo(_TemplatePath);
            }
        }

        public static DirectoryInfo ExportFolder
        {
            get
            {
                if (!string.IsNullOrEmpty(_ExportPath))
                {
                    return new DirectoryInfo(_ExportPath);
                }
                else
                {
                    return new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads");
                }
            }
        }

        #endregion

        #region "  Public Methods  "

        public static FileTemplateItem GetFileTemplate(string FileName)
        {
            if (TemplateFolder.Exists)
            {
                foreach (FileInfo File in TemplateFolder.GetFiles())
                {
                    if (File.Name == FileName)
                    {
                        return new FileTemplateItem(File);
                    }
                }
                return new FileTemplateItem();

            }
            else
            {
                TemplateFolder.Create();
                return new FileTemplateItem();
            }
        }

        public static List<FileTemplateItem> GetFileTemplates()
        {
            if (TemplateFolder.Exists)
            {
                List<FileTemplateItem> FileTemplates = new List<FileTemplateItem>();
                foreach (FileInfo File in TemplateFolder.GetFiles())
                {
                    FileTemplates.Add(new FileTemplateItem(File));
                }
                return FileTemplates;
            }
            else
            {
                TemplateFolder.Create();
                return new List<FileTemplateItem>();
            }
        }

        #endregion

    }
}
