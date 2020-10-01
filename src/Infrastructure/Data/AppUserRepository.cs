using Core.Entities;
using Core.Interfaces;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly AppIdentityDbContext _context;
        public AppUserRepository(AppIdentityDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IReadOnlyList<AppUser>> GetAppUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<AppUser> GetAppUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<AppUser> AddAppUserAsync(AppUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<AppUser> UpdateAppUserAsync(AppUser user)
        {
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<AppUser> DeleteAppUserByIdAsync(int userId)
        {
            AppUser user = await _context.Users.FindAsync(userId);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public bool AppUserByIdExists(int userId)
        {
            return _context.Users.Any(u => u.AppUserId == userId);
        }
    }
}
