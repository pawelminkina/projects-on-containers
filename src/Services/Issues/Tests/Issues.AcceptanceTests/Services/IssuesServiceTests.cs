using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Google.Protobuf.WellKnownTypes;
using Issues.AcceptanceTests.Base;
using Issues.API.Protos;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

namespace Issues.AcceptanceTests.Services
{
    public class IssuesServiceTests : IssuesTestServer
    {
        private IssueService.IssueServiceClient _grpcClient;
        private TestServer _server;

        [SetUp]
        public void Setup()
        {
            _server = CreateServer();
            var channel = GetGrpcChannel(_server);
            _grpcClient = new IssueService.IssueServiceClient(channel);
        }

        [Test]
        public async Task ShouldReturnIssuesForGroup()
        {
            //GIVEN group for which issues will be returned
            var groupId = "002-002";
            
            //AND expected issues
            var expected = GetExpectedIssues();

            //WHEN issues are retrieved from server
            var getRequest = new GetIssuesForGroupRequest() { GroupId = groupId};
            var getResponse = await _grpcClient.GetIssuesForGroupAsync(getRequest);

            //THEN check equality of actual and expected issues collection
            getResponse.Issues.Should().BeEquivalentTo(expected);

            #region Local methods

            IEnumerable<IssueReference> GetExpectedIssues() => new[]
            {
                new IssueReference() {Id = "005-003", Name = "Issue 3", CreatingUserId = "BaseUserId2", TimeOfCreation = new DateTimeOffset(new DateTime(2021,12,22), new TimeSpan(0,1,0,0)).ToTimestamp(), GroupId = "002-002"},
                new IssueReference() {Id = "005-004", Name = "Issue 4", CreatingUserId = "BaseUserId2", TimeOfCreation = new DateTimeOffset(new DateTime(2021,12,22), new TimeSpan(0,1,0,0)).ToTimestamp(), GroupId = "002-002"},
            };

            #endregion
        }

        [Test]
        public async Task ShouldReturnIssuesForUser()
        {
            //GIVEN user for which issues will be returned
            var userId = "BaseUserId2";

            //AND expected issues
            var expected = GetExpectedIssues();

            //WHEN issues are retrieved from server
            var getRequest = new GetIssuesForUserRequest() { UserId = userId };
            var getResponse = await _grpcClient.GetIssuesForUserAsync(getRequest);

            //THEN check equality of actual and expected issues collection
            getResponse.Issues.Should().BeEquivalentTo(expected);

            #region Local methods

            IEnumerable<IssueReference> GetExpectedIssues() => new[]
            {
                new IssueReference() {Id = "005-003", Name = "Issue 3", CreatingUserId = "BaseUserId2", TimeOfCreation = new DateTimeOffset(new DateTime(2021,12,22), new TimeSpan(0,1,0,0)).ToTimestamp(),  GroupId = "002-002"},
                new IssueReference() {Id = "005-004", Name = "Issue 4", CreatingUserId = "BaseUserId2", TimeOfCreation = new DateTimeOffset(new DateTime(2021,12,22), new TimeSpan(0,1,0,0)).ToTimestamp(),  GroupId = "002-002"},
            };

            #endregion
        }

        [Test]
        public async Task ShouldReturnIssueWithContent()
        {
            //GIVEN expected issue
            var expected = GetExpectedIssue();
            var expectedContent = GetExpectedContent();

            //WHEN issue is retrieved from server
            var getRequest = new GetIssueWithContentRequest() { IssueId = expected.Id };
            var getResponse = await _grpcClient.GetIssueWithContentAsync(getRequest);

            //THEN check equality of expected and actual issue
            getResponse.Issue.Should().BeEquivalentTo(expected);

            //AND issue content
            getResponse.Content.Should().BeEquivalentTo(expectedContent);

            #region Local methods

            IssueReference GetExpectedIssue() => new IssueReference()
            {
                Id = "005-003",
                Name = "Issue 3",
                CreatingUserId = "BaseUserId2",
                GroupId = "002-002",
                TimeOfCreation = new DateTimeOffset(new DateTime(2021, 12, 22), new TimeSpan(0, 1, 0, 0)).ToTimestamp()
            };

            IssueContent GetExpectedContent() => new IssueContent()
            {
                TextContent = "Issue 3 content"
            };

            #endregion
        }

