using System.Collections.Generic;
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
            return null;
        }

        public async Task<TypeOfIssueDto> GetTypeOfIssuesAsync(string id)
        {
            var res = await _client.GetTypeOfIssueAsync(new GetTypeOfIssueRequest());
            return null;
        }

        public async Task<string> CreateTypeOfIssuesAsync(TypeOfIssueDto type)
        {
            var res = await _client.CreateTypeOfIssueAsync(new CreateTypeOfIssueRequest());
            return string.Empty;
        }

        public async Task DeleteTypeOfIssuesAsync(string id)
        {
            var res = await _client.ArchiveTypeOfIssueAsync(new ArchiveTypeOfIssueRequest());
        }

        public async Task RenameTypeOfIssuesAsync(string id, string newName)
        {
            var res = await _client.RenameTypeOfIssueAsync(new RenameTypeOfIssueRequest());
        }
    }
}