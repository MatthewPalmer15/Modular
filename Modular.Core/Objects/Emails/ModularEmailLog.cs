using System.Net.Mail;

namespace Modular.Core
{
    public class EmailLog : ModularBase
    {

        #region "  Constructors  "

        public EmailLog()
        {
        }

        #endregion

        #region "  Constants  "

        protected static readonly new string MODULAR_DATABASE_TABLE = "tbl_Modular_EmailLog";

        #endregion

        #region "  Variables  "

        private MailMessage _Email = new MailMessage();

        private DateTime _SentDate;

        #endregion

        #region "  Properties  "

        public MailMessage Email
        {
            get
            {
                return _Email;
            }
            set
            {
                if (_Email != value)
                {
                    _Email = value;
                    OnPropertyChanged("Email");
                }
            }
        }

        public DateTime SentDate
        {
            get
            {
                return _SentDate;
            }
            set
            {
                if (_SentDate != value)
                {
                    _SentDate = value;
                    OnPropertyChanged("SentDate");
                }
            }
        }

        #endregion
    }
}
