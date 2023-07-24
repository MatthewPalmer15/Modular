using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modular.Core;

namespace Modular.Bookings
{
    [Serializable]
    public class Note : ModularBase
    {

        #region "  Constructors  "

        public Note()
        {
        }

        #endregion

        #region "  Enums  "

        public enum NoteType
        {
            Internal = 0,
            External = 1
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Booking_InternalNote";

        #endregion

        #region "  Variables  "

        private Guid _BookingID;

        private NoteType _NoteType;

        private string _Message = string.Empty;

        #endregion

        #region "  Properties  "

        public Guid BookingID
        {
            get
            {
                return _BookingID;
            }
            set
            {
                if (_BookingID != value)
                {
                    _BookingID = value;
                    OnPropertyChanged("BookingID");
                }
            }
        }

        public NoteType Type
        {
            get
            {
                return _NoteType;
            }
            set
            {
                if (_NoteType != value)
                {
                    _NoteType = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        public string Message
        {
            get
            {
                return _Message;
            }
            set
            {
                if (_Message != value)
                {
                    _Message = value;
                    OnPropertyChanged("Message");
                }
            }
        }

        #endregion

        #region "  Static Methods  "

        public static new Note Create()
        {
            Note obj = new Note();
            obj.SetDefaultValues();
            return obj;
        }

        public static new Note Load(Guid ID)
        {
            Note obj = new Note();
            obj.Fetch(ID);
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return $"{Enum.GetName(typeof(NoteType), Type)} - {Message}";
        }

        #endregion

    }
}
