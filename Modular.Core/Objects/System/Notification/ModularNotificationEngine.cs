namespace Modular.Core
{
    public class NotificationEngine
    {

        private List<Notification> _NotificationsToSend;
        private Timer _Timer;

        public NotificationEngine()
        {
            _NotificationsToSend = new List<Notification>();
        }

        private void DisplayNotification(Notification Notification)
        {
            // TODO: Display the notification using .NET MAUI

        }

        public async Task Start(CancellationToken CancellationToken)
        {
            await Task.Run(async () =>
            {
                while (!CancellationToken.IsCancellationRequested)
                {
                    GetPendingNotifications();

                    // Delay for a certain interval before checking again
                    await Task.Delay(TimeSpan.FromSeconds(5));
                }
            }, CancellationToken);
        }

        private void GetPendingNotifications()
        {
            _NotificationsToSend = Notification.LoadAll().Where(Notification => Notification.Status.Equals(Notification.NotificationStatusType.Pending) && Notification.ContactID.Equals(ModularSystem.Context.Identity.ContactID)).ToList();

            foreach (Notification Notification in _NotificationsToSend)
            {
                DisplayNotification(Notification);
            }

            // Clear the notifications list
            _NotificationsToSend.Clear();
        }

    }
}
