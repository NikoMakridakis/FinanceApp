using Core.Entities;
using System;

namespace Web.Extensions
{
    public static class GetLockoutMinutesExtension
    {
        public static int GetLockoutSecondsRemaining(this User user)
        {
            DateTimeOffset lockoutEndTime = (DateTimeOffset)user.LockoutEnd;
            DateTimeOffset currentTime = DateTimeOffset.Now;
            TimeSpan lockoutTimeRemaining = lockoutEndTime - currentTime;
            int lockoutSecondsRemaining = (int)lockoutTimeRemaining.TotalSeconds;

            return lockoutSecondsRemaining;
        }
    }
}
