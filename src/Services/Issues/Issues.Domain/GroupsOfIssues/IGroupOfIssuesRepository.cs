using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Issues.Domain.GroupsOfIssues
{
    public interface IGroupOfIssuesRepository
    {
        Task<GroupOfIssues> GetGroupOfIssuesByIdAsync(string id);
        Task<IEnumerable<GroupOfIssues>> GetGroupOfIssuesForOrganizationAsync(string organizationId);
    }
}
