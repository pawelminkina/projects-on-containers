﻿using Issues.Domain.StatusesFlow;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Issues.Infrastructure.Database.Configuration
{
    public class StatusInFlowConnectionEntityTypeConfiguration : IEntityTypeConfiguration<StatusInFlowConnection>
    {
        public void Configure(EntityTypeBuilder<StatusInFlowConnection> builder)
        {
            builder.Property(d => d.Id).IsRequired().HasMaxLength(63);
            builder.Property<string>("_parentStatusInFlowId").IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field).HasMaxLength(63);
            builder.Property<string>("_connectedStatusInFlowId").IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field).HasMaxLength(63);
            builder.HasOne(d => d.ConnectedStatusInFlow).WithMany().HasForeignKey("_connectedStatusInFlowId").OnDelete(DeleteBehavior.NoAction);
        }
    }
}