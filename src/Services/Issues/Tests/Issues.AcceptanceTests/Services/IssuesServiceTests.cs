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
                new IssueReference() {Id = "005-003", IsArchived = false, Name = "Issue 3", StatusId = "004-002", CreatingUserId = "BaseUserId", TimeOfCreation = new DateTimeOffset(new DateTime(2021,12,22), new TimeSpan(0,1,0,0)).ToTimestamp(), TypeOfIssueId = "003-002"},
                new IssueReference() {Id = "005-004", IsArchived = false, Name = "Issue 4", StatusId = "004-002", CreatingUserId = "BaseUserId", TimeOfCreation = new DateTimeOffset(new DateTime(2021,12,22), new TimeSpan(0,1,0,0)).ToTimestamp(), TypeOfIssueId = "003-002"},
            };

            #endregion
        }

        [Test]
        public async Task ShouldReturnIssuesForUser()
        {
            //GIVEN user for which issues will be returned
            //AND expected issues
            //WHEN issues are retrieved from server
            //THEN check equality of actual and expected issues collection
        }

        [Test]
        public async Task ShouldReturnIssueWithContent()
        {
            //GIVEN expected issue
            //WHEN issue is retrieved from server
            //THEN check equality of expected and actual issue
        }

        [Test]
        public async Task ShouldCreateIssue()
        {
            //GIVEN expected issue which will be created
            //WHEN issue is created
            //AND retrieved from server
            //THEN assign id of created issue to expected
            //AND check equality of expected and actual issue
        }

        [Test]
        public async Task ShouldRenameIssue()
        {
            //GIVEN issue which will be renamed
            //AND new name for this issue
            //WHEN issue has been renamed
            //AND issue is retrieved from server
            //THEN check that actual name is the same as expected
        }

        [Test]
        public async Task ShouldUpdateIssueTextContent()
        {
            //GIVEN issue to update
            //AND new text content for this issue
            //WHEN issue content is updated
            //AND issue is retrieved from server
            //THEN check that current text content is the same as expected
        }

        [Test]
        public async Task ShouldSetIssueStatusToDeleted()
        {
            //GIVEN issue to delete
            //WHEN issue is deleted
            //AND retrieved from server
            //THEN check that issue has isDeleted flag set to true
        }
    }
}
