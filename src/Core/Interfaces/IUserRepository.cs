using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUserRepository
    {
        Task<IReadOnlyList<User>> GetUsersAsync();
        Task<User> GetUserByUserIdAsync(int userId);
        Task<User> AddUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<User> DeleteUserByUserIdAsync(int userId);
        bool UserByUserIdExists(int userId);
    }
}
