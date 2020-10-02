using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data
{
    public class FinanceAppDbContext : IdentityDbContext<User>
    {
        public FinanceAppDbContext(DbContextOptions<FinanceAppDbContext> options) : base(options)
        {
        }

        public DbSet<BudgetGroup> BudgetGroups { get; set; }
        public DbSet<Item> Items { get; set; }

        //When creating the database migration, the specified configuration settings will be applied from Data/Config.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BudgetGroup>().HasOne(b => b.User).WithMany(u => u.BudgetGroups);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
