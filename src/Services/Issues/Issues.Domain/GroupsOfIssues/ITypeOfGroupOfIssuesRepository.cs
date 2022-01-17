using System.Collections.Generic;
using System.Threading.Tasks;

namespace Issues.Domain.GroupsOfIssues
{
    public interface ITypeOfGroupOfIssuesRepository
    {
        Task AddNewTypeofGroupOfIssuesAsync(TypeOfGroupOfIssues type);
        Task<TypeOfGroupOfIssues> GetTypeOfGroupOfIssuesByIdAsync(string id);
        Task<IEnumerable<TypeOfGroupOfIssues>> GetTypeOfGroupOfIssuesForOrganizationAsync(string organizationId);
        Task DeleteTypeofGroupOfIssuesAsync(string id);
    }
}