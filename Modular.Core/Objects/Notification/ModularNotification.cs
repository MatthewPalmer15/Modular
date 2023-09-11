using Modular.Core.Entity;
using Modular.Core.Mail;
using Modular.Core.SMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Core.Notification
{
    public class Notification : ModularBase
    {

        #region "  Constructors  "

        public Notification()
        {
        }

        #endregion

        #region "  Constants  "

        protected static readonly new string MODULAR_DATABASE_TABLE = "tbl_Modular_Notification";
        protected static new readonly string MODULAR_DATABASE_STOREDPROCEDURE_PREFIX = "usp_Modular_Notification";
        protected static new readonly Type MODULAR_OBJECTTYPE = typeof(Notification);

        #endregion

        #region "  Enums  "

        public enum SendNotificationType
        {
            Email = 1,
            SMS = 2
        }

        #endregion

        #region "  Variables  "

        private Guid _FromAccountID;

        private Guid _ToAccountID;

        private SendNotificationType _NotificationType;

        private string _Message = string.Empty;

        private bool _Important;

        #endregion

        #region "  Properties  "

        public Account From
        {
            get
            {
                return Account.Load(_FromAccountID);
            }
        }

        public Account To
        {
            get
            {
                return Account.Load(_ToAccountID);
            }
        }

        public SendNotificationType NotificationType
        {
            get
            {
                return _NotificationType;
            }
        }

        public string Message
        {
            get
            {
                return _Message;
            }
        }

        public bool Important
        {
            get
            {
                return _Important;
            }
        }

        #endregion

        #region "  Public Methods  "

        public void Send()
        {
            switch (NotificationType)
            {
                case SendNotificationType.Email:
                    //EmailUtils.SendEmail();
                    break;
                case SendNotificationType.SMS:
                    SmsUtils.SendSMS();
                    break;
            }


        }

        #endregion


    }
}
