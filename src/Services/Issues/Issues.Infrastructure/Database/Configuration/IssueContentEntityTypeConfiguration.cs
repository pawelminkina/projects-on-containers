using Issues.Domain.Issues;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Issues.Infrastructure.Database.Configuration
{
    public class IssueContentEntityTypeConfiguration : IEntityTypeConfiguration<IssueContent>
    {
        public void Configure(EntityTypeBuilder<IssueContent> builder)
        {
            builder.Property(d => d.Id).IsRequired().HasMaxLength(63);
            builder.Property(d => d.TextContent).IsRequired();
            builder.Property(d => d.IsArchived).IsRequired();
        }
    }
}