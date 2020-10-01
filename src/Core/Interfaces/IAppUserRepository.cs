using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAppUserRepository
    {
        Task<IReadOnlyList<AppUser>> GetAppUsersAsync();
        Task<AppUser> GetAppUserByIdAsync(int userId);
        Task<AppUser> AddAppUserAsync(AppUser user);
        Task<AppUser> UpdateAppUserAsync(AppUser user);
        Task<AppUser> DeleteAppUserByIdAsync(int userId);
        bool AppUserByIdExists(int userId);
    }
}
