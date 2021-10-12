using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Issues.API.Protos;
using WebBff.Api.Models.Issuses.GroupOfIssue;
using CreateGroupOfIssuesRequest = Issues.API.Protos.CreateGroupOfIssuesRequest;

namespace WebBff.Api.Services.Issues.GroupOfIssue
{
    public class GrpcGroupOfIssueService : IGroupOfIssueService
    {
        private readonly GroupOfIssueService.GroupOfIssueServiceClient _client;

        public GrpcGroupOfIssueService(GroupOfIssueService.GroupOfIssueServiceClient client)
        {
            _client = client;
        }

        public async Task<string> CreateGroupOfIssueAsync(Models.Issuses.GroupOfIssue.CreateGroupOfIssuesRequest request)
        {
            var res = await _client.CreateGroupOfIssuesAsync(new CreateGroupOfIssuesRequest(){Name = request.Name, ShortName = request.ShortName, TypeOfGroupId = request.TypeOfGroupId});
            return res.Id;
        }

        public async Task DeleteGroupOfIssueAsync(string id)
        {
            await _client.ArchiveGroupOfIssuesAsync(new ArchiveGroupOfIssuesRequest(){Id = id});
        }

        public async Task<GroupOfIssueDto> GetGroupOfIssueAsync(string id)
        {
            var res = await _client.GetGroupOfIssuesAsync(new GetGroupOfIssuesRequest() {Id = id});
            return MapToDto(res.Group);
        }

        public async Task<IEnumerable<GroupOfIssueDto>> GetGroupsOfIssuesAsync()
        {
            var res = await _client.GetGroupsOfIssuesAsync(new GetGroupsOfIssuesRequest());
            return res.Groups.Where(d=> !d.IsArchived).Select(MapToDto);
        }

        public async Task RenameGroupOfIssueAsync(string id, string newName)
        {
            await _client.RenameGroupOfIssuesAsync(new RenameGroupOfIssuesRequest() {Id = id, NewName = newName});
        }

        private GroupOfIssueDto MapToDto(global::Issues.API.Protos.GroupOfIssue group)
        {
            return new GroupOfIssueDto()
            {
                Id = group.Id,
                Name = group.Name,
                ShortName = group.ShortName,
                TypeOfGroupId = group.TypeOfGroupId
            };
        }
    }
}