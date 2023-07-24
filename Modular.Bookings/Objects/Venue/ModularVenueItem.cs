using Modular.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Bookings
{
    [Serializable]
    public class VenueItem : ModularBase
    {

        #region "  Constructors  "

        public VenueItem()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Booking_VenueItem";

        #endregion

        #region "  Variables  "

        //private byte[] _Image;

        private string _Name = string.Empty;

        private string _Description = string.Empty;

        private decimal _Cost;

        private decimal _CostVAT;

        #endregion

        #region "  Properties  "

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
        
        public decimal Cost
        {
            get
            {
                return _Cost;
            }
            set
            {
                if (_Cost != value)
                {
                    _Cost = value;
                    OnPropertyChanged("Cost");
                }
            }
        }

        public decimal CostVAT
        {
            get
            {
                return _CostVAT;
            }
            set
            {
                if (_CostVAT != value)
                {
                    _CostVAT = value;
                    OnPropertyChanged("CostVAT");
                }
            }
        }

        #endregion

        #region "  Static Methods  "

        public static new Venue Create()
        {
            Venue obj = new Venue();
            obj.SetDefaultValues();
            return obj;
        }

        public static new Venue Load(Guid ID)
        {
            Venue obj = new Venue();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Name;
        }

        #endregion

    }
}
