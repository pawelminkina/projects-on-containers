using System.Collections.Generic;
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
            return null;
        }

        public async Task<TypeOfGroupOfIssueDto> GetTypeOfGroupsOfIssuesAsync(string id)
        {
            var res = await _client.GetTypeOfGroupOfIssuesAsync(new GetTypeOfGroupOfIssuesRequest());
            return null;
        }

        public async Task<string> CreateTypeOfGroupOfIssueAsync(TypeOfGroupOfIssueDto type)
        {
            var res = await _client.GetTypeOfGroupOfIssuesAsync(new GetTypeOfGroupOfIssuesRequest());
            return string.Empty;
        }

        public async Task RenameTypeOfGroupOfIssueAsync(string id, string newName)
        {
            var res = await _client.RenameTypeOfGroupOfIssuesAsync(new RenameTypeOfGroupOfIssuesRequest());

        }

        public async Task DeleteTypeOfGroupOfIssuesAsync(string id)
        {
            var res = await _client.DeleteTypeOfGroupOfIssuesAsync(new DeleteTypeOfGroupOfIssuesRequest());

        }
    }
}