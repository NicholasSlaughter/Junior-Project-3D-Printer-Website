using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JPWeb.UI.Utilities
{
    public static class AuthorizeHtmlHelper
    {
        public static bool UserOrHigher(ClaimsIdentity user)
        {
            return user.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value.Equals("User"))
                || user.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value.Equals("Admin"))
                || user.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value.Equals("SuperAdmin"));
        }

        public static bool UserIsAdminOrHigher(ClaimsIdentity user)
        {
            return user.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value.Equals("Admin"))
                   || user.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value.Equals("SuperAdmin"));
        }

        public static bool UserIsSuperAdmin(ClaimsIdentity user)
        {
            return user.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value.Equals("SuperAdmin"));
        }
    }
}
