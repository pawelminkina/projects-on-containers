using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Issues.API.Protos;
using WebBff.Api.Models.Issuses.Issues;
using CreateIssueRequest = Issues.API.Protos.CreateIssueRequest;

namespace WebBff.Api.Services.Issues.Issues
{
    public class GrpcIssueService : IIssueService
    {
        private readonly IssueService.IssueServiceClient _client;

        public GrpcIssueService(IssueService.IssueServiceClient client)
        {
            _client = client;
        }
        public async Task<string> CreateIssueAsync(IssueDto issue)
        {
            var res = await _client.CreateIssueAsync(new CreateIssueRequest());
            return string.Empty;
        }

        public async Task DeleteIssueAsync(string id)
        {
            throw new InvalidOperationException();
        }

        public async Task<IEnumerable<IssueDto>> GetIssuesForGroupAsync(string groupId)
        {
            var res = await _client.GetIssuesForGroupAsync(new GetIssuesForGroupRequest());
            return null;
        }

        public async Task<IEnumerable<IssueDto>> GetIssuesForUserAsync(string userId)
        {
            var res = await _client.GetIssuesForUserAsync(new GetIssuesForUserRequest());
            return null;
        }

        public async Task RenameIssueAsync(string issueId, string newName)
        {
            throw new System.NotImplementedException();
        }

        public async Task RenameIssueAsync(string userId)
        {
            var res = await _client.RenameIssueAsync(new RenameIssueRequest());
        }

        public async Task<IssueWithContent> GetIssueWithContentAsync(string issueId)
        {
            var res = await _client.GetIssueWithContentAsync(new GetIssueWithContentRequest());
            return null;
        }

        public async Task UpdateIssueContentAsync(string textContent, string issueId)
        {
            var res = await _client.UpdateIssueContentAsync(new UpdateIssueContentRequest());
        }

        public async Task UpdateIssueStatusAsync(string issueId, string statusId)
        {
            var res = await _client.UpdateIssueStatusAsync(new UpdateIssueStatusRequest());
        }
    }
}