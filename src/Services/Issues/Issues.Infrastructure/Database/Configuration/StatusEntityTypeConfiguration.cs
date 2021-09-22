﻿using Issues.Domain.StatusesFlow;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Issues.Infrastructure.Database.Configuration
{
    public class StatusEntityTypeConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.Property(d => d.Id).IsRequired().HasMaxLength(63);
            builder.Property(d => d.Name).IsRequired().HasMaxLength(1023);
            builder.Property(d => d.OrganizationId).IsRequired().HasMaxLength(63);
            builder.Property(d => d.IsDeleted).IsRequired();
        }
    }
}