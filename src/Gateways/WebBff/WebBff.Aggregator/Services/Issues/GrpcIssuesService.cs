using Issues.API.Protos;
using WebBff.Aggregator.Models.Issues;

namespace WebBff.Aggregator.Services.Issues;

public class GrpcIssuesService : IIssuesService
{
    private readonly IssueService.IssueServiceClient _grpcClient;

    public GrpcIssuesService(IssueService.IssueServiceClient grpcClient)
    {
        _grpcClient = grpcClient;
    }
    public async Task<IEnumerable<IssueReferenceDto>> GetIssuesForUser(string userId)
    {
        var response = await _grpcClient.GetIssuesForUserAsync(new GetIssuesForUserRequest() {UserId = userId});
        return response.Issues.Select(MapReferenceToDto);
    }

    public async Task<IEnumerable<IssueReferenceDto>> GetIssuesForGroup(string groupId)
    {
        var response = await _grpcClient.GetIssuesForGroupAsync(new GetIssuesForGroupRequest() {GroupId = groupId});
        return response.Issues.Select(MapReferenceToDto);
    }

    public async Task<IssueWithContentDto> GetIssueWithContent(string id)
    {
        var response = await _grpcClient.GetIssueWithContentAsync(new GetIssueWithContentRequest() {Id = id});
        return MapToContentDto(response.Issue, response.Content);
    }

    public async Task<string> CreateIssue(IssueForCreationDto dto)
    {
        var response = await _grpcClient.CreateIssueAsync(new CreateIssueRequest() {GroupId = dto.GroupId, Name = dto.Name, TextContent = dto.TextContent});
        return response.Id;
    }

    public async Task RenameIssue(string id, string newName)
    {
        await _grpcClient.RenameIssueAsync(new RenameIssueRequest() { Id = id, NewName = newName });
    }

    public async Task UpdateTextContentOfIssue(string id, string textContent)
    {
        await _grpcClient.UpdateIssueTextContentAsync(new UpdateIssueTextContentRequest() { Id = id, TextContent = textContent });
    }

    public async Task DeleteIssue(string id)
    {
        await _grpcClient.DeleteIssueAsync(new DeleteIssueRequest() {Id = id});
    }

    public async Task ChangeStatusOfIssue(string issueId, string newStatusInFlowId)
    {
        await _grpcClient.ChangeStatusOfIssueAsync(new ChangeStatusOfIssueRequest() {IssueId = issueId, NewStatusInFlowId = newStatusInFlowId});
    }

    private IssueReferenceDto MapReferenceToDto(IssueReference reference)
    {
        return new IssueReferenceDto()
        {
            CreatingUserId = reference.CreatingUserId,
            GroupId = reference.GroupId,
            Id = reference.Id,
            IsDeleted = reference.IsDeleted,
            Name = reference.Name,
            StatusName = reference.StatusName,
            TimeOfCreation = reference.TimeOfCreation.ToDateTimeOffset()
        };
    }

    private IssueWithContentDto MapToContentDto(IssueReference reference, IssueContent content)
    {
        return new IssueWithContentDto(MapReferenceToDto(reference))
        {
            TextContent = content.TextContent
        };
    }
}