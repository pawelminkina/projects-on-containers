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
            builder.Property(d => d.CreatingUserId).IsRequired().HasMaxLength(63);
            builder.Property(d => d.TimeOfCreation).IsRequired();

            builder.OwnsOne(o=>o.Content, a => { a.Property<string>("IssueId"); a.WithOwner(); });

            builder.Property<string>("_typeOfIssueId").UsePropertyAccessMode(PropertyAccessMode.Field).HasColumnName("TypeOfIssueId").IsRequired();
            builder.HasOne(d => d.TypeOfIssue).WithMany().HasForeignKey("_typeOfIssueId");
        }
    }
}