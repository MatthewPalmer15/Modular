namespace Modular.Core
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
            return Document.LoadAll(DocumentName).OrderBy(Document => Document.ValidFrom).First(Document => Document.ValidFrom <= ValidFrom);
        }

        public static Document GetFirstDocument(string DocumentName)
        {
            return Document.LoadAll(DocumentName).OrderBy(Document => Document.ValidFrom).First();
        }

        public static Document GetSpecificDocument(string DocumentName, int Version)
        {
            return Document.LoadAll(DocumentName).Find(Document => Document.Version == Version);
        }

        #endregion



    }
}
