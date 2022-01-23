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
            return await _dbContext.TypesOfGroupsOfIssues.Include(d=>d.Groups).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<TypeOfGroupOfIssues>> GetTypeOfGroupOfIssuesForOrganizationAsync(string organizationId)
        {
            return _dbContext.TypesOfGroupsOfIssues.Include(d=>d.Groups).Where(s => s.OrganizationId == organizationId);
        }

        public async Task DeleteTypeofGroupOfIssuesAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AnyOfTypeOfGroupHasGivenNameAsync(string name, string organizationId)
        {
            throw new NotImplementedException();
        }

        public async Task<GroupOfIssues> GetGroupOfIssuesByIdAsync(string id)
        {
            return await _dbContext.GroupsOfIssues.Include(d=>d.TypeOfGroup).Include(s => s.Issues).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> AnyOfGroupHasGivenShortNameAsync(string shortName, string organizationId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AnyOfGroupHasGivenNameAsync(string name, string organizationId)
        {
            throw new NotImplementedException();
        }
    }
}
