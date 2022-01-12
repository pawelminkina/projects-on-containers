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
            builder.Property(d => d.ParentStatusInFlowId).IsRequired().HasMaxLength(63);
            builder.HasOne(d => d.ConnectedStatus).WithMany().HasForeignKey(s=>s.ConnectedStatusId);
        }
    }
}