using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Issues.API.Infrastructure.Factories;
using Issues.API.Protos;
using Issues.Tests.Core.Base;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

namespace Issues.Tests.Acceptance.Services
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

        #region Get all

        [Test]
        public async Task ShouldReturnStatusFlows()
        {
            //GIVEN expected status flows
            var expected = GetExpectedFlows();

            //WHEN status flows are retrieved from server
            var getRequest = new GetStatusFlowsRequest();
            var getResponse = await _grpcClient.GetStatusFlowsAsync(getRequest);

            //THEN check equality of actual and expected items
            getResponse.Flows.Should().BeEquivalentTo(expected);

            #region Local methods

            IEnumerable<StatusFlow> GetExpectedFlows() => new[]
            {
                GetStatusFlowWithId004001(),
                GetStatusFlowWithId004002(),
                GetStatusFlowWithId004003(),
                GetStatusFlowWithId004004(),
            };

            #endregion
        }

        #endregion

        #region Get Single

        [Test]
        public async Task ShouldReturnStatusFlow()
        {
            //GIVEN expected status flow
            var expected = GetExpectedStatusFlow();

            //WHEN flow is retrieved from server
            var getRequest = new GetStatusFlowRequest() {Id = expected.Id};
            var getResponse = await _grpcClient.GetStatusFlowAsync(getRequest);

            //THEN check equality of expected and actual item
            getResponse.Flow.Should().BeEquivalentTo(expected);

            #region Local methods

            StatusFlow GetExpectedStatusFlow() => GetStatusFlowWithId004002(); 

            #endregion
        }

        [Test]
        public async Task ShouldReturnStatusFlowForGroupOfIssues()
        {
            //GIVEN group of issues
            var groupOfIssuesId = "002-002";

            //AND expected status flow
            var expected = GetExpectedStatusFlow();

            //WHEN flow is retrieved from server
            var getRequest = new GetStatusFlowForGroupOfIssuesRequest() { GroupOfIssuesId = groupOfIssuesId};
            var getResponse = await _grpcClient.GetStatusFlowForGroupOfIssuesAsync(getRequest);

            //THEN check equality of expected and actual item
            getResponse.Flow.Should().BeEquivalentTo(expected);

            #region Local methods

            StatusFlow GetExpectedStatusFlow() => GetStatusFlowWithId004002();

            #endregion
        }

        #endregion

        #region Add Status

        [Test]
        public async Task ShouldAddStatusToFlow()
        {
            //GIVEN status to add
            var expectedName = "New status";

            //AND status flow in which status will be added
            var statusFlow = "004-002";

            //WHEN status is added to flow
            var addStatusRequest = new AddStatusToFlowRequest() { FlowId = statusFlow, StatusName = expectedName };
            var addStatusResponse = await _grpcClient.AddStatusToFlowAsync(addStatusRequest);

            //AND flow is retrieved from server
            var getRequest = new GetStatusFlowRequest() { Id = statusFlow };
            var getResponse = await _grpcClient.GetStatusFlowAsync(getRequest);

            //THEN check does flow contain added status
            getResponse.Flow.Statuses.Should().HaveCount(3);
            getResponse.Flow.Statuses.Should().Contain(s => s.Name == expectedName);
        }

        #endregion

        #region Delete status

        [Test]
        public async Task ShouldDeleteStatusFromFlowAndAllHisConnections()
        {
            //GIVEN status to delete
            var statusToDelete = "005-004";

            //AND status flow in which status will be deleted
            var statusFlow = "004-002";

            //WHEN status is removed from flow
            var deleteStatusRequest = new DeleteStatusFromFlowRequest() { StatusInFlowId = statusToDelete };
            var deleteStatusResponse = await _grpcClient.DeleteStatusFromFlowAsync(deleteStatusRequest);

            //AND flow is retrieved from server
            var getRequest = new GetStatusFlowRequest() { Id = statusFlow };
            var getResponse = await _grpcClient.GetStatusFlowAsync(getRequest);

            //THEN check does flow contain removed status
            getResponse.Flow.Statuses.Should().HaveCount(1).And.Contain(s => s.Name == "To do");

            //AND that none of statuses contain connection to delete status
            getResponse.Flow.Statuses.Should().NotContain(s => s.ConnectedStatusesId.Any(d => d == statusToDelete));
        }

        #endregion

        #region Add Connection

        [Test]
        public async Task ShouldAddConnectionToStatusInFlow()
        {
            //GIVEN parent status which is connecting
            var parentStatus = "005-011";

            //AND connected status to which connection is going
            var connectedStatus = "005-006";

            //AND flow in which connection will be added
            var statusFlow = "004-003";

            //WHEN connection is added to flow
            var addConnectionRequest = new AddConnectionToStatusInFlowRequest() { ParentStatusinFlowId = parentStatus, ConnectedStatusInFlowId = connectedStatus };
            var addConnectionResponse = await _grpcClient.AddConnectionToStatusInFlowAsync(addConnectionRequest);

            //AND flow is retrieved from server
            var getRequest = new GetStatusFlowRequest() { Id = statusFlow };
            var getResponse = await _grpcClient.GetStatusFlowAsync(getRequest);

            //AND status on which add connection operation was performed is retrieved from flow
            var actualStatus = getResponse.Flow.Statuses.FirstOrDefault(s => s.Id == parentStatus);

            //THEN check does flow contain added connection
            actualStatus.Should().NotBeNull();
            actualStatus.ConnectedStatusesId.Should().HaveCount(1);
            actualStatus.ConnectedStatusesId.First().Should().Be(connectedStatus);
        }

        #endregion

        #region Remove Connection

        [Test]
        public async Task ShouldRemoveConnectionFromStatusInFlow()
        {
            //GIVEN parent status which is connecting
            var parentStatus = "005-005";

            //AND connected status to which connection is going
            var connectedStatus = "005-006";

            //AND flow in which connection will be removed
            var statusFlow = "004-003";

            //WHEN connection is removed from flow
            var removeConnectionRequest = new RemoveConnectionFromStatusInFlowRequest() { ConnectedStatusInFlowId = connectedStatus, ParentStatusinFlowId = parentStatus };
            var removeConnectionResponse = await _grpcClient.RemoveConnectionFromStatusInFlowAsync(removeConnectionRequest);

            //AND flow is retrieved from server
            var getRequest = new GetStatusFlowRequest() { Id = statusFlow };
            var getResponse = await _grpcClient.GetStatusFlowAsync(getRequest);

            //AND status on which remove connection operation was performed is retrieved from flow
            var actualStatus = getResponse.Flow.Statuses.FirstOrDefault(d => d.Id == parentStatus);

            //THEN check does flow contain removed connection
            actualStatus.ConnectedStatusesId.Should().BeEmpty();
        }

        #endregion

        #region Change Status to default

        [Test]
        public async Task ShouldChangeStatusOfFlowToDefault()
        {
            //GIVEN status to be set as default
            var statusToChange = "005-004";

            //AND status flow in which status will be changed
            var statusFlow = "004-002";

            //WHEN status is changed
            var changeDefaultStatusRequest = new ChangeDefaultStatusInFlowRequest() { NewDefaultStatusInFlowId = statusToChange };
            var changeDefaultStatusResponse = await _grpcClient.ChangeDefaultStatusInFlowAsync(changeDefaultStatusRequest);

            //AND flow with statuses is retrieved from server
            var getRequest = new GetStatusFlowRequest() { Id = statusFlow };
            var getResponse = await _grpcClient.GetStatusFlowAsync(getRequest);
            var newDefaultStatus = getResponse.Flow.Statuses.FirstOrDefault(s => s.Id == statusToChange);
            var oldDefaultStatus = getResponse.Flow.Statuses.FirstOrDefault(s => s.Id != statusToChange);

            //THEN check does flow has default status as expected
            getResponse.Flow.Statuses.Should().HaveCount(2);
            newDefaultStatus.IsDefault.Should().BeTrue();
            oldDefaultStatus.IsDefault.Should().BeFalse();
        }

        #endregion

        #region Data from csv
        private StatusFlow GetStatusFlowWithId004001() =>
            GrpcStatusFlowFactory.Create("004-001", "Status Flow 1", new List<StatusInFlow>()
            {
                GrpcStatusInFlowFactory.Create("005-001", "To do", new List<string>()
                {
                    "005-002"
                }, true),
                GrpcStatusInFlowFactory.Create("005-002", "Done", new List<string>()
                {
                    "005-001"
                })
            });
        private StatusFlow GetStatusFlowWithId004002() =>
            GrpcStatusFlowFactory.Create("004-002", "Status Flow 2", new List<StatusInFlow>()
            {
                GrpcStatusInFlowFactory.Create("005-003", "To do", new List<string>()
                {
                    "005-004"
                }, true),
                GrpcStatusInFlowFactory.Create("005-004", "Done", new List<string>()
                {
                    "005-003"
                })
            });
        private StatusFlow GetStatusFlowWithId004003() =>
            GrpcStatusFlowFactory.Create("004-003", "Status Flow 3", new List<StatusInFlow>()
            {
                GrpcStatusInFlowFactory.Create("005-005", "To do", new List<string>()
                {
                    "005-006"
                }, true),
                GrpcStatusInFlowFactory.Create("005-006", "Done", new List<string>()
                {
                    "005-005"
                }),
                GrpcStatusInFlowFactory.Create("005-011", "Some status", new List<string>())
            });
        private StatusFlow GetStatusFlowWithId004004() =>
            GrpcStatusFlowFactory.Create("004-004", "Status Flow 4", new List<StatusInFlow>()
            {
                GrpcStatusInFlowFactory.Create("005-007", "To do", new List<string>()
                {
                    "005-008"
                }, true),
                GrpcStatusInFlowFactory.Create("005-008", "Done", new List<string>()
                {
                    "005-007"
                })
            }, true);


        #endregion
    }
}
