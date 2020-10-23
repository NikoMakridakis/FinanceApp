using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Extensions
{
    public static class UserManagerExtenstions
    {
        public static async Task<User> FindByEmailFromClaimsPrinciple(this UserManager<User> input, ClaimsPrincipal user)
        {
            string email = user?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;

            return await input.Users.SingleOrDefaultAsync(user => user.Email == email);
        }
    }
}
