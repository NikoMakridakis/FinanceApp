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
            builder.Property(i => i.GroupId).IsRequired();
            builder.Property(i => i.Label).HasMaxLength(40);
            builder.Property(i => i.Amount).HasColumnType("decimal(18,2)");
            builder.Property(i => i.Date).HasColumnType("date");
            builder.Property(i => i.Notes).HasMaxLength(500);
        }
    }
}