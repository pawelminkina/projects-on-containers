using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Issues.API.Protos;
using WebBff.Api.Models.Issuses.TypeOfIssue;

namespace WebBff.Api.Services.Issues.TypeOfIssue
{
    public class GrpcTypeOfIssueService : ITypeOfIssueService
    {
        private readonly TypeOfIssueService.TypeOfIssueServiceClient _client;

        public GrpcTypeOfIssueService(TypeOfIssueService.TypeOfIssueServiceClient client)
        {
            _client = client;
        }
        public async Task<IEnumerable<TypeOfIssueDto>> GetTypesOfIssuesAsync()
        {
            var res = await _client.GetTypesOfIssuesAsync(new GetTypesOfIssuesRequest());
            return res.Types_.Select(MapToDto);
        }

        public async Task<TypeOfIssueDto> GetTypeOfIssuesAsync(string id)
        {
            var res = await _client.GetTypeOfIssueAsync(new GetTypeOfIssueRequest() { Id = id});
            return MapToDto(res.Type);
        }

        public async Task DeleteTypeOfIssuesAsync(string id)
        {
            var res = await _client.ArchiveTypeOfIssueAsync(new ArchiveTypeOfIssueRequest() { Id = id});
        }

        public async Task RenameTypeOfIssuesAsync(string id, string newName)
        {
            var res = await _client.RenameTypeOfIssueAsync(new RenameTypeOfIssueRequest() { Id = id, Name = newName});
        }

        public async Task<string> CreateTypeOfIssuesAsync(CreateTypeOfIssuesRequest request)
        {
            var res = await _client.CreateTypeOfIssueAsync(new CreateTypeOfIssueRequest() { Name = request.Name});
            return res.Id;
        }

        private TypeOfIssueDto MapToDto(TypeOfIssues type) => new TypeOfIssueDto() { Id = type.Id, Name = type.Name };
    }
}