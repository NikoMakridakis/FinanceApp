using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Web.Data.Config
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)

        {
            builder.Property(u => u.Email).HasMaxLength(100);
            builder.Property(u => u.FullName).HasMaxLength(100);
            builder.Property(u => u.MonthlyIncome).HasColumnType("decimal(18,2)");
        }
    }
}
