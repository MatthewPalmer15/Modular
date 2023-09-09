using Modular.Core;
using Modular.Core.Security;
using Modular.Core.System;
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

        private string _Description = string.Empty;

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

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public string Description
        {
            get
            {
                return _Description;
            }
            set
            { 
                if (_Description != value)
                {
                    _Description = value;
                    OnPropertyChanged("Description");
                } 
            }
        }

        public bool IsPublic
        {
            get
            {
                return _IsPublic;
            }
            set
            {
                if (_IsPublic != value)
                {
                    _IsPublic = value;
                    OnPropertyChanged("IsPublic");
                }
            }
        }

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
            set
            {
                if (_Port != value)
                {
                    _Port = value;
                    OnPropertyChanged("Port");
                }
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

        public Guid RoleID
        {
            get
            {
                return _RoleID;
            }
            set
            {
                if (_RoleID != value)
                {
                    _RoleID = value;
                    OnPropertyChanged("RoleID");
                }
            }
        }

        public DateTime LastConnected
        {
            get
            {
                return _LastConnected;
            }
            set
            {
                if (_LastConnected != value)
                {
                    _LastConnected = value;
                    OnPropertyChanged("LastConnected");
                }
            }
        }

        public Guid LastConnectedBy
        {
            get
            {
                return _LastConnectedBy;
            }
            set
            {
                if (_LastConnectedBy != value)
                {
                    _LastConnectedBy = value;
                    OnPropertyChanged("LastConnectedBy");
                }
            }
        }

        #endregion


        public void Initalise()
        {
            if ((ContactID != Guid.Empty && ContactID == SystemCore.Context.Identity.ContactID) || (RoleID != Guid.Empty && SystemCore.Context.Identity.IsInRole(Role.Load(RoleID).Name)) && !IsPublic)
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
}
