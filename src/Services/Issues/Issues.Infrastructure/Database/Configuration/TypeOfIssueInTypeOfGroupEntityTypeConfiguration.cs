using Issues.Domain.TypesOfIssues;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Issues.Infrastructure.Database.Configuration
{
    public class TypeOfIssueInTypeOfGroupEntityTypeConfiguration : IEntityTypeConfiguration<TypeOfIssueInTypeOfGroup>
    {
        public void Configure(EntityTypeBuilder<TypeOfIssueInTypeOfGroup> builder)
        {
            builder.Property(d => d.Id).IsRequired().HasMaxLength(63);
            builder.Property(d => d.IsArchived).IsRequired();
            builder.Property(d => d.TypeOfGroupOfIssuesId).IsRequired();
            builder.Property(d => d.StatusFlowId).IsRequired();
            builder.HasOne(d => d.Parent).WithMany(d => d.TypesInGroups);

            builder.HasOne(d => d.Flow).WithMany().HasForeignKey("StatusFlowId");

            builder.HasOne(d => d.TypeOfGroup).WithMany().HasForeignKey("TypeOfGroupOfIssuesId");

        }
    }
}