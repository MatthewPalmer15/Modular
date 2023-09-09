using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Core.Interfaces
{
    public interface IModularPage
    {

        #region "  Properties  "

        public string Name { get; set; }

        #endregion

        #region "  Methods  "

        public void OnSave()
        {
            // Method intentionally left empty.
        }

        public void OnLoad()
        {
            // Method intentionally left empty.
        }

        public void OnDelete()
        {
            // Method intentionally left empty.
        }

        public void OnChange() 
        {

            // Method intentionally left empty.
        }

        public void OnCancel()
        {

            // Method intentionally left empty.
        }

        #endregion

    }
}
