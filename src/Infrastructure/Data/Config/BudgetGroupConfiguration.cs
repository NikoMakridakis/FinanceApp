using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class BudgetGroupConfiguration : IEntityTypeConfiguration<BudgetGroup>
    {
        public void Configure(EntityTypeBuilder<BudgetGroup> builder)
        {
            builder.Property(g => g.GroupId).IsRequired();
            builder.Property(g => g.BudgetId).IsRequired();
            builder.Property(g => g.GroupTitle).HasMaxLength(50);
        }
    }
}