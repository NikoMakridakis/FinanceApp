using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(b => b.BudgetId).IsRequired();
            builder.Property(b => b.MonthlyIncome).HasColumnType("decimal(18,2)");
            builder.Property(b => b.MonthlySpending).HasColumnType("decimal(18,2)");
        }
    }
}
