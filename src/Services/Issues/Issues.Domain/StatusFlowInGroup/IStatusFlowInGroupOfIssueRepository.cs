using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Issues.Domain.StatusFlowInGroup
{
    public interface IStatusFlowInGroupOfIssueRepository
    {
        StatusFlowInGroupOfIssue GetByStatusFlowId(string statusFlowId);
        StatusFlowInGroupOfIssue GetByGroupOfIssueId(string groupOfIssueId);
        StatusFlowInGroupOfIssue Add(string statusFlowId, string groupOfIssueId);
        void Remove(string id);
    }
}
