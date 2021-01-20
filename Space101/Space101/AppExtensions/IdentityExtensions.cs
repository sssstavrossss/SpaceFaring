using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace Space101.AppExtensions
{
    public static class IdentityExtensions
    {
        public static string GetNickName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("NickName");

            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}