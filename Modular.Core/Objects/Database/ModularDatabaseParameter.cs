using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Core.Databases
{
    public struct DatabaseParameter
    {

        public DatabaseParameter()
        {
            this.Name = "";
            this.Value = "";
        }

        public DatabaseParameter(string Name, string Value)
        {
            this.Name = Name.Trim().StartsWith('@') ? Name.Trim().Replace("@", "") : Name;
            this.Value = Value.Trim();
        }

        public string Name { get; set; }

        public string Value { get; set; }

    }
}
