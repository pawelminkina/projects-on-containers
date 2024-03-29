﻿using Issues.Domain.StatusesFlow;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Issues.Infrastructure.Database.Configuration
{
    public class StatusFlowEntityTypeConfiguration : IEntityTypeConfiguration<StatusFlow>
    {
        public void Configure(EntityTypeBuilder<StatusFlow> builder)
        {
            builder.Metadata.FindNavigation(nameof(StatusFlow.StatusesInFlow)).SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(d => d.Id).IsRequired().HasMaxLength(63);
            builder.Property(d => d.Name).IsRequired().HasMaxLength(1023);
            builder.Property(d => d.OrganizationId).IsRequired().HasMaxLength(63);
            builder.Property(d => d.IsDeleted).IsRequired();
            builder.Property(d => d.IsDefault).IsRequired();
            builder.HasMany(d => d.StatusesInFlow).WithOne(s => s.StatusFlow).OnDelete(DeleteBehavior.Cascade);
        }
    }
}