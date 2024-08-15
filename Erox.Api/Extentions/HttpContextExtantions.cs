using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace Erox.Api.Extentions
{
    public static class HttpContextExtantions
    {
        public static Guid GetUserProfileIdClaimValue(this HttpContext context)
        {
           return GetGuidCliamValue("UserProfileId",context);
           
        }

        public static Guid GetIdentityIdClaimValue(this HttpContext context)
        {
            return GetGuidCliamValue("Identityid", context);
        }

        private static Guid GetGuidCliamValue(string key,HttpContext context)
        {
            var identity = context.User.Identity as ClaimsIdentity;
            return Guid.Parse(identity?.FindFirst(key)?.Value);
        }
    }
}
