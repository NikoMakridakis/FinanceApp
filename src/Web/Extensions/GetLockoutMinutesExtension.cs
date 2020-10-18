using Core.Entities;
using System;

namespace Web.Extensions
{
    public static class GetLockoutMinutesExtension
    {
        public static int GetLockoutMinutesRemaining(this User user)
        {
            DateTimeOffset lockoutEndTime = (DateTimeOffset)user.LockoutEnd;
            DateTimeOffset currentTime = DateTimeOffset.Now;
            TimeSpan lockoutTimeRemaining = lockoutEndTime - currentTime;
            int lockoutMinutesRemaining = (int)lockoutTimeRemaining.TotalMinutes;

            return lockoutMinutesRemaining;
        }
    }
}
