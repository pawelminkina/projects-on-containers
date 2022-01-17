using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Issues.AcceptanceTests.Base;
using Issues.API.Infrastructure.Factories;
using Issues.API.Protos;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

namespace Issues.AcceptanceTests.Services
{
    public class StatusFlowServiceTests : IssuesTestServer
    {
        private StatusFlowService.StatusFlowServiceClient _grpcClient;
        private TestServer _server;

        [SetUp]
        public void Setup()
        {
            _server = CreateServer();
            var channel = GetGrpcChannel(_server);
            _grpcClient = new StatusFlowService.StatusFlowServiceClient(channel);
        }

        [Test]
        public async Task ShouldReturnStatusFlows()
        {
            //GIVEN expected status flows
            var expected = GetExpectedFlows();

            //WHEN status flows are retrieved from server
            var getRequest = new GetStatusFlowsRequest();
            var getResponse = await _grpcClient.GetStatusFlowsAsync(getRequest);

            //THEN check equality of actual and expected items
            getResponse.Flow.Should().BeEquivalentTo(expected);

            #region Local methods

            IEnumerable<StatusFlow> GetExpectedFlows() => new[]
            {
                GrpcStatusFlowFactory.Create("007-001", "Status Flow 1", new []
                {
                    GrpcStatusInFlowFactory.Create("004-001", 0, new List<ConnectedStatuses>()
                    {
                        new ConnectedStatuses() {ConnectedStatusId = "004-002", ParentStatusId = "004-001", DirectionOfStatus = ConnectedStatuses.Types.Direction.Out},
                        new ConnectedStatuses() {ConnectedStatusId = "004-002", ParentStatusId = "004-001", DirectionOfStatus = ConnectedStatuses.Types.Direction.In},
                        new ConnectedStatuses() {ConnectedStatusId = "004-003", ParentStatusId = "004-001", DirectionOfStatus = ConnectedStatuses.Types.Direction.In},
                    }),
                    GrpcStatusInFlowFactory.Create("004-002", 1, new List<ConnectedStatuses>()
                    {
                        new ConnectedStatuses() {ConnectedStatusId = "004-001", ParentStatusId = "004-002", DirectionOfStatus = ConnectedStatuses.Types.Direction.Out},
                        new ConnectedStatuses() {ConnectedStatusId = "004-003", ParentStatusId = "004-002", DirectionOfStatus = ConnectedStatuses.Types.Direction.Out},
                        new ConnectedStatuses() {ConnectedStatusId = "004-001", ParentStatusId = "004-002", DirectionOfStatus = ConnectedStatuses.Types.Direction.In},
                    }),
                    GrpcStatusInFlowFactory.Create("004-003", 2, new List<ConnectedStatuses>()
                    {
                        new ConnectedStatuses() {ConnectedStatusId = "004-001", ParentStatusId = "004-003", DirectionOfStatus = ConnectedStatuses.Types.Direction.In},
                        new ConnectedStatuses() {ConnectedStatusId = "004-002", ParentStatusId = "004-003", DirectionOfStatus = ConnectedStatuses.Types.Direction.In},
                        new ConnectedStatuses() {ConnectedStatusId = "004-001", ParentStatusId = "004-003", DirectionOfStatus = ConnectedStatuses.Types.Direction.Out},

                    }),
                }),
                GrpcStatusFlowFactory.Create("007-002", "Status Flow 2", null),
            };

            #endregion
        }

        [Test]
        public async Task ShouldReturnStatusFlow()
        {
            //GIVEN expected status flow

            //WHEN flow is retrieved from server

            //THEN check equality of expected and actual item

            Assert.Fail();
        }

        [Test]
        public async Task ShouldCreateStatusFlow()
        {
            //GIVEN expected status flow to create

            //WHEN flow is created

            //AND id of created flow is assigned to expected flow

            //AND flow is retrieved from server

            //THEN check equality of expected and actual item

            Assert.Fail();

        }

        [Test]
        public async Task ShouldRemoveStatusFlow()
        {
            //GIVEN status flow to remove

            //WHEN status flow is removed

            //AND is retrieved from server

            //THEN returned status flow should be null
            Assert.Fail();

            //TODO should he? Shouldn't he be set to isDeleted or smth?
        }

        public async Task ShouldRenameStatusFlow()
        {
            //GIVEN status flow to rename

            //AND new name for status flow

            //WHEN status is renamed

            //AND retrieved from server

            Assert.Fail();
            //THEN check equality of actual and expected status flow name
        }

        //TODO I think it should be another service for this methods

        [Test]
        public async Task ShouldAddStatusToFlow()
        {
            //GIVEN status to add

            //AND status flow in which status will be added

            //WHEN status is added to flow

            //AND flow is retrieved from server
            Assert.Fail();

            //THEN check does flow contain added status
        }

        [Test]
        public async Task ShouldRemoveStatusFromFlow()
        {
            //GIVEN status to remove

            //AND status flow from status will be removed

            //WHEN status is removed from flow

            //AND flow is retrieved from server
            Assert.Fail();

            //THEN check does flow contain removed status
        }

        [Test]
        public async Task ShouldAddConnectionToStatusInFlow()
        {
            //GIVEN connection in flow to add

            //WHEN connection is added to flow

            //AND flow is retrieved from server
            Assert.Fail();

            //THEN check does flow contain added connection
        }

        [Test]
        public async Task ShouldRemoveconnectionFromStatusInFlow()
        {
            //GIVEN connection in flow to remove

            //WHEN connection is removed from flow

            //AND flow is retrieved from server
            Assert.Fail();

            //THEN check does flow contain removed connection
        }
    }
}
