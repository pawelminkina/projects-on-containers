using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Issues.Domain.Dtos;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.StatusesFlow;

namespace Issues.Domain
{
    public static class DefaultEntityCreator
    {
        public static StatusFlow CreateDefaultStatusFlow(string name, string organizationId, IEnumerable<StatusInFlowToCreateDto> statuses) =>
            StatusFlow.CreateDefault(name,organizationId, statuses);

        public static TypeOfGroupOfIssues CreateDefaultTypeOfGroupOfIssues(string name, string organizationId) =>
            TypeOfGroupOfIssues.CreateDefault(name, organizationId);
    }
}
