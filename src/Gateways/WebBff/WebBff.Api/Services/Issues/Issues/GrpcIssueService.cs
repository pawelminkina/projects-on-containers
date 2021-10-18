using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Issues.API.Protos;
using Microsoft.AspNetCore.Rewrite;
using WebBff.Api.Models.Issuses.Issues;
using CreateIssueRequest = Issues.API.Protos.CreateIssueRequest;
using IssueContent = Issues.API.Protos.IssueContent;

namespace WebBff.Api.Services.Issues.Issues
{
    public class GrpcIssueService : IIssueService
    {
        private readonly IssueService.IssueServiceClient _client;

        public GrpcIssueService(IssueService.IssueServiceClient client)
        {
            _client = client;
        }
        public async Task<string> CreateIssueAsync(Models.Issuses.Issues.CreateIssueRequest request)
        {
            var res = await _client.CreateIssueAsync(new CreateIssueRequest() {GroupId = request.GroupId, Name = request.Name, TypeOfIssueId = request.TypeOfIssueId, TextContent = request.TextContent});
            return res.Id;
        }

        public async Task DeleteIssueAsync(string id)
        {
            await _client.DeleteIssueAsync(new DeleteIssueRequest() {Id = id});
        }

        public async Task<IEnumerable<IssueDto>> GetIssuesForGroupAsync(string groupId)
        {
            var res = await _client.GetIssuesForGroupAsync(new GetIssuesForGroupRequest() {GroupId = groupId});
            return res.Issues.Select(MapToIssue);
        }

        public async Task<IEnumerable<IssueDto>> GetIssuesForUserAsync(string userId)
        {
            var res = await _client.GetIssuesForUserAsync(new GetIssuesForUserRequest() {UserId = userId});
            return res.Issues.Select(MapToIssue);
        }

        public async Task RenameIssueAsync(string issueId, string newName)
        {
            await _client.RenameIssueAsync(new RenameIssueRequest() {Id = issueId, NewName = newName});
        }

        public async Task<IssueWithContent> GetIssueWithContentAsync(string issueId)
        {
            var res = await _client.GetIssueWithContentAsync(new GetIssueWithContentRequest());
            return MapToIssueWithContent(res.Issue, res.Content);
        }

        public async Task UpdateIssueContentAsync(string textContent, string issueId)
        {
            await _client.UpdateIssueContentAsync(new UpdateIssueContentRequest() {IssueId = issueId, TextContent = textContent});
        }

        private IssueDto MapToIssue(IssueReference reference)
        {
            return new IssueDto()
            {
                CreatingUserId = reference.CreatingUserId,
                Id = reference.Id,
                Name = reference.Name,
                StatusId = reference.StatusId,
                TimeOfCreation = reference.TimeOfCreation.ToDateTimeOffset(),
                TypeOfIssueId = reference.TypeOfIssueId,
            };
        }

        private IssueWithContent MapToIssueWithContent(IssueReference reference, IssueContent content)
        {
            return new IssueWithContent()
            {
                Issue = new IssueDto()
                {
                    CreatingUserId = reference.CreatingUserId,
                    Id = reference.Id,
                    Name = reference.Name,
                    StatusId = reference.StatusId,
                    TimeOfCreation = reference.TimeOfCreation.ToDateTimeOffset(),
                    TypeOfIssueId = reference.TypeOfIssueId,
                },
                TextContent = content.TextContent
            };
        }
    }
}