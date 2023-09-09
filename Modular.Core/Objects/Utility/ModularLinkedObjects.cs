using Modular.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Core.Objects.Utility
{
    public class LinkedObjects : ModularBase
    {

        #region "  Constructors  "

        private LinkedObjects()
        {
        }

        #endregion

        #region "  Constants  "

        protected static readonly new string MODULAR_DATABASE_TABLE = "tbl_Modular_LinkedObjects";
        protected static readonly new string MODULAR_DATABASE_STOREDPROCEDURE_PREFIX = "usp_Modular_LinkedObjects";

        #endregion

        #region "  Variables  "

        private Guid _ObjectID;

        private ObjectTypes.ObjectType _ObjectType;

        private Guid _RelatedObjectID;

        private ObjectTypes.ObjectType _RelatedObjectType;

        #endregion

        #region "  Properties  "

        public Guid ObjectID
        {
            get
            {
                return _ObjectID;
            }
            set
            {
                _ObjectID = value;
                OnPropertyChanged("ObjectID");
            }
        }

        public ObjectTypes.ObjectType ObjectType
        {
            get
            {
                return _ObjectType;
            }
            set
            {
                _ObjectType = value;
                OnPropertyChanged("ObjectType");
            }
        }

        public Guid RelatedObjectID
        {
            get
            {
                return _RelatedObjectID;
            }
            set
            {
                _RelatedObjectID = value;
                OnPropertyChanged("RelatedObjectID");
            }
        }

        public ObjectTypes.ObjectType RelatedObjectType
        {
            get
            {
                return _RelatedObjectType;
            }
            set
            {
                _RelatedObjectType = value;
                OnPropertyChanged("RelatedObjectType");
            }
        }

        #endregion

        #region "  Static Methods  "

        public static void LinkObjects(Guid ObjectID, ObjectTypes.ObjectType ObjectType, Guid RelatedObjectID, ObjectTypes.ObjectType RelatedObjectType)
        {
            LinkedObjects obj = new LinkedObjects
            {
                ObjectID = ObjectID,
                ObjectType = ObjectType,
                RelatedObjectID = RelatedObjectID,
                RelatedObjectType = RelatedObjectType
            };

            obj.Save();
        }

        #endregion

    }
}
