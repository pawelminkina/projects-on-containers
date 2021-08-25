using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Issues.Domain.StatusesFlow;
using Issues.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

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
            var status = new Status(name, organizationId);
            await _dbContext.Statuses.AddAsync(status);
            return await GetStatusById(status.Id);
        }

        public async Task<StatusFlow> AddNewStatusFlowAsync(string name, string organizationId)
        {
            var status = new StatusFlow(name, organizationId);
            await _dbContext.StatusFlows.AddAsync(status);
            return await GetFlowById(status.Id);
        }

        public async Task<Status> GetStatusById(string id)
        {
            return await _dbContext.Statuses.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Status>> GetStatusesForOrganization(string organizationId)
        {
            return _dbContext.Statuses.Where(s => s.OrganizationId == organizationId);
        }

        public async Task<StatusFlow> GetFlowById(string id)
        {
            return await _dbContext.StatusFlows
                .Include(s=>s.StatusesInFlow).ThenInclude(s=>s.ConnectedStatuses).ThenInclude(d=>d.ParentStatus)
                .Include(d=>d.StatusesInFlow).ThenInclude(d=>d.ConnectedStatuses).ThenInclude(d=>d.ConnectedWithParent)
                .Include(d=>d.StatusesInFlow).ThenInclude(d=>d.ParentStatus).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<StatusFlow>> GetFlowsByOrganization(string organizationId)
        {
            return _dbContext.StatusFlows
                .Include(s => s.StatusesInFlow).ThenInclude(s => s.ConnectedStatuses).ThenInclude(d => d.ParentStatus)
                .Include(d => d.StatusesInFlow).ThenInclude(d => d.ConnectedStatuses).ThenInclude(d => d.ConnectedWithParent)
                .Include(d => d.StatusesInFlow).ThenInclude(d => d.ParentStatus)
                .Where(s => s.OrganizationId == organizationId);
        }
    }
}