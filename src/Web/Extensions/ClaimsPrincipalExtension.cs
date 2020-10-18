using System.Linq;
using System.Security.Claims;

namespace Web.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static string RetrieveEmailFromClaimsPrincipal(this ClaimsPrincipal user)
        {
            return user?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
        }
    }
}
