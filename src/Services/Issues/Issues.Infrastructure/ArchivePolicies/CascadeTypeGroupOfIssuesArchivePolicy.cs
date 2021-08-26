using System.Linq;
using Issues.Domain.GroupsOfIssues;
using Issues.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Issues.Infrastructure.ArchivePolicies
{
    public class CascadeTypeGroupOfIssuesArchivePolicy : ITypeGroupOfIssuesArchivePolicy
    {
        private readonly IssuesServiceDbContext _dbContext;

        public CascadeTypeGroupOfIssuesArchivePolicy(IssuesServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Archive(TypeOfGroupOfIssues type)
        {
            var groups = _dbContext.GroupsOfIssues.Include(s => s.Issues)
                .Where(s => s.TypeOfGroupId == type.Id);

            foreach (var group in groups)
                group.Archive();
        }
    }
}