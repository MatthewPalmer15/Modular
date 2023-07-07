namespace Modular.Core
{

    public struct MenuItem
    {
        #region "  Constuctors  "

        public MenuItem(string Name, byte[] Icon, string Command)
        {
            this.Name = Name;
            this.Icon = Icon;
            this.Command = Command;
        }

        #endregion

        #region "  Properties  "

        public string Name { get; set; }

        public byte[] Icon { get; set; }

        public string Command { get; set; }

        #endregion

    }

}
