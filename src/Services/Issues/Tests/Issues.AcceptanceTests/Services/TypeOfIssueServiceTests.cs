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
    public class TypeOfIssueServiceTests : IssuesTestServer
    {
        private TypeOfIssueService.TypeOfIssueServiceClient _grpcClient;
        private TestServer _server;

        [SetUp]
        public void Setup()
        {
            _server = CreateServer();
            var channel = GetGrpcChannel(_server);
            _grpcClient = new TypeOfIssueService.TypeOfIssueServiceClient(channel);
        }

        [Test]
        public async Task ShouldReturnTypesOfIssues()
        {
            //GIVEN expected types of issues
            var expected = GetExpectedTypes();

            //WHEN types are retrieved from server
            var getRequest = new GetTypesOfIssuesRequest();
            var getResponse = await _grpcClient.GetTypesOfIssuesAsync(getRequest);

            //THEN check equality of actual and expected items
            getResponse.Types_.Should().BeEquivalentTo(expected);

            #region Local methods

            IEnumerable<TypeOfIssues> GetExpectedTypes() => new[]
            {
                new TypeOfIssues(){Id = "003-001", Name = "Type Of Issues 1"},
                new TypeOfIssues(){Id = "003-002", Name = "Type Of Issues 2"},
                new TypeOfIssues(){Id = "003-003", Name = "Type Of Issues 3"}
            };

            #endregion
        }

        [Test]
        public async Task ShouldReturnTypeOfIssues()
        {
            //GIVEN expected type of issues
            var expected = GetExpectedType();

            //WHEN type is retrieved from server
            var getRequest = new GetTypeOfIssueRequest() {Id = expected.Id};
            var getResponse = await _grpcClient.GetTypeOfIssueAsync(getRequest);

            //THEN check equality of actual and expected item 
            getResponse.Type.Should().BeEquivalentTo(expected);

            #region Local methods

            TypeOfIssues GetExpectedType() => new TypeOfIssues()
            {
                Id = "003-002",
                Name = "Type Of Issues 2"
            };

            #endregion
        }

        [Test]
        public async Task ShouldCreateTypeOfIssues()
        {
            //GIVEN expected type which will be created
            var expected = GetExpectedType();

            //WHEN type is created
            var createRequest = new CreateTypeOfIssueRequest() {Name = expected.Name};
            var createResponse = await _grpcClient.CreateTypeOfIssueAsync(createRequest);

            //AND id of created type is assigned to expected
            expected.Id = createResponse.Id;

            //AND created type is retrieved from server
            var getRequest = new GetTypeOfIssueRequest() { Id = expected.Id };
            var getResponse = await _grpcClient.GetTypeOfIssueAsync(getRequest);

            //THEN check equality of expected and actual type
            getResponse.Type.Should().BeEquivalentTo(expected);

            #region Local methods

            TypeOfIssues GetExpectedType() => new TypeOfIssues()
            {
                Name = "Type Of Issues 7"
            };

            #endregion
        }

        [Test]
        public async Task ShouldRenameTypeOfIssues()
        {
            //GIVEN type of issues to rename
            var typeId = "003-002";

            //AND new name for this type
            var newName = "new name for type of issues";

            //WHEN issue has been renamed
            var renameRequest = new RenameTypeOfIssueRequest() {Id = typeId, Name = newName};
            var renameResponse = await _grpcClient.RenameTypeOfIssueAsync(renameRequest);

            //AND issue is retrieved from server
            var getRequest = new GetTypeOfIssueRequest() { Id = typeId };
            var getResponse = await _grpcClient.GetTypeOfIssueAsync(getRequest);

            //THEN check that actual name is the same as expected
            getResponse.Type.Name.Should().Be(newName);
        }

        [Test]
        public async Task ShouldArchiveTypeOfIssues()
        {
            //GIVEN type of issue to archive
            var typeId = "003-002";

            //WHEN type is archived
            var archiveRequest = new ArchiveTypeOfIssueRequest() {Id = typeId};
            var archiveResponse = await _grpcClient.ArchiveTypeOfIssueAsync(archiveRequest);

            //AND retrieved from server
            var getRequest = new GetTypeOfIssueRequest() { Id = typeId };
            var getResponse = await _grpcClient.GetTypeOfIssueAsync(getRequest);

            //THEN check that type has archived property with value of true
            getResponse.Type.IsArchived.Should().BeTrue();
        }
    }
}
