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
    public class StatusServiceTests : IssuesTestServer
    {
        private StatusService.StatusServiceClient _grpcClient;
        private TestServer _server;

        [SetUp]
        public void Setup()
        {
            _server = CreateServer();
            var channel = GetGrpcChannel(_server);
            _grpcClient = new StatusService.StatusServiceClient(channel);
        }

        [Test]
        public async Task ShouldReturnStatuses()
        {
            //GIVEN expected statuses
            var expected = GetExpectedStatuses();

            //WHEN statuses are retrieved from server
            var getRequest = new GetStatusesRequest();
            var getResponse = await _grpcClient.GetStatusesAsync(getRequest);

            //THEN check equality of actual and expected items
            getResponse.Statuses.Should().BeEquivalentTo(expected);

            #region Local methods

            IEnumerable<Status> GetExpectedStatuses() => new[]
            {
                new Status(){Id = "004-001", Name = "Status 1"},
                new Status(){Id = "004-002", Name = "Status 2"},
                new Status(){Id = "004-003", Name = "Status 3"},
            };

            #endregion
        }

        [Test]
        public async Task ShouldReturnStatus()
        {
            //GIVEN expected status
            var expected = GetExpectedStatus();

            //WHEN status is retrieved from server
            var getRequest = new GetStatusRequest() {Id = expected.Id};
            var getResponse = await _grpcClient.GetStatusAsync(getRequest);

            //THEN check equality of expected and actual item
            getResponse.Status.Should().BeEquivalentTo(expected);

            #region Local methods

            Status GetExpectedStatus() => new Status()
            {
                Id = "004-002",
                Name = "Status 2"
            };

            #endregion
        }

        [Test]
        public async Task ShouldCreateStatus()
        {
            //GIVEN expected status which will be created
            var expected = GetExpectedStatus();

            //WHEN status is created
            var createRequest = new CreateStatusRequest() {Name = expected.Name};
            var createResponse = await _grpcClient.CreateStatusAsync(createRequest);

            //AND id of created status is assigned to expected
            expected.Id = createResponse.Id;

            //AND status is retrieved from server
            var getRequest = new GetStatusRequest() { Id = expected.Id };
            var getResponse = await _grpcClient.GetStatusAsync(getRequest);

            //THEN check equality of expected and actual status
            getResponse.Status.Should().BeEquivalentTo(expected);

            #region Local methods

            Status GetExpectedStatus() => new Status()
            {
                Name = "Status 007"
            };

            #endregion
        }

        [Test]
        public async Task ShouldRenameStatus()
        {
            //GIVEN status which will be renamed
            var statusId = "004-003";

            //AND new name for status
            var newName = "this is a new name for status";

            //WHEN status is renamed
            var renameRequest = new RenameStatusRequest() {Id = statusId, Name = newName};
            var renameResponse = await _grpcClient.RenameStatusAsync(renameRequest);

            //AND is retrieved from server
            var getRequest = new GetStatusRequest() { Id = statusId };
            var getResponse = await _grpcClient.GetStatusAsync(getRequest);

            //THEN check equality of actual and expected status name
            getResponse.Status.Name.Should().Be(newName);
        }
    }
}
