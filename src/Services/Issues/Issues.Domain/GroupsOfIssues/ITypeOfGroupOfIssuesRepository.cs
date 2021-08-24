using System.Collections.Generic;
using System.Threading.Tasks;

namespace Issues.Domain.GroupsOfIssues
{
    public interface ITypeOfGroupOfIssuesRepository
    {
        Task<TypeOfGroupOfIssues> AddNewTypeofGroupOfIssues(string name, string organizationId);
        Task<TypeOfGroupOfIssues> GetTypeOfGroupOfIssuesById(string id);
        Task<IEnumerable<TypeOfGroupOfIssues>> GetTypeOfGroupOfIssuesForOrganization(string organizationId);
    }
}