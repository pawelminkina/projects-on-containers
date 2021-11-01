using System.Collections.Generic;
using System.Threading.Tasks;
using WebBff.Api.Models.Issuses.TypeOfIssue;

namespace WebBff.Api.Services.Issues.TypeOfIssue
{
    public interface ITypeOfIssueService
    {
        Task<IEnumerable<TypeOfIssueDto>> GetTypesOfIssuesAsync();
        Task<TypeOfIssueDto> GetTypeOfIssuesAsync(string id);
        Task<string> CreateTypeOfIssuesAsync(CreateTypeOfIssuesRequest request);
        Task DeleteTypeOfIssuesAsync(string id);
        Task RenameTypeOfIssuesAsync(string id, string newName);
    }
}