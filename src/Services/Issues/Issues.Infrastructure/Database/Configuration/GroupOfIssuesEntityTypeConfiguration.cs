using Issues.Domain.GroupsOfIssues;
using Issues.Domain.StatusesFlow;
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
            builder.Property(d => d.ShortName).IsRequired();
            builder.Property(d => d.IsDeleted).IsRequired();
            builder.Property(d => d.TimeOfDeleteUtc);
            builder.Property<string>("_typeOfGroupId").UsePropertyAccessMode(PropertyAccessMode.Field).IsRequired().HasMaxLength(63);
            builder.Property<string>("_connectedStatusFlowId").UsePropertyAccessMode(PropertyAccessMode.Field).HasMaxLength(63);
            builder.HasOne(d => d.ConnectedStatusFlow).WithOne(s=>s.ConnectedGroupOfIssues).HasForeignKey<GroupOfIssues>("_connectedStatusFlowId");
            builder.HasMany(d => d.Issues).WithOne(s => s.GroupOfIssue).HasForeignKey("_groupOfIssueId");
        }
    }
}