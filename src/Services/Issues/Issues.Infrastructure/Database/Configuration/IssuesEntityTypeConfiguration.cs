using Issues.Domain.Issues;
using Issues.Domain.TypesOfIssues;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Issues.Infrastructure.Database.Configuration
{
    public class IssuesEntityTypeConfiguration : IEntityTypeConfiguration<Issue>
    {
        public void Configure(EntityTypeBuilder<Issue> builder)
        {
            builder.Property(d => d.Id).IsRequired().HasMaxLength(63);
            builder.Property(d => d.Name).IsRequired().HasMaxLength(1023);
            builder.Property(d => d.StatusId).IsRequired().HasMaxLength(63);
            builder.Property(d => d.IsArchived).IsRequired();
            builder.Property(d => d.CreatingUserId).IsRequired().HasMaxLength(63);
            builder.Property(d => d.TimeOfCreation).IsRequired();
            //builder.Property(d => d.TypeOfIssueId).IsRequired();
            builder.Property(d => d.GroupOfIssueId).IsRequired();

            builder.OwnsOne(o=>o.Content, a => { a.Property<string>("IssueId").IsRequired(); a.WithOwner(); });
            builder.HasOne(d=>d.TypeOfIssue).WithMany().HasForeignKey(s=>s.TypeOfIssueId);
        }
    }
}