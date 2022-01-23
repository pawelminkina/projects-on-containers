using Issues.Domain.GroupsOfIssues;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Issues.Infrastructure.Database.Configuration
{
    public class GroupOfIssuesEntityTypeConfiguration : IEntityTypeConfiguration<GroupOfIssues>
    {
        public void Configure(EntityTypeBuilder<GroupOfIssues> builder)
        {
            builder.Property(d => d.Id).IsRequired().HasMaxLength(63);
            builder.Property(d => d.Name).IsRequired().HasMaxLength(1023);
            builder.Property(d => d.ShortName).IsRequired().HasMaxLength(6);
            builder.Property(d => d.TypeOfGroupId).IsRequired().HasMaxLength(63);
            builder.Property(d => d.IsDeleted).IsRequired();
            builder.Property(d => d.TimeOfDeleteUtc);
            builder.HasOne(d => d.ConnectedStatusFlow).WithOne(s=>s.ConnectedGroupOfIssues).HasForeignKey<GroupOfIssues>(s=>s.ConnectedStatusFlowId);
            builder.HasMany(d => d.Issues).WithOne(s => s.GroupOfIssue).HasForeignKey(s=>s.GroupOfIssueId);
        }
    }
}