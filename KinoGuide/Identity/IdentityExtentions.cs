using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace KinoGuide.Identity
{
    public static class IdentityExtentions
    {
        public static int GetUserId(this IIdentity identity)
        {
            if (identity is ClaimsIdentity claimIdentity) {
                return int.Parse(claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
            }
            return 0;
        }
    }
}