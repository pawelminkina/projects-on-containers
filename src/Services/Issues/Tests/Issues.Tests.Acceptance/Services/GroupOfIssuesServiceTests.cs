using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Issues.API.Protos;
using Issues.Tests.Core.Base;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

namespace Issues.Tests.Acceptance.Services
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

        #region Get All


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
                new GroupOfIssue() {Id = "002-001", Name = "Group Of Issues 1", ShortName = "GOF1", TypeOfGroupId = "001-001"},
                new GroupOfIssue() {Id = "002-002", Name = "Group Of Issues 2", ShortName = "GOF2", TypeOfGroupId = "001-001"},
                new GroupOfIssue() {Id = "002-003", Name = "Group Of Issues 3", ShortName = "GOF3", TypeOfGroupId = "001-002"},
                new GroupOfIssue() {Id = "002-004", Name = "Group Of Issues 4", ShortName = "GOF4", TypeOfGroupId = "001-002", TimeOfDelete = new DateTimeOffset(new DateTime(2020, 12, 22), new TimeSpan(0, 1, 0, 0)).ToTimestamp()},
            };

            #endregion
        }

        #endregion

        #region Get Single

        [Test]
        public async Task ShouldReturnExpectedGroupOfIssues()
        {
            //GIVEN expected group of issue
            var expected = GetExpectedGroupOfIssue();

            //WHEN group is retrieved from server
            var request = new GetGroupOfIssuesRequest() { Id = expected.Id };
            var response = await _grpcClient.GetGroupOfIssuesAsync(request);
            var actual = response.Group;

            //THEN check equality of expected and actual group
            actual.Should().BeEquivalentTo(expected);

            #region Local methods

            GroupOfIssue GetExpectedGroupOfIssue() =>
                new() { Id = "002-001", Name = "Group Of Issues 1", ShortName = "GOF1", TypeOfGroupId = "001-001" };

            #endregion
        }

        #endregion

        #region Create

        [Test]
        public async Task ShouldCreateGroupOfIssues()
        {
            //GIVEN expected group of issues
            var expected = GetExpectedGroupOfIssue();

            //WHEN group is created
            var createRequest = new CreateGroupOfIssuesRequest() { Name = expected.Name, ShortName = expected.ShortName, TypeOfGroupId = expected.TypeOfGroupId };
            var createResponse = await _grpcClient.CreateGroupOfIssuesAsync(createRequest);

            //AND retrieved from server
            var getRequest = new GetGroupOfIssuesRequest() { Id = createResponse.Id };
            var getResponse = await _grpcClient.GetGroupOfIssuesAsync(getRequest);
            var actual = getResponse.Group;

            //AND id of created is assigned to expected
            expected.Id = actual.Id;

            //THEN check equality of expected and actual group
            actual.Should().BeEquivalentTo(expected);

            #region Local methods

            GroupOfIssue GetExpectedGroupOfIssue() =>
                new() { Name = "Expected group of issue", ShortName = "EXP", TypeOfGroupId = "001-001" };

            #endregion
        }

        [Test]
        public void ShouldNotCreateGroupOfIssuesBecauseAlreadyExistOneWithGivenName()
        {
            //GIVEN expected group of issues
            var expectedToBeCreated = GetExpectedGroupOfIssue();

            //WHEN item is created
            var createRequest = new CreateGroupOfIssuesRequest() { Name = expectedToBeCreated.Name, ShortName = expectedToBeCreated.ShortName, TypeOfGroupId = expectedToBeCreated.TypeOfGroupId};
            var exception = Assert.ThrowsAsync<RpcException>(async () => await _grpcClient.CreateGroupOfIssuesAsync(createRequest));

            //THEN check errors status code
            exception.Status.StatusCode.Should().Be(StatusCode.AlreadyExists);

            //AND message
            var expectedErrorMessage = Domain.GroupsOfIssues.GroupOfIssues.ErrorMessages.SomeGroupAlreadyExistWithName(expectedToBeCreated.Name);
            exception.Status.Detail.Should().Be(expectedErrorMessage);

            #region Local methods

            GroupOfIssue GetExpectedGroupOfIssue() =>
                new() { Name = "Group Of Issues 1", ShortName = "EXP", TypeOfGroupId = "001-001" };

            #endregion
        }

        [Test]
        public void ShouldNotCreateGroupOfIssuesBecauseAlreadyExistOneWithGivenShortName()
        {
            //GIVEN expected group of issues
            var expectedToBeCreated = GetExpectedGroupOfIssue();

            //WHEN item is created
            var createRequest = new CreateGroupOfIssuesRequest() { Name = expectedToBeCreated.Name, ShortName = expectedToBeCreated.ShortName, TypeOfGroupId = expectedToBeCreated.TypeOfGroupId };
            var exception = Assert.ThrowsAsync<RpcException>(async () => await _grpcClient.CreateGroupOfIssuesAsync(createRequest));

            //THEN check errors status code
            exception.Status.StatusCode.Should().Be(StatusCode.AlreadyExists);

            //AND message
            var expectedErrorMessage = Domain.GroupsOfIssues.GroupOfIssues.ErrorMessages.SomeGroupAlreadyExistWithShortName(expectedToBeCreated.ShortName);
            exception.Status.Detail.Should().Be(expectedErrorMessage);

            #region Local methods

            GroupOfIssue GetExpectedGroupOfIssue() =>
                new() { Name = "Expected group of issue", ShortName = "GOF1", TypeOfGroupId = "001-001" };

            #endregion
        }

        #endregion

        #region Rename

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
        public void ShouldNotRenameGroupOfIssuesBecauseAlreadyExistOneWithGivenName()
        {
            //GIVEN group of issues to change
            var idToChange = "002-001";

            //AND new name for this group
            var expectedName = "Group Of Issues 2";

            //WHEN items name is changed
            var changeRequest = new RenameGroupOfIssuesRequest() { Id = idToChange, NewName = expectedName };
            var exception = Assert.ThrowsAsync<RpcException>(async () => await _grpcClient.RenameGroupOfIssuesAsync(changeRequest));

            //THEN check errors status code
            exception.Status.StatusCode.Should().Be(StatusCode.AlreadyExists);

            //AND message
            var expectedErrorMessage = Domain.GroupsOfIssues.GroupOfIssues.ErrorMessages.SomeGroupAlreadyExistWithName(expectedName);
            exception.Status.Detail.Should().Be(expectedErrorMessage);
        }

        #endregion

        #region Change Short Name

        [Test]
        public async Task ShouldChangeShortNameForGroupOfIssues()
        {
            //GIVEN group of issues to change
            var idToChange = "002-001";

            //AND new short name for this group
            var expectedShortName = "NSN";

            //WHEN items short name is changed
            var changeRequest = new ChangeShortNameForGroupOfIssuesRequest() { Id = idToChange, NewShortName= expectedShortName };
            var changeResponse = await _grpcClient.ChangeShortNameForGroupOfIssuesAsync(changeRequest);

            //AND retrieved from server
            var getRequest = new GetGroupOfIssuesRequest() { Id = idToChange };
            var getResponse = await _grpcClient.GetGroupOfIssuesAsync(getRequest);
            var actualName = getResponse.Group.ShortName;

            //THEN check that item has new name
            actualName.Should().Be(expectedShortName);
        }

        [Test]
        public void ShouldNotChangeShortNameOfGroupOfIssuesBecauseAlreadyExistOneWithGivenShortName()
        {
            //GIVEN group of issues to change
            var idToChange = "002-001";

            //AND new short name for this group
            var expectedShortName = "GOF2";

            //WHEN items short name is changed
            var changeRequest = new ChangeShortNameForGroupOfIssuesRequest() { Id = idToChange, NewShortName= expectedShortName };
            var exception = Assert.ThrowsAsync<RpcException>(async () => await _grpcClient.ChangeShortNameForGroupOfIssuesAsync(changeRequest));

            //THEN check errors status code
            exception.Status.StatusCode.Should().Be(StatusCode.AlreadyExists);

            //AND message
            var expectedErrorMessage = Domain.GroupsOfIssues.GroupOfIssues.ErrorMessages.SomeGroupAlreadyExistWithShortName(expectedShortName);
            exception.Status.Detail.Should().Be(expectedErrorMessage);
        }

        #endregion

        #region Delete

        [Test]
        public async Task ShouldMoveGroupOfIssuesToThrash()
        {
            //GIVEN group of issues to delete
            var idToDelete = "002-001";

            //WHEN item is deleted
            var deleteRequest = new DeleteGroupOfIssuesRequest() { Id = idToDelete};
            var deleteResponse = await _grpcClient.DeleteGroupOfIssuesAsync(deleteRequest);

            //AND retrieved from server
            var getRequest = new GetGroupOfIssuesRequest() { Id = idToDelete };
            var getResponse = await _grpcClient.GetGroupOfIssuesAsync(getRequest);
            var deletedGroup = getResponse.Group;

            //THEN check that item is in thrash
            deletedGroup.IsInThrash.Should().Be(true);

            //AND check that date of delete is today
            deletedGroup.TimeOfDelete.ToDateTime().Date.Should().Be(DateTime.Today.Date);
        }

        [Test]
        public async Task ShouldNotDeleteGroupOfIssuesBecauseItIsAlreadyDeleted()
        {
            //GIVEN group of issues to delete
            var idToDelete = "002-001";

            //WHEN item is deleted
            var deleteRequest = new DeleteGroupOfIssuesRequest() { Id = idToDelete };
            var deleteResponse = await _grpcClient.DeleteGroupOfIssuesAsync(deleteRequest);

            //AND deleted again
            var exception = Assert.ThrowsAsync<RpcException>(async () => await _grpcClient.DeleteGroupOfIssuesAsync(deleteRequest));

            //THEN check errors status code
            exception.Status.StatusCode.Should().Be(StatusCode.Internal);

            //AND message
            var expectedErrorMessage = Domain.GroupsOfIssues.TypeOfGroupOfIssues.ErrorMessages.CannotDeleteGroupWhichIsAlreadyDeleted(idToDelete);
            exception.Status.Detail.Should().Be(expectedErrorMessage);
        }

        #endregion
    }
}
