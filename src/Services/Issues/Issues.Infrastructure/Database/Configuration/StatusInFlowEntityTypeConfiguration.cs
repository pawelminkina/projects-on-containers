using Issues.Domain.StatusesFlow;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Issues.Infrastructure.Database.Configuration
{
    public class StatusInFlowEntityTypeConfiguration : IEntityTypeConfiguration<StatusInFlow>
    {
        public void Configure(EntityTypeBuilder<StatusInFlow> builder)
        {
            builder.Metadata.FindNavigation(nameof(StatusInFlow.ConnectedStatuses)).SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(d => d.Id).IsRequired().HasMaxLength(63);
            builder.Property(d => d.IsDefault).IsRequired();
            builder.Property(d => d.Name).IsRequired().HasMaxLength(63);
            builder.Property<string>("_statusFlowId").IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field).HasMaxLength(63);
            builder.HasOne(d => d.StatusFlow).WithMany(s => s.StatusesInFlow).HasForeignKey("_statusFlowId");
            builder.HasMany(d => d.ConnectedStatuses).WithOne(s => s.ParentStatusInFlow).HasForeignKey("_parentStatusInFlowId").OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}