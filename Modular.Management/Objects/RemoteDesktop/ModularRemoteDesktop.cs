using Modular.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Management.RemoteDesktop
{
    public class RemoteDesktopConnection : ModularBase
    {

        #region "  Constructors  "

        public RemoteDesktopConnection() 
        { 
        }

        #endregion

        #region "  Constants  "

        private string _Name = string.Empty;

        private string Description = string.Empty;

        private bool _IsPublic;

        private string _IPAddress = string.Empty;

        private string _Port = string.Empty;

        private string _Username = string.Empty;

        private string _Password = string.Empty;

        private Guid _ContactID; // LIMIT TO ONE USER

        private Guid _RoleID;   // LIMIT TO ONLY ROLE


        private DateTime _LastConnected;

        private Guid _LastConnectedBy;

        #endregion

        #region "  Properties  "

        public string IPAddress
        {
            get
            {
                return _IPAddress;
            }
            set
            {
                _IPAddress = value;
                OnPropertyChanged("IPAddress");
            }
        }

        public string Port
        {
            get
            {
                return _Port;
            }
        }

        protected string Username
        {
            get
            {
                return _Username;
            }
            set
            {
                if (_Username != value)
                {
                    _Username = value;
                    OnPropertyChanged("Username");
                }
            }
        }

        protected string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                if (_Password != value)
                {
                    _Password = value;
                    OnPropertyChanged("Password");
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

        #endregion


        public void Initalise()
        {
            // The following parameters are for the Remote Desktop client (mstsc.exe)
            string rdcPath = Environment.ExpandEnvironmentVariables(@"%SystemRoot%\system32\mstsc.exe");
            string rdcArguments = $"/v:{IPAddress}"; // Specifies the server address to connect to

            // Start the Remote Desktop client process
            Process rdcProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = rdcPath,
                    Arguments = rdcArguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };

            try
            {
                rdcProcess.Start();
                rdcProcess.WaitForExit();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error starting Remote Desktop client: {ex.Message}");
            }
        }

    }
}
