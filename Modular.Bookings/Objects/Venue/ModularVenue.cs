using Modular.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Bookings
{
    [Serializable]
    public class Venue : ModularBase
    {

        #region "  Constructors  "

        public Venue()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Booking_Venue";

        #endregion

        #region "  Variables  "

        //private byte[] _Image;

        private string _Name = string.Empty;

        private string _Description = string.Empty;

        private string _AddressLine1 = string.Empty;

        private string _AddressLine2 = string.Empty;

        private string _AddressLine3 = string.Empty;

        private string _AddressCity = string.Empty;

        private string _AddressCounty = string.Empty;

        private Guid _AddressCountryID;

        private string _AddressPostcode = string.Empty;

        private Guid _ContactID;

        private decimal _VenueCost;
        
        private int _MaxAttendees;

        private bool _IsAccomodation;

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

        public string AddressLine1
        {
            get
            {
                return _AddressLine1;
            }
            set
            {
                if (_AddressLine1 != value)
                {
                    _AddressLine1 = value;
                    OnPropertyChanged("AddressLine1");
                }
            }
        }

        public string AddressLine2
        {
            get
            {
                return _AddressLine2;
            }
            set
            {
                if (_AddressLine2 != value)
                {
                    _AddressLine2 = value;
                    OnPropertyChanged("AddressLine2");
                }
            }
        }

        public string AddressLine3
        {
            get
            {
                return _AddressLine3;
            }
            set
            {
                if (_AddressLine3 != value)
                {
                    _AddressLine3 = value;
                    OnPropertyChanged("AddressLine3");
                }
            }
        }

        public string AddressCity
        {
            get
            {
                return _AddressCity;
            }
            set
            {
                if (_AddressCity != value)
                {
                    _AddressCity = value;
                    OnPropertyChanged("AddressCity");
                }
            }
        }

        public string AddressCounty
        {
            get
            {
                return _AddressCounty;
            }
            set
            {
                if (_AddressCounty != value)
                {
                    _AddressCounty = value;
                    OnPropertyChanged("AddressCounty");
                }
            }
        }

        public Guid AddressCountryID
        {
            get
            {
                return _AddressCountryID;
            }
            set
            {
                if (_AddressCountryID != value)
                {
                    _AddressCountryID = value;
                    OnPropertyChanged("AddressCountryID");
                }
            }
        }

        public string AddressPostcode
        {
            get
            {
                return _AddressPostcode;
            }
            set
            {
                if (_AddressPostcode != value)
                {
                    _AddressPostcode = value;
                    OnPropertyChanged("AddressPostcode");
                }
            }
        }

        public Guid ContactID
        {
            get
            {
                return _ContactID;
            }
            set
            {
                if (_ContactID != value)
                {
                    _ContactID = value;
                    OnPropertyChanged("ContactID");
                }
            }
        }

        public decimal VenueCost
        {
            get
            {
                return _VenueCost;
            }
            set
            {
                if (_VenueCost != value)
                {
                    _VenueCost = value;
                    OnPropertyChanged("VenueCost");
                }
            }
        }

        public int MaxAttendees
        {
            get
            {
                return _MaxAttendees;
            }
            set
            {
                if (_MaxAttendees != value)
                {
                    _MaxAttendees = value;
                    OnPropertyChanged("MaxAttendees");
                }
            }
        }

        public bool IsAccomodation
        {
            get
            {
                return _IsAccomodation;
            }
            set
            {
                if (_IsAccomodation != value)
                {
                    _IsAccomodation = value;
                    OnPropertyChanged("IsAccomodation");
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
