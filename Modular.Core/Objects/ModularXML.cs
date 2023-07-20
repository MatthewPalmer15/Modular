//using Microsoft.Data.SqlClient;
//using System.IO;
//using System.Xml.Serialization;
//
//namespace Modular.Base.Security
//{
//
//    public class Permission : ModularBase
//    {
//        #region "  Constructors  "
//
//        public AccountPermission() 
//        {
//            ApplicationData = ApplicationDataModel(this);
//        }
//
//        #endregion
//
//        #region "  Variables  "
//
//        private Guid _ContactID;
//
//        private string _PermissionsXML = string.Empty;
//
//        #endregion
//
//        #region "  Properties  "
//
//        protected bool IsDeserialising;
//
//        public bool IsLoaded
//        {
//            get
//            {
//                return !ID.Equals(Guid.Empty);
//            }
//        }
//
//        public Guid ContactID
//        {
//            get
//            { 
//                return _ContactID; 
//            }
//            set 
//            { 
//                if (_ContactID != value)
//                {
//                    _ContactID = value;
//                    OnPropertyChanged("ContactID");
//                }
//            }
//        }
//
//        public string PermissionXML
//        {
//            get
//            {
//                return _PermissionsXML;
//            }
//            set
//            {
//                if (_PermissionsXML != value)
//                {
//                    _PermissionsXML = value;
//                    OnPropertyChanged("PermissionsXML");
//                }
//            }
//        }
//
//        public ApplicationDataModel ApplicationData;
//
//        #endregion
//
//        #region "  Serialisation  "
//
//        private void SerializeToXML()
//        {
//            using (StringWriter Writer = new StringWriter())
//            {
//                XmlSerializer Serializer = new XmlSerializer(typeof(ApplicationDataModel));
//                Serializer.Serialize(Writer, ApplicationData);
//                PermissionXML = Writer.ToString();
//            }
//        }
//
//        private void DeserializeToXML()
//        {
//            IsDeserialising = true;
//            using (StringReader Reader = new StringReader(PermissionXML)) 
//            {
//                XmlSerializer Serializer = new XmlSerializer(typeof(ApplicationDataModel));
//                ApplicationData = (ApplicationDataModel)Serializer.Deserialize(Reader);
//                ApplicationData.AccountPermissionObject = this;
//            }
//            IsDeserialising = false;
//        }
//
//        #endregion
//
//        #region "  ApplicationDataModel  "
//
//        [Serializable]
//        public class ApplicationDataModel
//        {
//
//            #region "  Constructors  "
//
//            public ApplicationDataModel()
//            {
//
//            }
//
//            public ApplicationDataModel(AccountPermission AccountPermissionReference)
//            {
//                this.AccountPermissionObject = AccountPermissionReference;
//            }
//
//            #endregion
//
//            #region "  Properties  "
//
//            [XmlIgnore]
//            public AccountPermission AccountPermissionObject;
//
//
//            [XmlElement("Contact")]
//            public PermissionModelItem Contact;
//
//            [XmlElement("Account")]
//            public PermissionModelItem Account;
//
//
//            #endregion
//
//            public class PermissionModelItem
//            {
//                [XmlElement("View")]
//                public bool View { get; set; }
//
//                [XmlElement("Create")]
//                public bool Create { get; set; }
//
//                [XmlElement("Update")]
//                public bool Update { get; set; }
//
//                [XmlElement("Delete")]
//                public bool Delete { get; set; }
//
//            }
//
//        }
//
//
//        #endregion
//
//        #region "  Static Methods  "
//
//        public static new AccountPermission Create()
//        {
//            AccountPermission objAccountPermission = new AccountPermission();
//            objAccountPermission.SetDefaultValues();
//            return objAccountPermission;
//        }
//
//        public static new AccountPermission Load(Guid ID)
//        {
//            AccountPermission objAccountPermission = new AccountPermission();
//            objAccountPermission.DataFetch(ID);
//            return objAccountPermission;
//        }
//
//        #endregion
//
//    }
//}