using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Web.Data.Config
{
    public class BudgetGroupConfiguration : IEntityTypeConfiguration<BudgetGroup>
    {
        public void Configure(EntityTypeBuilder<BudgetGroup> builder)
        {
            builder.Property(b => b.BudgetGroupId).IsRequired();
            builder.Property(b => b.BudgetGroupTitle).HasMaxLength(50);
            builder.Property(b => b.UserId).IsRequired();
        }
    }
}