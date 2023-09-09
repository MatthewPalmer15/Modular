namespace Modular.Core.Documents
{
    public static class DocumentUtils
    {

        #region "  Methods  "

        public static Document GetLatestDocument(string DocumentName)
        {
            return GetLatestDocument(DocumentName, DateTime.Now);
        }

        public static Document GetLatestDocument(string DocumentName, DateTime ValidFrom)
        {
            return Document
                    .LoadList()
                    .Where(Document => Document.Filename == DocumentName)
                    .OrderBy(Document => Document.ValidFrom)
                    .First(Document => Document.ValidFrom <= ValidFrom);
        }

        public static Document GetFirstDocument(string DocumentName)
        {
            return Document
                    .LoadList()
                    .Where(Document => Document.Filename == DocumentName)
                    .OrderBy(Document => Document.ValidFrom)
                    .First();
        }

        public static Document GetSpecificDocument(string DocumentName, int Version)
        {
            return Document
                .LoadList()
                .SingleOrDefault(Document => Document.Version == Version && Document.Filename == DocumentName);
        }

        #endregion



    }
}
