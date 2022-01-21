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
    public class SqlStatusRepository : IStatusRepository, IStatusFlowRepository
    {
        private readonly IssuesServiceDbContext _dbContext;

        public SqlStatusRepository(IssuesServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Status> AddNewStatusAsync(string name, string organizationId)
        {
            var status = new Status(name, organizationId);
            await _dbContext.Statuses.AddAsync(status);
            return status;
        }

        public async Task<StatusFlow> AddNewStatusFlowAsync(string name, string organizationId)
        {
            var status = new StatusFlow(name, organizationId);
            await _dbContext.StatusFlows.AddAsync(status);
            return status;
        }

        public async Task<Status> GetStatusById(string id)
        {
            return await _dbContext.Statuses.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Status>> GetStatusesForOrganization(string organizationId)
        {
            return _dbContext.Statuses.Where(s => s.OrganizationId == organizationId);
        }

        public async Task RemoveStatusById(string id)
        {
            var statusToRemove = await GetStatusById(id);
            _dbContext.Statuses.Remove(statusToRemove);
        }

        public async Task<StatusFlow> GetFlowById(string id)
        {
            return await GetFlowsWithInclude().FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<StatusFlow>> GetFlowsByOrganizationAsync(string organizationId)
        {
            return GetFlowsWithInclude().Where(s => s.OrganizationId == organizationId);
        }

        private IIncludableQueryable<StatusFlow, Status> GetFlowsWithInclude() =>

            _dbContext.StatusFlows
                .Include(d => d.StatusesInFlow).ThenInclude(d => d.ConnectedStatuses).ThenInclude(d => d.ConnectedStatusInFlow)
                .Include(d => d.StatusesInFlow).ThenInclude(d => d.ConnectedStatuses)
                .Include(d => d.StatusesInFlow).ThenInclude(d => d.ParentStatus);
        

        public async Task RemoveStatusInFlow(string statusInFlowId)
        {
            var statusInFlow = await _dbContext.StatusesInFlow.FirstOrDefaultAsync(s=>s.Id == statusInFlowId);
            _dbContext.StatusesInFlow.Remove(statusInFlow);
        }
    }
}