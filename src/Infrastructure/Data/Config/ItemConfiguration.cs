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
            builder.Property(i => i.Label).HasMaxLength(150);
            builder.Property(i => i.Amount).HasColumnType("decimal(18,4)");
            builder.Property(i => i.Notes).HasMaxLength(500);
        }
    }
}