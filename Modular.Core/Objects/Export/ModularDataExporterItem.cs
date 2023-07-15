using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Core
{
    public struct DataExporterItem
    {

        #region "  Constructors  "

        public DataExporterItem()
        {
            FileName = "Unknown";
            FilePath = "Unknown";
        }

        public DataExporterItem(FileInfo File)
        {
            using (StreamReader StreamReader = new StreamReader(File.FullName))
            {
                FileName = File.Name;
                FilePath = File.FullName;

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
                                    case "NAME":
                                        Name = SplitScriptLine2[1];
                                        break;

                                    case "DESCRIPTION":
                                        Description = SplitScriptLine2[1];
                                        break;

                                    case "CATEGORY":
                                        Category = SplitScriptLine2[1];
                                        break;
                                }
                            }
                        }
                    }
                } while (!StreamReader.EndOfStream);
            }
        }

        #endregion

        #region "  Properties  "

        public string FileName { get; private set; }

        public string FilePath { get; private set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        #endregion

    }
}
