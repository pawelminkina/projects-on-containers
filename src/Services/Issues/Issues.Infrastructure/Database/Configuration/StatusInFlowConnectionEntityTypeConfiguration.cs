using Issues.Domain.StatusesFlow;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Issues.Infrastructure.Database.Configuration
{
    public class StatusInFlowConnectionEntityTypeConfiguration : IEntityTypeConfiguration<StatusInFlowConnection>
    {
        public void Configure(EntityTypeBuilder<StatusInFlowConnection> builder)
        {
            builder.Property(d => d.Id).IsRequired().HasMaxLength(63);
            builder.HasOne(d => d.ParentStatus).WithMany(d => d.ConnectedStatuses);
            builder.HasOne(d => d.ConnectedWithParent).WithOne()
                .HasForeignKey<StatusInFlowConnection>(d => d.ConnectedWithParentId);
        }
    }
}