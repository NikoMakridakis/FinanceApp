﻿using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class BudgetGroupConfiguration : IEntityTypeConfiguration<BudgetGroup>
    {
        public void Configure(EntityTypeBuilder<BudgetGroup> builder)
        {
            builder.Property(b => b.BudgetGroupId).IsRequired();
            builder.Property(b => b.UserId).IsRequired();
            builder.Property(b => b.BudgetGroupTitle).HasMaxLength(50);
        }
    }
}