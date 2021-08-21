using Issues.Domain.StatusesFlow;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Issues.Infrastructure.Database.Configuration
{
    public class StatusInFlowEntityTypeConfiguration : IEntityTypeConfiguration<StatusInFlow>
    {
        public void Configure(EntityTypeBuilder<StatusInFlow> builder)
        {
            builder.Property(d => d.Id).IsRequired().HasMaxLength(63);
            builder.Property(d => d.IndexInFlow).IsRequired();
            builder.Property(d => d.IsArchived).IsRequired();
            builder.HasOne(d => d.StatusFlow).WithMany(s => s.StatusesInFlow);
            builder.HasOne(d => d.ParentStatus).WithMany().HasForeignKey(s => s.ParentStatusId);
            builder.HasMany(d => d.ConnectedStatuses).WithOne(d=>d.ParentStatus);
        }
    }
}