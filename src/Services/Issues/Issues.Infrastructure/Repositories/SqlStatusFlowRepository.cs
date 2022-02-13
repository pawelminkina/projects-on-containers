using Issues.Domain.GroupsOfIssues;
using Issues.Domain.StatusesFlow;
using Issues.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Issues.Infrastructure.Repositories
{
    public class SqlStatusFlowRepository : IStatusFlowRepository
    {
        private readonly IssuesServiceDbContext _dbContext;

        public SqlStatusFlowRepository(IssuesServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<StatusFlow> AddNewStatusFlowAsync(StatusFlow statusFlow)
        {
            await _dbContext.StatusFlows.AddAsync(statusFlow);
            return statusFlow;
        }
        public async Task<StatusFlow> GetFlowById(string id)
        {
            return await GetFlowsWithInclude().FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<StatusInFlow> GetStatusInFlowById(string id)
        {
            return Task.FromResult(_dbContext.StatusesInFlow.Include(s => s.ConnectedStatuses).ThenInclude(d => d.ConnectedStatusInFlow)
                .Include(d=>d.StatusFlow).ThenInclude(s=>s.StatusesInFlow)
                .FirstOrDefault(s => s.Id == id));
        }

        public Task<IEnumerable<StatusFlow>> GetFlowsByOrganizationAsync(string organizationId)
        {
            return Task.FromResult(GetFlowsWithInclude().Where(s => s.OrganizationId == organizationId).AsEnumerable());
        }

        public Task<StatusFlow> GetDefaultStatusFlowAsync(string organizationId)
        {
            return Task.FromResult(GetFlowsWithInclude().FirstOrDefault(d => d.OrganizationId == organizationId && d.IsDefault));
        }

        private IIncludableQueryable<StatusFlow, TypeOfGroupOfIssues> GetFlowsWithInclude() =>

            _dbContext.StatusFlows
                .Include(d => d.StatusesInFlow).ThenInclude(d => d.ConnectedStatuses)
                .ThenInclude(d => d.ConnectedStatusInFlow)
                .Include(d => d.ConnectedGroupOfIssues).ThenInclude(s => s.TypeOfGroup);



        public Task RemoveStatusInFlow(StatusInFlow statusInFlow)
        {
            return Task.FromResult(_dbContext.StatusesInFlow.Remove(statusInFlow));
        }
    }
}