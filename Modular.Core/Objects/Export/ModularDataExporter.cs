using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Core
{

    public static partial class DataExporter
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
            if (Database.CheckDatabaseConnection())
            {
                switch (Database.ConnectionMode)
                {
                    case Database.DatabaseConnectivityMode.Remote:
                        using (SqlConnection Connection = new SqlConnection())
                        {
                            Connection.Open();

                            using (SqlCommand Command = new SqlCommand())
                            {
                                Command.Connection = Connection;
                                Command.CommandType = CommandType.Text;
                                Command.CommandText = File.ReadAllText(Item.Name);

                                SqlDataReader DataReader = Command.ExecuteReader();
                                ExportToExcel(Item, DataReader);
                            }

                            Connection.Close();
                        }

                        break;

                    case Database.DatabaseConnectivityMode.Local:
                        break;
                }
            }
            else
            {
                throw new ModularException(ExceptionType.DatabaseConnectionError, "Database connection is not available.");
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
