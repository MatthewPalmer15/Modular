using System.Net;
using System.Net.Mail;

namespace Modular.Core
{
    public class Email
    {

        #region "  Constructors  "

        public Email()
        {
        }

        #endregion

        #region "  Variables  "

        private string _FromAddress = string.Empty;

        private string _ToAddress = string.Empty;

        private List<string> _CCAddresses = new List<string>();

        private List<string> _BCCAddresses = new List<string>();

        private string _Subject = string.Empty;

        private string _Body = string.Empty;

        private List<Attachment> _Attachments = new List<Attachment>();

        private bool _IsHTMLBody = false;

        #endregion

        #region "  Properties  "

        public string FromAddress
        {
            get
            {
                return _FromAddress;
            }
            set
            {
                if (_FromAddress != value)
                {
                    _FromAddress = value;
                }
            }
        }

        public string ToAddress
        {
            get
            {
                return _ToAddress;
            }
            set
            {
                if (_ToAddress != value)
                {
                    _ToAddress = value;
                }
            }
        }

        public List<string> CCAddresses
        {
            get
            {
                return _CCAddresses;
            }
            set
            {
                if (_CCAddresses != value)
                {
                    _CCAddresses = value;
                }
            }
        }

        public List<string> BCCAddresses
        {
            get
            {
                return _BCCAddresses;
            }
            set
            {
                if (_BCCAddresses != value)
                {
                    _BCCAddresses = value;
                }
            }
        }

        public string Subject
        {
            get
            {
                return _Subject;
            }
            set
            {
                if (_Subject != value)
                {
                    _Subject = value;
                }
            }
        }

        public string Body
        {
            get
            {
                return _Body;
            }
            set
            {
                if (_Body != value)
                {
                    _Body = value;
                }
            }
        }

        public List<Attachment> Attachments
        {
            get
            {
                return _Attachments;
            }
            set
            {
                if (_Attachments != value)
                {
                    _Attachments = value;
                }
            }
        }

        public bool IsHTMLBody
        {
            get
            {
                return _IsHTMLBody;
            }
            set
            {
                if (_IsHTMLBody != value)
                {
                    _IsHTMLBody = value;
                }
            }
        }

        #endregion

        #region "  Methods  "

        public void SendEmail(MailMessage Email)
        {
            try
            {
                SmtpClient SMTPClient = new SmtpClient()
                {
                    Host = MailClient.Host,
                    Port = MailClient.Port,
                    Credentials = new NetworkCredential(MailClient.Credentials.Username, MailClient.Credentials.Password),
                    DeliveryMethod = MailClient.DeliveryMethod,
                    EnableSsl = MailClient.EnableSSL
                };

                SMTPClient.Send(Email);


            }
            catch
            {
                throw new ModularException(ExceptionType.SMTPClientEmailSendError, "There was an issue sending an email.");
            }

        }

        #endregion

    }
}
