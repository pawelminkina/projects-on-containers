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
        public async Task AddNewTypeofGroupOfIssuesAsync(TypeOfGroupOfIssues type)
        {
            await _dbContext.TypesOfGroupsOfIssues.AddAsync(type);
        }

        public async Task<TypeOfGroupOfIssues> GetTypeOfGroupOfIssuesByIdAsync(string id)
        {
            return await _dbContext.TypesOfGroupsOfIssues.FirstOrDefaultAsync(s => s.Id == id && !s.IsArchived);
        }

        public async Task<IEnumerable<TypeOfGroupOfIssues>> GetTypeOfGroupOfIssuesForOrganizationAsync(string organizationId)
        {
            return _dbContext.TypesOfGroupsOfIssues.Where(s => s.OrganizationId == organizationId && !s.IsArchived);
        }

        public async Task<GroupOfIssues> GetGroupOfIssuesByIdAsync(string id)
        {
            return await _dbContext.GroupsOfIssues.Include(s=>s.Flow).Include(s=>s.Issues).FirstOrDefaultAsync(s => s.Id == id && !s.IsArchived);
        }

        public async Task<IEnumerable<GroupOfIssues>> GetGroupOfIssuesForOrganizationAsync(string organizationId)
        {
            return _dbContext.GroupsOfIssues.Include(s => s.Flow).Include(s => s.Issues).Where(s => s.OrganizationId == organizationId && !s.IsArchived);
        }

    }
}
