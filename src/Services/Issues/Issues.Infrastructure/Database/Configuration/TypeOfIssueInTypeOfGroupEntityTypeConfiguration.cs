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
            builder.HasOne(d => d.Parent).WithMany(d => d.TypesInGroups);

            builder.HasOne(d => d.Flow).WithMany().HasForeignKey("_statusFlowId");
            builder.Property<string>("_statusFlowId").UsePropertyAccessMode(PropertyAccessMode.Field).HasColumnName("StatusFlowId").IsRequired();

            builder.HasOne(d => d.TypeOfGroup).WithMany().HasForeignKey("_typeOfGroupOfIssuesId");
            builder.Property<string>("_typeOfGroupOfIssuesId").UsePropertyAccessMode(PropertyAccessMode.Field).HasColumnName("TypeOfGroupOfIssuesId").IsRequired();

        }
    }
}