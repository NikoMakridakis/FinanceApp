using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class BudgetConfiguration : IEntityTypeConfiguration<Budget>
    {
        public void Configure(EntityTypeBuilder<Budget> builder)
        {
            builder.Property(b => b.BudgetId).IsRequired();
            builder.Property(b => b.TodaysDate).IsRequired().HasColumnType("date");
            builder.Property(b => b.DaysLeftInMonth).IsRequired();
            builder.Property(b => b.MonthlyIncome).HasColumnType("decimal(18,2)");
            builder.Property(b => b.MonthlySpending).HasColumnType("decimal(18,2)");
        }
    }
}
