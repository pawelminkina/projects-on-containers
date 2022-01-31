using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Google.Protobuf;
using Grpc.Core;
using Issues.AcceptanceTests.Base;
using Issues.API.Protos;
using Issues.Application.Common.Exceptions;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Issues.AcceptanceTests.Services
{
    public class TypeOfGroupOfIssueServiceTests : IssuesTestServer
    {
        private TypeOfGroupOfIssueService.TypeOfGroupOfIssueServiceClient _grpcClient;
        private TestServer _server;

        [SetUp]
        public void Setup()
        {
            _server = CreateServer();
            var channel = GetGrpcChannel(_server);
            _grpcClient = new TypeOfGroupOfIssueService.TypeOfGroupOfIssueServiceClient(channel);
        }

        #region Get All

        [Test]
        public async Task ShouldReturnTypesOfGroupsOfIssues()
        {
            //GIVEN expected collection of types of groups
            var expectedTypesOfGroupOfIssues = GetExpectedTypesOfGroupOfIssues();

            //WHEN types are retrieved from server
            var request = new GetTypesOfGroupsOfIssuesRequest();
            var response = await _grpcClient.GetTypesOfGroupsOfIssuesAsync(request);
            var actual = response.TypesOfGroups;

            //THEN check equality of expected and actual items
            actual.Should().BeEquivalentTo(expectedTypesOfGroupOfIssues);

            #region Local methods

            IEnumerable<TypeOfGroupOfIssues> GetExpectedTypesOfGroupOfIssues() => new[]
            {
                new TypeOfGroupOfIssues() {Id = "001-001", Name = "Type Of Group Of Issues 1"},
                new TypeOfGroupOfIssues() {Id = "001-002", Name = "Type Of Group Of Issues 2"},
                new TypeOfGroupOfIssues() {Id = "001-003", Name = "Type Of Group Of Issues 3"},
            };

            #endregion
        }

        #endregion

        #region Get Single

        [Test]
        public async Task ShouldReturnExpectedTypeOfGroupOfIssues()
        {
            //GIVEN expected type of group of issues
            var expected = GetExpectedTypeOfGroupOfIssues();

            //WHEN item is retrieved from server
            var request = new GetTypeOfGroupOfIssuesRequest() { Id = expected.Id };
            var response = await _grpcClient.GetTypeOfGroupOfIssuesAsync(request);
            var actual = response.TypeOfGroup;

            //THEN check equality of expected and actual item
            actual.Should().BeEquivalentTo(expected);

            #region Local methods

            TypeOfGroupOfIssues GetExpectedTypeOfGroupOfIssues() =>
                new() { Id = "001-002", Name = "Type Of Group Of Issues 2" };

            #endregion
        }

        #endregion

        #region Create

        [Test]
        public async Task ShouldCreateTypeOfGroupOfIssues()
        {
            //GIVEN expected type of group of issues
            var expectedToBeCreated = GetExpectedTypeOfGroupOfIssuesToBeCreated();

            //WHEN item is created
            var createRequest = new CreateTypeOfGroupOfIssuesRequest() { Name = expectedToBeCreated.Name };
            var createResponse = await _grpcClient.CreateTypeOfGroupOfIssuesAsync(createRequest);

            //AND retrieved from server
            var getRequest = new GetTypeOfGroupOfIssuesRequest() { Id = createResponse.Id };
            var getResponse = await _grpcClient.GetTypeOfGroupOfIssuesAsync(getRequest);
            var actual = getResponse.TypeOfGroup;

            //THEN assign id of created item to expected
            expectedToBeCreated.Id = createResponse.Id;

            //AND check equality of expected and actual item
            actual.Should().BeEquivalentTo(expectedToBeCreated);

            #region Local methods

            TypeOfGroupOfIssues GetExpectedTypeOfGroupOfIssuesToBeCreated() =>
                new() { Name = "Type to create" };

            #endregion
        }

        [Test]
        public void ShouldNotCreateTypeOfGroupOfIssuesBecauseAlreadyExistOneWithGivenName()
        {
            //GIVEN expected type of group of issues
            var expectedToBeCreated = GetExpectedTypeOfGroupOfIssuesToBeCreated();

            //WHEN item is created
            var createRequest = new CreateTypeOfGroupOfIssuesRequest() { Name = expectedToBeCreated.Name };
            var exception = Assert.ThrowsAsync<RpcException>(async () => await _grpcClient.CreateTypeOfGroupOfIssuesAsync(createRequest));

            //THEN check errors status code
            exception.Status.StatusCode.Should().Be(StatusCode.AlreadyExists);

            //AND message
            var expectedErrorMessage = Domain.GroupsOfIssues.TypeOfGroupOfIssues.ErrorMessages.SomeTypeOfGroupAlreadyExistWithName(expectedToBeCreated.Name);
            exception.Status.Detail.Should().Be(expectedErrorMessage);

            #region Local methods

            TypeOfGroupOfIssues GetExpectedTypeOfGroupOfIssuesToBeCreated() =>
                new() { Name = "Type Of Group Of Issues 1" };

            #endregion
        }

        #endregion

        #region Rename

        [Test]
        public async Task ShouldRenameTypeOfGroupOfIssues()
        {
            //GIVEN type of group of issues to change
            var idToChange = "001-002";

            //AND new name for this type
            var expectedName = "New name";

            //WHEN items name is changed
            var changeRequest = new RenameTypeOfGroupOfIssuesRequest() { Id = idToChange, NewName = expectedName };
            var changeResponse = await _grpcClient.RenameTypeOfGroupOfIssuesAsync(changeRequest);

            //AND retrieved from server
            var getRequest = new GetTypeOfGroupOfIssuesRequest() { Id = idToChange };
            var getResponse = await _grpcClient.GetTypeOfGroupOfIssuesAsync(getRequest);
            var actualName = getResponse.TypeOfGroup.Name;

            //THEN check that item has new name
            actualName.Should().Be(expectedName);
        }

        [Test]
        public void ShouldNotRenameTypeOfGroupOfIssuesBecauseAlreadyExistOneWithGivenName()
        {
            //GIVEN type of group of issues to change
            var idToChange = "001-002";

            //AND new name for this type
            var expectedName = "Type Of Group Of Issues 1";

            //WHEN items name is changed
            var changeRequest = new RenameTypeOfGroupOfIssuesRequest() { Id = idToChange, NewName = expectedName };
            var exception = Assert.ThrowsAsync<RpcException>(async () => await _grpcClient.RenameTypeOfGroupOfIssuesAsync(changeRequest));

            //THEN check errors status code
            exception.Status.StatusCode.Should().Be(StatusCode.AlreadyExists);

            //AND message
            var expectedErrorMessage = Domain.GroupsOfIssues.TypeOfGroupOfIssues.ErrorMessages.SomeTypeOfGroupAlreadyExistWithName(expectedName);
            exception.Status.Detail.Should().Be(expectedErrorMessage);

        }

        #endregion

        #region Delete

        [Test]
        public async Task ShouldDeleteTypeOfGroupOfIssuesWithoutAssignedGroupOfIssues()
        {
            //GIVEN type of group of issues to delete
            var idToDelete = "001-003";

            //WHEN item is deleted
            var deleteRequest = new DeleteTypeOfGroupOfIssuesRequest() { Id = idToDelete };
            var deleteResponse = await _grpcClient.DeleteTypeOfGroupOfIssuesAsync(deleteRequest);

            //AND retrieved from server
            var getRequest = new GetTypeOfGroupOfIssuesRequest() { Id = idToDelete };
            var exception = Assert.ThrowsAsync<RpcException>(async () => await _grpcClient.GetTypeOfGroupOfIssuesAsync(getRequest));

            //THEN check errors status code
            exception.Status.StatusCode.Should().Be(StatusCode.NotFound);

            //AND message
            exception.Status.Detail.Should().Be(NotFoundException.RequestedResourceWithIdWasNotFound(idToDelete).Message);
        }

        #endregion
    }
}
