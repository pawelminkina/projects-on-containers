﻿using System;
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

        public Task<IEnumerable<TypeOfGroupOfIssues>> GetTypeOfGroupOfIssuesForOrganizationAsync(string organizationId)
        {
            return Task.FromResult(_dbContext.TypesOfGroupsOfIssues.Include(d=>d.Groups).Where(s => s.OrganizationId == organizationId).AsEnumerable());
        }

        public async Task DeleteTypeOfGroupOfIssuesAsync(string id)
        {
            _dbContext.TypesOfGroupsOfIssues.Remove(await GetTypeOfGroupOfIssuesByIdAsync(id));
        }

        public Task<bool> AnyOfTypeOfGroupHasGivenNameAsync(string name, string organizationId)
        {
            return Task.FromResult(_dbContext.TypesOfGroupsOfIssues.Any(s => s.OrganizationId == organizationId && s.Name == name));
        }

        public async Task<GroupOfIssues> GetGroupOfIssuesByIdAsync(string id)
        {
            return await _dbContext.GroupsOfIssues.Include(d=>d.TypeOfGroup).Include(s => s.Issues).ThenInclude(s=>s.StatusInFlow).Include(d=>d.ConnectedStatusFlow).ThenInclude(d=>d.StatusesInFlow).ThenInclude(s=>s.ConnectedStatuses).FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<bool> AnyOfGroupHasGivenShortNameAsync(string shortName, string organizationId)
        {
            return Task.FromResult(_dbContext.GroupsOfIssues.Any(s => s.TypeOfGroup.OrganizationId == organizationId && s.ShortName == shortName));
        }

        public Task<bool> AnyOfGroupHasGivenNameAsync(string name, string organizationId)
        {
            return Task.FromResult(_dbContext.GroupsOfIssues.Any(s => s.TypeOfGroup.OrganizationId == organizationId && s.Name == name));
        }
    }
}
