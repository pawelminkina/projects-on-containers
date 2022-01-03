using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Issues.AcceptanceTests.Base;
using Issues.API.Protos;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

namespace Issues.AcceptanceTests.Services
{
    public class GroupOfIssuesServiceTests : IssuesTestServer
    {
        private GroupOfIssueService.GroupOfIssueServiceClient _grpcClient;
        private TestServer _server;

        [SetUp]
        public void Setup()
        {
            _server = CreateServer();
            var channel = GetGrpcChannel(_server);
            _grpcClient = new GroupOfIssueService.GroupOfIssueServiceClient(channel);
        }

        [Test]
        public async Task ShouldReturnGroupsOfIssues()
        {
            //GIVEN expected collection of groups
            var expectedGroupOfIssues = GetExpectedGroupOfIssues();

            //WHEN groups are retrieved from server
            var request = new GetGroupsOfIssuesRequest();
            var response = await _grpcClient.GetGroupsOfIssuesAsync(request);
            var actual = response.Groups;

            //THEN check equality of expected and actual items
            actual.Should().BeEquivalentTo(expectedGroupOfIssues);

            #region Local methods

            IEnumerable<GroupOfIssue> GetExpectedGroupOfIssues() => new[]
            {
                new GroupOfIssue() {Id = "002-001", IsArchived = false, Name = "Group Of Issues 1", ShortName = "GOF1", TypeOfGroupId = "001-001"},
                new GroupOfIssue() {Id = "002-002", IsArchived = false, Name = "Group Of Issues 2", ShortName = "GOF2", TypeOfGroupId = "001-001"},
                new GroupOfIssue() {Id = "002-003", IsArchived = false, Name = "Group Of Issues 3", ShortName = "GOF3", TypeOfGroupId = "001-002"},
            };

            #endregion
        }

        [Test]
        public async Task ShouldReturnExpectedGroupOfIssues()
        {
            //GIVEN expected group of issue
            var expected = GetExpectedGroupOfIssue();

            //WHEN group is retrieved from server
            var request = new GetGroupOfIssuesRequest() {Id = expected.Id};
            var response = await _grpcClient.GetGroupOfIssuesAsync(request);
            var actual = response.Group;

            //THEN check equality of expected and actual group
            actual.Should().BeEquivalentTo(expected);

            #region Local methods

            GroupOfIssue GetExpectedGroupOfIssue() =>
                new() { Id = "002-001", IsArchived = false, Name = "Group Of Issues 1", ShortName = "GOF1", TypeOfGroupId = "001-001" };

            #endregion
        }

        [Test]
        public async Task ShouldCreateGroupOfIssues()
        {
            //GIVEN expected group of issues
            var expected = GetExpectedGroupOfIssue();

            //WHEN group is created
            var createRequest = new CreateGroupOfIssuesRequest() {Name = expected.Name, ShortName = expected.ShortName, TypeOfGroupId = expected.TypeOfGroupId};
            var createResponse = await _grpcClient.CreateGroupOfIssuesAsync(createRequest);

            //AND retrieved from server
            var getRequest = new GetGroupOfIssuesRequest() {Id = createResponse.Id};
            var getResponse = await _grpcClient.GetGroupOfIssuesAsync(getRequest);
            var actual = getResponse.Group;

            //THEN assign id from get to expected
            expected.Id = actual.Id;

            //check equality of expected and actual group
            actual.Should().BeEquivalentTo(expected);

            #region Local methods

            GroupOfIssue GetExpectedGroupOfIssue() =>
                new() { IsArchived = false, Name = "Expected group of issue", ShortName = "EXCP", TypeOfGroupId = "001-001" };

            #endregion
        }

        [Test]
        public async Task ShouldRenameGroupOfIssues()
        {
            //GIVEN group of issues to change
            var idToChange = "002-001";

            //AND new name for this group
            var expectedName = "New name";

            //WHEN items name is changed
            var changeRequest = new RenameGroupOfIssuesRequest() { Id = idToChange, NewName = expectedName };
            var changeResponse = await _grpcClient.RenameGroupOfIssuesAsync(changeRequest);

            //AND retrieved from server
            var getRequest = new GetGroupOfIssuesRequest() { Id = idToChange };
            var getResponse = await _grpcClient.GetGroupOfIssuesAsync(getRequest);
            var actualName = getResponse.Group.Name;

            //THEN check that item has new name
            actualName.Should().Be(expectedName);
        }

        [Test]
        public async Task ShouldArchiveGroupOfIssue()
        {
            //GIVEN group of issues to archive
            var idToArchive = "002-002";

            //WHEN item is archived
            var archiveRequest = new ArchiveGroupOfIssuesRequest() { Id = idToArchive };
            var archiveResponse = await _grpcClient.ArchiveGroupOfIssuesAsync(archiveRequest);

            //AND retrieved from server
            var getRequest = new GetGroupOfIssuesRequest() { Id = idToArchive };
            var getResponse = await _grpcClient.GetGroupOfIssuesAsync(getRequest);
            var actualArchiveStatus = getResponse.Group.IsArchived;

            //THEN check that item has been archived
            actualArchiveStatus.Should().BeTrue();
        }
    }
}
