using System;
using System.Threading.Tasks;

namespace Modular.Core.ScheduledTasks
{
    public static class ScheduledTaskEngine
    {

        #region "  Variables  "

        private static bool _IsRunning = false;

        private static List<ScheduledTask> _ScheduledTasks = new List<ScheduledTask>();

        #endregion

        #region "  Properties  "

        public static bool IsRunning
        {
            get
            {
                return _IsRunning;
            }
        }

        public static List<ScheduledTask> ScheduledTasks
        {
            get
            {
                return _ScheduledTasks;
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
                    foreach (ScheduledTask Task in _ScheduledTasks)
                    {
                        if (Task.Enabled && Task.NextRunTime <= DateTime.Now)
                        {
                            Task.Execute();
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
