using System.Collections.Generic;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.Issues;
using Issues.Domain.StatusesFlow;
using Microsoft.Extensions.Logging;

namespace Issues.API.Infrastructure.Database.Seeding
{
    public interface IIssueSeedItemService
    {
        IEnumerable<TypeOfGroupOfIssues> GetTypeOfGroupOfIssuesFromSeed();
        IEnumerable<GroupOfIssues> GetGroupsOfIssuesFromSeed();
        IEnumerable<Issue> GetIssuesFromSeed();
        IEnumerable<StatusFlow> GetStatusFlowsFromSeed();
        IEnumerable<StatusInFlow> GetStatusesInFlowFromSeed();
        IEnumerable<StatusInFlowConnection> GetStatusesInFlowConnectionFromSeed();
    }
}
