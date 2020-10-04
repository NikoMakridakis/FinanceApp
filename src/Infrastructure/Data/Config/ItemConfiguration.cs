using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.Property(i => i.ItemId).IsRequired();
            builder.Property(i => i.ItemTitle).HasMaxLength(50);
            builder.Property(i => i.ItemMontlyAmount).HasColumnType("decimal(18,2)");
            builder.Property(i => i.BudgetGroupId).IsRequired();
        }
    }
}