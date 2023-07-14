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

                                SqlDataReader DataReader = Command.ExecuteReader();
                                if (Item.Format.Equals(DataExporterItem.ExportType.XLSX))
                                {
                                    ExportToExcel(Item, DataReader);
                                }
                                else
                                {
                                    throw new ModularException(ExceptionType.ArgumentError, "Export format is not supported.");
                                }
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

        private static void ExportToExcel(DataExporterItem Script, SqlDataReader DataReader)
        {
            using (SpreadsheetDocument ExcelDocument = SpreadsheetDocument.Create(_ExportPath, SpreadsheetDocumentType.Workbook))
            {
                // Create the workbook part
                WorkbookPart ExcelDocumentPart = ExcelDocument.AddWorkbookPart();
                ExcelDocumentPart.Workbook = new Workbook();

                // Create the worksheet part
                WorksheetPart ExcelWorksheetPart = ExcelDocumentPart.AddNewPart<WorksheetPart>();
                ExcelWorksheetPart.Worksheet = new Worksheet(new SheetData());

                // Create the sheets
                Sheets ExcelSheets = ExcelDocument.WorkbookPart.Workbook.AppendChild(new Sheets());

                // Create a new sheet and associate it with the worksheet part
                Sheet ExcelSheet = new Sheet()
                {
                    Id = ExcelDocument.WorkbookPart.GetIdOfPart(ExcelWorksheetPart),
                    SheetId = 1,
                    Name = Script.Name
                };

                ExcelSheets.Append(ExcelSheet);

                ExcelDocument.Save();
            }
        }

    }
}
