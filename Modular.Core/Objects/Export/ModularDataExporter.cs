using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Core
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

        #region "  Methods  "

        public static List<DataExporterItem> GetScripts()
        {
           if (ScriptFolder.Exists)
            {
                List<DataExporterItem> AllDataExportScripts = new List<DataExporterItem>();

                foreach (var ScriptFile in ScriptFolder.GetFiles("*.sql"))
                {
                    using (StreamReader StreamReader = new StreamReader(ScriptFile.FullName))
                    {
                        DataExporterItem DataExportScript = new DataExporterItem();
                        DataExportScript.Name = ScriptFile.Name;
                        do
                        {
                            string ScriptLine = StreamReader.ReadLine();
                            if (ScriptLine.StartsWith("-- "))
                            {
                                string[] SplitScriptLine = ScriptLine.Split(new string[] { "-- " }, StringSplitOptions.None);
                                if (SplitScriptLine.Length > 1)
                                {
                                    string[] SplitScriptLine2 = SplitScriptLine[1].Split(new string[] { ": " }, StringSplitOptions.None);
                                    if (SplitScriptLine2.Length > 1)
                                    {
                                        switch (SplitScriptLine2[0].ToUpper())
                                        {
                                            case "DESCRIPTION":
                                                DataExportScript.Description = SplitScriptLine2[1];
                                                break;

                                            case "CATEGORY":
                                                DataExportScript.Category = SplitScriptLine2[1];
                                                break;

                                            case "FORMAT":
                                                DataExportScript.Format = (DataExporterItem.ExportType)Enum.Parse(typeof(DataExporterItem.ExportType), SplitScriptLine2[1]);
                                                break;
                                        }
                                    }
                                }
                            }
                        } while (!StreamReader.EndOfStream);
                        AllDataExportScripts.Add(DataExportScript);
                    }
                }
                return AllDataExportScripts;
            }
           else
            {
                ScriptFolder.Create();
                return new List<DataExporterItem>();
            }
        }

        public static void ExportScript(DataExporterItem Item)
        {
            if (Database.CheckDatabaseConnection())
            {
                switch(Database.ConnectionMode)
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

                                SqlDataReader sqlDataReader = Command.ExecuteReader();
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

        #endregion

        //public static void ExportToExcel(string fileName, string sheetName, string[] headers, string[,] data)
        //{
        //    var excel = new Microsoft.Office.Interop.Excel.Application();
        //    excel.Visible = false;
        //    excel.DisplayAlerts = false;
        //    var workbook = excel.Workbooks.Add(Type.Missing);
        //    var sheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.ActiveSheet;
        //    sheet.Name = sheetName;
        //    for (int i = 0; i < headers.Length; i++)
        //    {
        //        sheet.Cells[1, i + 1] = headers[i];
        //    }
        //    for (int i = 0; i < data.GetLength(0); i++)
        //    {
        //        var row = new string[data.GetLength(1)];
        //        for (int j = 0; j < data.GetLength(1); j++)
        //        {
        //            row[j] = data[i, j];
        //        }
        //        for (int j = 0; j < row.Length; j++)
        //        {
        //            sheet.Cells[i + 2, j + 1] = row[j];
        //        }
        //    }
        //    workbook.SaveAs(fileName);
        //    workbook.Close();
        //    excel.Quit();
        //}

    }
}
