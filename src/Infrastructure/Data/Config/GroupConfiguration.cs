using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.Property(g => g.GroupId).IsRequired();
            builder.Property(g => g.BudgetId).IsRequired();
            builder.Property(g => g.Title).HasMaxLength(150);
        }
    }
}