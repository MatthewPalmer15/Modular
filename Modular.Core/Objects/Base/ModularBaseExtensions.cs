using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Core
{
    public static class ModularBaseExtensions
    {
        public static List<ModularBase> Where(Func<ModularBase, bool> predicate)
        {

            // return .Where(predicate);
            return new List<ModularBase>();
        }

    }
}
