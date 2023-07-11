using System.Threading;

namespace Modular.Core
{
    public class NotificationEngine
    {

        #region "  Constructors  "

        public NotificationEngine()
        {
            _IsRunning = false;
            _CancellationTokenSource = new CancellationTokenSource();
        }

        #endregion

        #region "  Variables  "

        private bool _IsRunning = false;

        private CancellationTokenSource _CancellationTokenSource;

        #endregion

        #region "  Public Methods  "

        public async Task Start()
        {
            if (!_IsRunning)
            {
                _IsRunning = true;
                _CancellationTokenSource = new CancellationTokenSource();

                await Task.Run(async () =>
                {
                    try
                    {
                        while (!_CancellationTokenSource.Token.IsCancellationRequested)
                        {
                            GetPendingNotifications();

                            // Delay for a certain interval before checking again
                            await Task.Delay(TimeSpan.FromSeconds(5), _CancellationTokenSource.Token);
                        }
                    }
                    catch (TaskCanceledException)
                    {
                        // Ignore the exception
                    }
                    catch (Exception Exception)
                    {
                        // Create a log entry for the exception
                    }
                    finally
                    {
                        _IsRunning = false;
                    }
                }, _CancellationTokenSource.Token);

            }

        }

        public void Stop()
        {
            if (_IsRunning)
            {
                _CancellationTokenSource.Cancel();
                _IsRunning = false;
            }
        }

        #endregion

        #region "  Private Methods  "

        private void DisplayNotification(Notification Notification)
        {
            
            // TODO: Display the notification using .NET MAUI

        }

        private void GetPendingNotifications()
        {
            List<Notification> NotificationsToSend = Notification.LoadAll().Where(Notification => Notification.Status.Equals(Notification.NotificationStatusType.Pending) && Notification.ContactID.Equals(ModularSystem.Context.Identity.ContactID)).ToList();

            foreach (Notification Notification in NotificationsToSend)
            {
                DisplayNotification(Notification);
            }

            // Clear the notifications list
            NotificationsToSend.Clear();
        }

        #endregion

    }
}
