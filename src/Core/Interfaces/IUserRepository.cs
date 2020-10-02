using Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUserRepository
    {
        Task<IReadOnlyList<User>> GetUsersAsync();
        Task<User> GetUserByUserIdAsync(Guid userId);
        Task<User> AddUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<User> DeleteUserByUserIdAsync(Guid userId);
    }
}
