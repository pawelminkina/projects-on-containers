using Issues.Domain.Issues;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Issues.Infrastructure.Database.Configuration
{
    public class TypeOfIssueEntityTypeConfiguration : IEntityTypeConfiguration<TypeOfIssue>
    {
        public void Configure(EntityTypeBuilder<TypeOfIssue> builder)
        {
            builder.Property(d => d.Id).IsRequired().HasMaxLength(63);
            builder.Property(d => d.Name).IsRequired().HasMaxLength(1023);
            builder.Property(d => d.StatusFlowId).IsRequired().HasMaxLength(63);
            builder.Property(d => d.TypeOfGroupOfIssuesId).IsRequired().HasMaxLength(63);
            builder.Property(d => d.Icon).IsRequired();
            builder.Property(d => d.OrganizationId).IsRequired().HasMaxLength(63);
            builder.Property(d => d.IsArchived).IsRequired();
        }
    }
}