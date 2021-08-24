using System.Collections.Generic;
using System.Threading.Tasks;
using Issues.Domain.Issues;
using Issues.Infrastructure.Database;

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
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<TypeOfIssue>> GetTypeOfIssuesForOrganizationAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<TypeOfIssue> AddNewTypeOfIssueAsync(string organizationId, string name, string statusFlowId, string typeOfGroupOfIssuesId)
        {
            throw new System.NotImplementedException();
        }
    }
}