using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.LocalNotification;

namespace Modular.Core.Notification
{
    public class Notification : ModularBase
    {

        #region "  Constructors  "

        private Notification()
        {
        }

        #endregion

        #region "  Variables  "

        private Guid _ContactID;

        private string _Title;

        private string _Subtitle;

        private string _Message;

        private bool _IsSent;

        private DateTime _SentDate;


        #endregion

        #region "  Properties  "

        public Entity.Contact Contact
        {
            get
            {
                return Entity.Contact.Load(_ContactID);
            }
            set
            {
                if (_ContactID != value.ID)
                {
                    _ContactID = value.ID;
                    OnPropertyChanged("Contact");
                }
            }
        }

        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                if (_Title != value)
                {
                    _Title = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        public string Subtitle
        {
            get
            {
                return _Subtitle;
            }
            set
            {
                if (_Subtitle != value)
                {
                    _Subtitle = value;
                    OnPropertyChanged("Subtitle");
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

        public bool IsSent
        {
            get
            {
                return _IsSent;
            }
            set
            {
                if (_IsSent != value)
                {
                    _IsSent = value;
                    OnPropertyChanged("IsSent");
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

        #region "  Static Methods  "

        /// <summary>
        /// Creates a new instance with default values
        /// </summary>
        /// <returns>A new instance</returns>
        public static void Create(Entity.Contact Contact, string Title, string Subtitle, string Message)
        {
            Notification obj = new Notification();
            obj.SetDefaultValues(); // Prevent any null values.
            obj.Contact = Contact;
            obj.Title = Title;
            obj.Subtitle = Subtitle;
            obj.Message = Message;
            obj.Save();
        }


        #endregion

        #region "  Instance Methods  "

        public void Send()
        {
            var Notification = new NotificationRequest();
            Notification.Title = Title;
            Notification.Subtitle = Subtitle;
            Notification.Description = Message;
            Notification.Schedule.NotifyTime = DateTime.Now.AddSeconds(1);

            LocalNotificationCenter.Current.Show(Notification);

            this.IsSent = true;
            this.SentDate = DateTime.Now;
            this.Save();
        }

        public override string ToString()
        {
            return Title;
        }


        #endregion

    }
}
