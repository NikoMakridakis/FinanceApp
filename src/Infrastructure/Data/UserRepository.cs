using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly FinanceAppDbContext _context;
        public UserRepository(FinanceAppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IReadOnlyList<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByUserIdAsync(Guid userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<User> AddUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> DeleteUserByUserIdAsync(Guid userId)
        {
            User user = await _context.Users.FindAsync(userId);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
