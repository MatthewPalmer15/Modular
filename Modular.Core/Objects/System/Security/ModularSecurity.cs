using Modular.Core.System;
using System.Linq;

namespace Modular.Core.Security
{
    public static partial class Security
    {

        /// <summary>
        /// Checks to see if the current user has the specified security permission. Returns true if the user has the permission, false if not.
        /// </summary>
        /// <param name="SecurityPermission"></param>
        /// <returns></returns>
        public static bool CheckSecurity(Enum SecurityPermission)
        {
            return ModularSystem.Context.Identity.Role.Permissions.First(RolePermission => RolePermission.Permission == SecurityPermission) != null;
        }

        public static bool CheckSecurity(string RoleName)
        {
            return ModularSystem.Context.Identity.IsInRole(RoleName);
        }

    }
}