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
            return await _dbContext.TypesOfIssues.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<TypeOfIssue>> GetTypeOfIssuesForOrganizationAsync(string organizationId)
        {
            return _dbContext.TypesOfIssues.Where(s => s.OrganizationId == organizationId);
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