using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WebBff.Api.Models.Issuses.TypeOfGroupOfIssue;

namespace WebBff.Api.Services.Issues.TypeOfGroupOfIssue
{
    public interface ITypeOfGroupOfIssueService
    {
        Task<IEnumerable<TypeOfGroupOfIssueDto>> GetTypesOfGroupsOfIssuesAsync();
        Task<TypeOfGroupOfIssueDto> GetTypeOfGroupsOfIssuesAsync(string id);
        Task<string> CreateTypeOfGroupOfIssueAsync(TypeOfGroupOfIssueDto type);
        Task RenameTypeOfGroupOfIssueAsync(string id, string newName);
        Task DeleteTypeOfGroupOfIssuesAsync(string id);

    }
}