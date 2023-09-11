using Modular.Core.Audit;
using Modular.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Core.Notification
{
    public static class NotificationEngine
    {

        #region "  Variables  "

        private static bool _IsRunning = false;

        private static List<Notification> _Notifications = new List<Notification>();

        private static DateTime _LastRetrievedNotifications = DateTime.MinValue;

        #endregion

        #region "  Properties  "

        public static bool IsRunning
        {
            get
            {
                return _IsRunning;
            }
        }

        public static List<Notification> Notifications
        {
            get
            {
                if (_LastRetrievedNotifications.AddMinutes(1) < DateTime.Now)
                {
                    //_Notifications = Notification.LoadList();
                    _LastRetrievedNotifications = DateTime.Now;
                }

                return _Notifications;
            }
        }

        #endregion

        #region "  Public Methods  "

        public static void Start()
        {
            _IsRunning = true;

            Task.Factory.StartNew(() =>
            {
                while (_IsRunning)
                {

                    foreach (Notification Notification in _Notifications)
                    {
                        try
                        {
                            Notification.Send();
                            AuditLog.Create(ObjectTypes.ObjectType.Notification, Guid.Empty, $"Notification: Sent notification to {Notification.To.Username}. Message: {Notification.Message}.");

                        }
                        catch (Exception Exception)
                        {
                            AuditLog.Create(ObjectTypes.ObjectType.Notification, Guid.Empty, $"Notification: Failed to send notification to {Notification.To.Username}. Message: {Notification.Message}. Exception: {Exception.ToString()}");
                        }
                    }
                }
            });

        }

        public static void Stop()
        {
            _IsRunning = false;
        }

        #endregion

    }
}
