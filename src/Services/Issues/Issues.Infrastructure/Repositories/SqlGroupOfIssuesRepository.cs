using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Issues.Domain.GroupsOfIssues;
using Issues.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Issues.Infrastructure.Repositories
{
    public class SqlGroupOfIssuesRepository : ITypeOfGroupOfIssuesRepository, IGroupOfIssuesRepository
    {
        private readonly IssuesServiceDbContext _dbContext;

        public SqlGroupOfIssuesRepository(IssuesServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<TypeOfGroupOfIssues> AddNewTypeofGroupOfIssues(string name, string organizationId)
        {
            var newType = new TypeOfGroupOfIssues(organizationId, name);
            await _dbContext.TypesOfGroupsOfIssues.AddAsync(newType);
            return await GetTypeOfGroupOfIssuesById(newType.Id);
        }

        public async Task<TypeOfGroupOfIssues> GetTypeOfGroupOfIssuesById(string id)
        {
            return await _dbContext.TypesOfGroupsOfIssues.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<TypeOfGroupOfIssues>> GetTypeOfGroupOfIssuesForOrganization(string organizationId)
        {
            return _dbContext.TypesOfGroupsOfIssues.Where(s => s.OrganizationId == organizationId);
        }

        public async Task<GroupOfIssues> GetGroupOfIssuesByIdAsync(string id)
        {
            return await _dbContext.GroupsOfIssues.Include(s=>s.Flow).Include(s=>s.Issues).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<GroupOfIssues>> GetGroupOfIssuesForOrganizationAsync(string organizationId)
        {
            return _dbContext.GroupsOfIssues.Include(s => s.Flow).Include(s => s.Issues).Where(s => s.OrganizationId == organizationId);
        }

        //TODO this can be added with domain login instead of repository pattern, i think so
        public async Task<GroupOfIssues> AddNewGroupOfIssues(string name, string organizationId, string typeOfGroupId, string statusFlowId)
        {
            var newType = new GroupOfIssues(name, organizationId, typeOfGroupId, statusFlowId);
            await _dbContext.GroupsOfIssues.AddAsync(newType);
            return await GetGroupOfIssuesByIdAsync(newType.Id);
        }

    }
}
