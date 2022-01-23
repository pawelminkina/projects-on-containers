using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Issues.Domain.StatusesFlow;
using Issues.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

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
            throw new System.NotImplementedException();

        }
        public async Task<StatusFlow> GetFlowById(string id)
        {
            return await GetFlowsWithInclude().FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<StatusInFlow> GetStatusInFlowById(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<StatusFlow>> GetFlowsByOrganizationAsync(string organizationId)
        {
            return GetFlowsWithInclude().Where(s => s.OrganizationId == organizationId);
        }

        public async Task<StatusFlow> GetDefaultStatusFlowAsync(string organizationId)
        {
            throw new System.NotImplementedException();
        }

        private IIncludableQueryable<StatusFlow, StatusFlow> GetFlowsWithInclude() =>

            _dbContext.StatusFlows
                .Include(d => d.StatusesInFlow).ThenInclude(d => d.ConnectedStatuses).ThenInclude(d => d.ConnectedStatusInFlow).ThenInclude(d => d.StatusFlow)
                .Include(d => d.StatusesInFlow).ThenInclude(d => d.ConnectedStatuses).ThenInclude(s=>s.ParentStatusInFlow).ThenInclude(d=>d.StatusFlow);
        

        public async Task RemoveStatusInFlow(string statusInFlowId)
        {
            var statusInFlow = await _dbContext.StatusesInFlow.FirstOrDefaultAsync(s=>s.Id == statusInFlowId);
            _dbContext.StatusesInFlow.Remove(statusInFlow);
        }
    }
}