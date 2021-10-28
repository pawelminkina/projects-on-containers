using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Issues.Domain.Issues;
using Issues.Domain.TypesOfIssues;
using Issues.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Issues.Infrastructure.Repositories
{
    public class SqlTypeOfIssueRepository : ITypeOfIssueRepository
    {
        private readonly IssuesServiceDbContext _dbContext;

        public SqlTypeOfIssueRepository(IssuesServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<TypeOfIssue> GetTypeOfIssueByIdAsync(string id)
        {
            return await _dbContext.TypesOfIssues.Include(d => d.TypesInGroups).ThenInclude(d => d.Flow).ThenInclude(d=>d.StatusesInFlow).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<TypeOfIssue>> GetTypeOfIssuesForOrganizationAsync(string organizationId)
        {
            return _dbContext.TypesOfIssues.Include(d=>d.TypesInGroups).ThenInclude(d=>d.TypeOfGroup).Include(s=>s.TypesInGroups).ThenInclude(s=>s.Flow).Where(s => s.OrganizationId == organizationId);
        }

        public async Task<TypeOfIssue> AddNewTypeOfIssueAsync(TypeOfIssue type)
        {
            await _dbContext.TypesOfIssues.AddAsync(type);
            return await GetTypeOfIssueByIdAsync(type.Id);
        }

        public async Task RemoveTypeOfIssueInTypeofGroupOfIssues(TypeOfIssueInTypeOfGroup typeToDelete)
        {
            _dbContext.TypesOfIssueInTypeOfGroups.Remove(typeToDelete);
        }
    }
}