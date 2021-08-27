using Issues.Domain.GroupsOfIssues;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Issues.Infrastructure.Database.Configuration
{
    public class TypeOfGroupOfIssuesEntityTypeConfiguration : IEntityTypeConfiguration<TypeOfGroupOfIssues>
    {
        public void Configure(EntityTypeBuilder<TypeOfGroupOfIssues> builder)
        {
            builder.Property(d => d.Id).IsRequired().HasMaxLength(63);
            builder.Property(d => d.Name).IsRequired().HasMaxLength(1023);
            builder.Property(d => d.OrganizationId).IsRequired().HasMaxLength(63);
            builder.Property(d => d.IsArchived).IsRequired();
            builder.HasMany(d => d.Groups).WithOne(d => d.TypeOfGroup);
        }
    }
}