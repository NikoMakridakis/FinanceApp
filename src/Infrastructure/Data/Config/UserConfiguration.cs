using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)

        {
            builder.Property(u => u.Email).HasMaxLength(100);
            builder.Property(u => u.UserName).HasMaxLength(100);
            builder.Property(u => u.FirstName).HasMaxLength(25);
            builder.Property(u => u.LastName).HasMaxLength(25);
            builder.Property(u => u.PhoneNumber).HasMaxLength(25);
            builder.Property(u => u.MonthlyIncome).HasColumnType("decimal(18,2)");
        }
    }
}
