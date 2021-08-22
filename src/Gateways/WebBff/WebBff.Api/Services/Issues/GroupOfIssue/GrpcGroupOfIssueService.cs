using System.Collections.Generic;
using System.Threading.Tasks;
using Issues.API.Protos;
using WebBff.Api.Models.Issuses.GroupOfIssue;

namespace WebBff.Api.Services.Issues.GroupOfIssue
{
    public class GrpcGroupOfIssueService : IGroupOfIssueService
    {
        private readonly GroupOfIssueService.GroupOfIssueServiceClient _client;

        public GrpcGroupOfIssueService(GroupOfIssueService.GroupOfIssueServiceClient client)
        {
            _client = client;
        }
        public async Task ChangeGroupOfIssueTypeAsync()
        {
            await _client.ChangeGroupOfIssueTypeAsync(new ChangeGroupOfIssueTypeRequest());
        }

        public async Task<string> CreateGroupOfIssueAsync(GroupOfIssueDto groupOfIssue)
        {
            var res = await _client.CreateGroupOfIssuesAsync(new CreateGroupOfIssuesRequest());
            return string.Empty;
        }

        public async Task DeleteGroupOfIssueAsync(string id)
        {
            await _client.DeleteGroupOfIssuesAsync(new DeleteGroupOfIssuesRequest());
        }

        public async Task<GroupOfIssueDto> GetGroupOfIssueAsync(string id)
        {
            var res = await _client.GetGroupOfIssuesAsync(new GetGroupOfIssuesRequest());
            return null;
        }

        public async Task<IEnumerable<GroupOfIssueDto>> GetGroupsOfIssuesAsync()
        {
            var res =await _client.GetGroupsOfIssuesAsync(new GetGroupsOfIssuesRequest());
            return null;

        }

        public async Task RenameGroupOfIssueAsync(string id, string newName)
        {
            await _client.RenameGroupOfIssuesAsync(new RenameGroupOfIssuesRequest());
        }
    }
}