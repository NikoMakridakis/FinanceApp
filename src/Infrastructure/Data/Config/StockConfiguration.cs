using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.Property(s => s.Id).IsRequired();
            builder.Property(s => s.Symbol).IsRequired().HasMaxLength(10);
            builder.Property(s => s.Exchange).HasMaxLength(50);
            builder.Property(s => s.Name).HasMaxLength(100);
            builder.Property(s => s.LatestPrice).HasColumnType("decimal(18,4)");
        }
    }
}