        [Test]
        public async Task ShouldCreateIssue()
        {
            //GIVEN expected issue which will be created
            var expected = GetExpectedIssue();
            var expectedContent = GetExpectedContent();
            
            //WHEN issue is created
            var createRequest = new CreateIssueRequest()
            {
                GroupId = "002-002", Name = expected.Name, TextContent = expectedContent.TextContent,
            };
            var createResponse = await _grpcClient.CreateIssueAsync(createRequest);

            //AND id of created issue is assigned to expected
            expected.Id = createResponse.Id;

            //AND issue is retrieved from server
            var getRequest = new GetIssueWithContentRequest() { IssueId = expected.Id };
            var getResponse = await _grpcClient.GetIssueWithContentAsync(getRequest);

            //THEN check equality of expected and actual issue
            expected.TimeOfCreation = getResponse.Issue.TimeOfCreation;
            getResponse.Issue.Should().BeEquivalentTo(expected);

            //AND issue content
            getResponse.Content.Should().BeEquivalentTo(expectedContent);

            #region Local methods

            IssueReference GetExpectedIssue() => new IssueReference()
            {
                Name = "Issue 6",
                CreatingUserId = "BaseUserId",
                TimeOfCreation = new DateTimeOffset(new DateTime(2021, 12, 22), new TimeSpan(0, 1, 0, 0)).ToTimestamp(),
                GroupId = "002-002"
            };

            IssueContent GetExpectedContent() => new IssueContent()
            {
                TextContent = "Issue 6 content"
            };

            #endregion
        }

        [Test]
        public async Task ShouldRenameIssue()
        {
            //GIVEN issue which will be renamed
            var issueId = "005-003";

            //AND new name for this issue
            var newName = "new name for issue";

            //WHEN issue has been renamed
            var renameRequest = new RenameIssueRequest() {Id = issueId, NewName = newName};
            var renameResponse = await _grpcClient.RenameIssueAsync(renameRequest);
            
            //AND issue is retrieved from server
            var getRequest = new GetIssueWithContentRequest() { IssueId = issueId };
            var getResponse = await _grpcClient.GetIssueWithContentAsync(getRequest);

            //THEN check that actual name is the same as expected
            getResponse.Issue.Name.Should().Be(newName);
        }

        [Test]
        public async Task ShouldUpdateIssueTextContent()
        {
            //GIVEN issue to update
            var issueId = "005-003";

            //AND new text content for this issue
            var newContent = "new content for issue";

            //WHEN issue content is updated
            var updateTextContentRequest = new UpdateIssueTextContentRequest() {IssueId = issueId, TextContent = newContent};
            var updateTextContentResponse = await _grpcClient.UpdateIssueTextContentAsync(updateTextContentRequest);

            //AND issue is retrieved from server
            var getRequest = new GetIssueWithContentRequest() { IssueId = issueId };
            var getResponse = await _grpcClient.GetIssueWithContentAsync(getRequest);

            //THEN check that current text content is the same as expected
            getResponse.Content.TextContent.Should().Be(newContent);
        }

        [Test]
        public async Task ShouldSetIssueStatusToDeleted()
        {
            //GIVEN issue to delete
            var issueId = "005-003";

            //WHEN issue is deleted
            var deleteIssueRequest = new DeleteIssueRequest() {Id = issueId};
            var deleteIssueResponse = await _grpcClient.DeleteIssueAsync(deleteIssueRequest);

            //AND retrieved from server
            //AND issue is retrieved from server
            var getRequest = new GetIssueWithContentRequest() { IssueId = issueId };
            var getResponse = await _grpcClient.GetIssueWithContentAsync(getRequest);

            //THEN check that issue has isDeleted flag set to true
            getResponse.Issue.IsDeleted.Should().BeTrue();
        }
    }
}
