using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Google.Protobuf;
using Issues.AcceptanceTests.Base;
using Issues.API.Protos;
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

        [Test]
        public async Task ShouldReturnTypesOfGroupsOfIssues()
        {
            //GIVEN expected collection of types of groups
            var expectedTypesOfGroupOfIssues = GetExpectedTypesOfGroupOfIssues();
            
            //WHEN types are retrieved from server
            var request = new GetTypesOfGroupsOfIssuesRequest();
            var response = await _grpcClient.GetTypesOfGroupsOfIssuesAsync(request);
            var actual = response.Types_;

            //THEN check equality of expected and actual items
            actual.Should().BeEquivalentTo(expectedTypesOfGroupOfIssues);

            #region Local methods

            IEnumerable<TypeOfGroupOfIssues> GetExpectedTypesOfGroupOfIssues() => new[]
            {
                new TypeOfGroupOfIssues() {Id = "001-001", IsArchived = false, Name = "Type Of Group Of Issues 1"},
                new TypeOfGroupOfIssues() {Id = "001-002", IsArchived = false, Name = "Type Of Group Of Issues 2"},
            };

            #endregion
        }

        [Test]
        public async Task ShouldReturnExpectedTypeOfGroupOfIssues()
        {
            //GIVEN expected type of group of issues
            var expected = GetExpectedTypeOfGroupOfIssues();

            //WHEN item is retrieved from server
            var request = new GetTypeOfGroupOfIssuesRequest() {Id = expected.Id};
            var response = await _grpcClient.GetTypeOfGroupOfIssuesAsync(request);
            var actual = response.Type;

            //THEN check equality of expected and actual item
            actual.Should().BeEquivalentTo(expected);

            #region Local methods

            TypeOfGroupOfIssues GetExpectedTypeOfGroupOfIssues() => 
                new () {Id = "001-002", IsArchived = false, Name = "Type Of Group Of Issues 2"};
            
            #endregion
        }

        [Test]
        public async Task ShouldCreateTypeOfGroupOfIssues()
        {
            //GIVEN expected type of group of issues
            var expectedToBeCreated = GetExpectedTypeOfGroupOfIssuesToBeCreated();

            //WHEN item is created
            var createRequest = new CreateTypeOfGroupOfIssuesRequest() {Name = expectedToBeCreated.Name};
            var createResponse = await _grpcClient.CreateTypeOfGroupOfIssuesAsync(createRequest);
            
            //AND retrieved from server
            var getRequest = new GetTypeOfGroupOfIssuesRequest() {Id = createResponse.Id};
            var getResponse = await _grpcClient.GetTypeOfGroupOfIssuesAsync(getRequest);
            var actual = getResponse.Type;

            //THEN assign id of created item to expected
            expectedToBeCreated.Id = createResponse.Id;

            //AND check equality of expected and actual item
            actual.Should().BeEquivalentTo(expectedToBeCreated);

            #region Local methods

            TypeOfGroupOfIssues GetExpectedTypeOfGroupOfIssuesToBeCreated() =>
                new() { IsArchived = false, Name = "Type to create" };

            #endregion
        }

        [Test]
        public async Task ShouldRenameTypeOfGroupOfIssues()
        {
            //GIVEN type of group of issues to change
            var idToChange = "001-001";

            //AND new name for this type
            var expectedName = "New name";

            //WHEN items name is changed
            var changeRequest = new RenameTypeOfGroupOfIssuesRequest() {Id = idToChange, NewName = expectedName};
            var changeResponse = await _grpcClient.RenameTypeOfGroupOfIssuesAsync(changeRequest);

            //AND retrieved from server
            var getRequest = new GetTypeOfGroupOfIssuesRequest() { Id = idToChange };
            var getResponse = await _grpcClient.GetTypeOfGroupOfIssuesAsync(getRequest);
            var actualName = getResponse.Type.Name;

            //THEN check that item has new name
            actualName.Should().Be(expectedName);
        }

        [Test]
        public async Task ShouldChangeStatusOfTypeOfGroupOfIssuesToArchived()
        {
            //GIVEN type of group of issues to archive
            var idToArchive = "001-002";

            //WHEN item is archived
            var archiveRequest = new ArchiveTypeOfGroupOfIssuesRequest() { Id = idToArchive, TypeOfGroupOfIssuesWhereGroupsWillBeMovedId = "001-001"};
            var archiveResponse = await _grpcClient.ArchiveTypeOfGroupOfIssuesAsync(archiveRequest);

            //AND retrieved from server
            var getRequest = new GetTypeOfGroupOfIssuesRequest() { Id = idToArchive };
            var getResponse = await _grpcClient.GetTypeOfGroupOfIssuesAsync(getRequest);
            var actualArchiveStatus = getResponse.Type.IsArchived;
            
            //THEN check that item has been archived
            actualArchiveStatus.Should().BeTrue();
        }
    }
}
