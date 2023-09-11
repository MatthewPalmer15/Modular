using Modular.Core.Configuration;

namespace Modular.Core.Exporter
{

    public static class DataExporter
    {

        #region "  Variables  "

        private readonly static string _ScriptPath = AppConfig.GetValue("FolderPath:DataExporterScript");

        private readonly static string _ExportPath = AppConfig.GetValue("FolderPath:DataExporterExport");

        #endregion

        #region "  Properties  "

        public static DirectoryInfo ScriptFolder
        {
            get
            {
                return new DirectoryInfo(_ScriptPath);
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

        public static void ExcecuteScript(DataExporterItem Item)
        {
            Item.Excecute();
        }

        public static DataExporterItem GetScript(string ScriptName)
        {
            if (ScriptFolder.Exists)
            {
                foreach (FileInfo ScriptFile in ScriptFolder.GetFiles("*.sql"))
                {
                    if (ScriptFile.Name == ScriptName)
                    {
                        return new DataExporterItem(ScriptFile);
                    }
                }
                return new DataExporterItem();
            }
            else
            {
                ScriptFolder.Create();
                return new DataExporterItem();
            }
        }

        public static List<DataExporterItem> GetAllScripts()
        {
            if (ScriptFolder.Exists)
            {
                List<DataExporterItem> AllDataExportScripts = new List<DataExporterItem>();

                foreach (FileInfo ScriptFile in ScriptFolder.GetFiles("*.sql"))
                {
                    AllDataExportScripts.Add(new DataExporterItem(ScriptFile));
                }
                return AllDataExportScripts;
            }
            else
            {
                ScriptFolder.Create();
                return new List<DataExporterItem>();
            }
        }

        #endregion
    }
}
