using System.Collections.Generic;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.Issues;
using Issues.Domain.StatusesFlow;
using Issues.Domain.TypesOfIssues;
using Microsoft.Extensions.Logging;

namespace Issues.API.Infrastructure.Database.Seeding
{
    public interface IIssueSeedItemService
    {
        IEnumerable<TypeOfGroupOfIssues> GetTypeOfGroupOfIssuesFromSeed();
        IEnumerable<TypeOfIssue> GetTypesOfIssuesFromSeed();
        IEnumerable<Status> GetStatusesFromSeed();

        IEnumerable<GroupOfIssues> GetGroupsOfIssuesFromSeed();
        IEnumerable<Issue> GetIssuesFromSeed();

        IEnumerable<TypeOfIssueInTypeOfGroup> GetTypesOfIssueInTypeOfGroupsFromSeed();
        IEnumerable<StatusFlow> GetStatusFlowsFromSeed();

        IEnumerable<StatusInFlow> GetStatusesInFlowFromSeed();
        IEnumerable<StatusInFlowConnection> GetStatusesInFlowConnectionFromSeed();
    }
}
