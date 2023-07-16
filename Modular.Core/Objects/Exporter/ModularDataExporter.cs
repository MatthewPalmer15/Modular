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
                                ExportToFile(Item, DataReader);
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

        #region "  Private Methods  "

        private static void ExportToFile(DataExporterItem Script, SqlDataReader DataReader)
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


                // Get the sheet data
                SheetData sheetData = ExcelWorksheetPart.Worksheet.GetFirstChild<SheetData>();

                // Add headers to the first row
                Row headerRow = new Row();
                int columnIndex = 1;
                for (int i = 0; i < DataReader.FieldCount; i++)
                {
                    DocumentFormat.OpenXml.Spreadsheet.Cell cell = CreateTextCell(GetColumnName(columnIndex), DataReader.GetName(i));
                    headerRow.AppendChild(cell);
                    columnIndex++;
                }
                sheetData.AppendChild(headerRow);

                // Add data rows
                while (DataReader.Read())
                {
                    Row dataRow = new Row();
                    columnIndex = 1;
                    for (int i = 0; i < DataReader.FieldCount; i++)
                    {
                        DocumentFormat.OpenXml.Spreadsheet.Cell cell = CreateTextCell(GetColumnName(columnIndex), DataReader.GetValue(i).ToString());
                        dataRow.AppendChild(cell);
                        columnIndex++;
                    }
                    sheetData.AppendChild(dataRow);
                }

                ExcelDocument.Save();
            }
        }

        private static DocumentFormat.OpenXml.Spreadsheet.Cell CreateTextCell(string columnName, string cellValue)
        {
            DocumentFormat.OpenXml.Spreadsheet.Cell cell = new DocumentFormat.OpenXml.Spreadsheet.Cell()
            {
                DataType = CellValues.String,
                CellReference = columnName,
                CellValue = new CellValue(cellValue)
            };
            return cell;
        }

        private static string GetColumnName(int columnIndex)
        {
            int dividend = columnIndex;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (dividend - modulo) / 26;
            }

            return columnName;
        }


        #endregion
    }
}
