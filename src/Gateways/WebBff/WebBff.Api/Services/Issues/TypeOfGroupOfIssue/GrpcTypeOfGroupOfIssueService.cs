using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Issues.API.Protos;
using WebBff.Api.Models.Issuses.TypeOfGroupOfIssue;

namespace WebBff.Api.Services.Issues.TypeOfGroupOfIssue
{
    public class GrpcTypeOfGroupOfIssueService : ITypeOfGroupOfIssueService
    {
        private readonly TypeOfGroupOfIssueService.TypeOfGroupOfIssueServiceClient _client;

        public GrpcTypeOfGroupOfIssueService(TypeOfGroupOfIssueService.TypeOfGroupOfIssueServiceClient client)
        {
            _client = client;
        }
        public async Task<IEnumerable<TypeOfGroupOfIssueDto>> GetTypesOfGroupsOfIssuesAsync()
        {
            var res = await _client.GetTypesOfGroupsOfIssuesAsync(new GetTypesOfGroupsOfIssuesRequest());
            return res.Types_.Select(MapToDto);
        }

        public async Task<TypeOfGroupOfIssueDto> GetTypeOfGroupsOfIssuesAsync(string id)
        {
            var res = await _client.GetTypeOfGroupOfIssuesAsync(new GetTypeOfGroupOfIssuesRequest());
            return MapToDto(res.Type);
        }

        public async Task<string> CreateTypeOfGroupOfIssueAsync(TypeOfGroupOfIssueDto type)
        {
            var res = await _client.CreateTypefOfGroupOfIssuesAsync(new CreateTypefOfGroupOfIssuesRequest(){Name = type.Name});
            return res.Id;
        }

        public async Task RenameTypeOfGroupOfIssueAsync(string id, string newName)
        {
            await _client.RenameTypeOfGroupOfIssuesAsync(new RenameTypeOfGroupOfIssuesRequest(){Id = id, NewName = newName});
        }

        public async Task DeleteTypeOfGroupOfIssuesAsync(string id)
        {
            await _client.DeleteTypeOfGroupOfIssuesAsync(new DeleteTypeOfGroupOfIssuesRequest(){Id = id});
        }

        private TypeOfGroupOfIssueDto MapToDto(TypeOfGroupOfIssues type) 
            => new TypeOfGroupOfIssueDto() {Id = type.Id, Name = type.Name, OrganizationId = type.OrganizationId};
    }
}