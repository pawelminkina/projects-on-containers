using Issues.Domain.Issues;
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
            builder.HasOne(s => s.Content).WithOne(s => s.ParentIssue).HasForeignKey<IssueContent>(d=>d.ParentIssueId);
            builder.Property(d => d.CreatingUserId).IsRequired().HasMaxLength(63);
            builder.Property(d => d.TimeOfCreation).IsRequired();
            builder.HasOne(s => s.TypeOfIssue).WithMany().HasForeignKey(d => d.TypeOfIssueId);
        }
    }
}