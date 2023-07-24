using Modular.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Schooling.Accreditation
{
    [Serializable]
    public class AccreditationLevelItem : ModularBase
    {

        #region "  Constructors  "

        public AccreditationLevelItem()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_AccreditationLevelItem";

        #endregion

        #region "  Variables  "

        private Guid _AccreditationLevelID;

        private string _Name = string.Empty;

        private string _Description = string.Empty;

        private DateTime _AvaliableFrom;

        private DateTime _AvaliableTo;

        private bool _IsAvailableOnline;

        private bool _IsAvailableInBackOffice;

        private decimal _PriceExcVAT;

        private decimal _PriceVAT;

        #endregion

        #region "  Properties  "

        public Guid AccreditationLevelID
        {
            get
            {
                return _AccreditationLevelID;
            }
            set
            {
                if (_AccreditationLevelID != value)
                {
                    _AccreditationLevelID = value;
                    OnPropertyChanged("AccreditationLevelID");
                }
            }
        }

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        public DateTime AvaliableFrom
        {
            get
            {
                return _AvaliableFrom;
            }
            set
            {
                if (_AvaliableFrom != value)
                {
                    _AvaliableFrom = value;
                    OnPropertyChanged("AvaliableFrom");
                }
            }
        }

        public DateTime AvaliableTo
        {
            get
            {
                return _AvaliableTo;
            }
            set
            {
                if (_AvaliableTo != value)
                {
                    _AvaliableTo = value;
                    OnPropertyChanged("AvaliableTo");
                }
            }
        }

        public bool IsAvailableOnline
        {
            get
            {
                return _IsAvailableOnline;
            }
            set
            {
                if (_IsAvailableOnline != value)
                {
                    _IsAvailableOnline = value;
                    OnPropertyChanged("IsAvailableOnline");
                }
            }
        }

        public bool IsAvailableInBackOffice
        {
            get
            {
                return _IsAvailableInBackOffice;
            }
            set
            {
                if (_IsAvailableInBackOffice != value)
                {
                    _IsAvailableInBackOffice = value;
                    OnPropertyChanged("IsAvailableInBackOffice");
                }
            }
        }

        public decimal PriceExcVAT
        {
            get
            {
                return _PriceExcVAT;
            }
            set
            {
                if (_PriceExcVAT != value)
                {
                    _PriceExcVAT = value;
                    OnPropertyChanged("PriceExcVAT");
                }
            }
        }

        public decimal PriceVAT
        {
            get
            {
                return _PriceVAT;
            }
            set
            {
                if (_PriceVAT != value)
                {
                    _PriceVAT = value;
                    OnPropertyChanged("PriceVAT");
                }
            }
        }

        public decimal PriceIncVAT
        {
            get
            {
                return PriceExcVAT + PriceVAT;
            }
        }

        #endregion

        #region "  Static Methods  "

        /// <summary>
        /// Creates a new instance with default values
        /// </summary>
        /// <returns>A new instance</returns>
        public static new AccreditationLevelItem Create()
        {
            AccreditationLevelItem obj = new AccreditationLevelItem();
            obj.SetDefaultValues();
            return obj;
        }

        public static new AccreditationLevelItem Load(Guid ID)
        {
            AccreditationLevelItem obj = new AccreditationLevelItem();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Name;
        }

        public override AccreditationLevelItem Clone()
        {
            return AccreditationLevelItem.Load(ID);
        }

        #endregion


    }
}
