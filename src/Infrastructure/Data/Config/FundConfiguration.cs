using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class FundConfiguration : IEntityTypeConfiguration<Fund>
    {
        public void Configure(EntityTypeBuilder<Fund> builder)
        {
            builder.Property(f => f.FundId).IsRequired();
            builder.Property(f => f.Label).HasMaxLength(150);
            builder.Property(f => f.GoalAmount).HasColumnType("decimal(18,4)");
            builder.Property(f => f.CurrentAmount).HasColumnType("decimal(18,4)");
            builder.Property(f => f.MonthlyFundAmount).HasColumnType("decimal(18,4)");
        }
    }
}