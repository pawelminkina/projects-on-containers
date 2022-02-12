using Issues.API.Protos;
using WebBff.Aggregator.Models.GroupOfIssues;
using System.Linq;
using WebBff.Aggregator.Services.Issues;

namespace WebBff.Aggregator.Services.GroupOfIssues
{
    public class GrpcGroupOfIssuesService : IGroupOfIssuesService
    {
        private readonly GroupOfIssueService.GroupOfIssueServiceClient _grpcClient;
        private readonly IssueService.IssueServiceClient _issuesGrpcClient;

        public GrpcGroupOfIssuesService(GroupOfIssueService.GroupOfIssueServiceClient grpcClient, IssueService.IssueServiceClient issuesGrpcClient)
        {
            _grpcClient = grpcClient;
            _issuesGrpcClient = issuesGrpcClient;
        }

        public async Task<IEnumerable<GroupOfIssuesDto>> GetGroupsOfIssues()
        {
            var response = await _grpcClient.GetGroupsOfIssuesAsync(new GetGroupsOfIssuesRequest());
            return response.Groups.Select(MapToDto);
        }

        public async Task<GroupOfIssuesDto> GetGroupOfIssues(string id)
        {
            var response = await _grpcClient.GetGroupOfIssuesAsync(new GetGroupOfIssuesRequest() {Id = id});
            return MapToDto(response.Group);
        }

        public async Task<string> CreateGroupOfIssues(GroupOfIssuesForCreationDto dto)
        {
            var response = await _grpcClient.CreateGroupOfIssuesAsync(new CreateGroupOfIssuesRequest() {Name = dto.Name, ShortName = dto.ShortName,TypeOfGroupId = dto.TypeOfGroupId});
            return response.Id;
        }

        public async Task RenameGroupOfIssues(RenameGroupOfIssuesDto dto)
        {
            await _grpcClient.RenameGroupOfIssuesAsync(new RenameGroupOfIssuesRequest() {Id=dto.Id, NewName = dto.NewName});
        }

        public async Task ChangeShortNameForGroupOfIssues(ChangeShortNameForGroupOfIssuesDto dto)
        {
            await _grpcClient.ChangeShortNameForGroupOfIssuesAsync(new ChangeShortNameForGroupOfIssuesRequest() {Id = dto.Id, NewShortName = dto.NewShortName});
        }

        public async Task DeleteGroupOfIssues(string id)
        {
            await _grpcClient.DeleteGroupOfIssuesAsync(new DeleteGroupOfIssuesRequest() {Id = id});
        }


        private GroupOfIssuesDto MapToDto(GroupOfIssue group)
        {
            return new GroupOfIssuesDto()
            {
                Id = group.Id,
                IsInTrash = group.IsInThrash,
                Name = group.Name,
                ShortName = group.ShortName,
                TimeOfDelete = group.TimeOfDelete?.ToDateTimeOffset(),
                TypeOfGroupId = group.TypeOfGroupId
            };
        }

    
    }
}
