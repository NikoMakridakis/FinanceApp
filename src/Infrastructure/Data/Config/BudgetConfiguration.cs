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
            //builder.Property(s => s.Symbol).IsRequired().HasMaxLength(10);
            //builder.Property(s => s.Exchange).HasMaxLength(50);
            //builder.Property(s => s.Name).HasMaxLength(100);
            //builder.Property(s => s.LatestPrice).HasColumnType("decimal(18,4)");
        }
    }
}
