using System.Threading.Tasks;
using Issues.Domain.StatusesFlow;
using Issues.Infrastructure.Database;

namespace Issues.Infrastructure.Repositories
{
    public class SqlStatusRepository : IStatusRepository
    {
        private readonly IssuesServiceDbContext _dbContext;

        public SqlStatusRepository(IssuesServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Status> AddNewStatusAsync(string name, string organizationId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<StatusFlow> AddNewStatusFlowAsync(string name, string organizationId)
        {
            throw new System.NotImplementedException();
        }
    }
}