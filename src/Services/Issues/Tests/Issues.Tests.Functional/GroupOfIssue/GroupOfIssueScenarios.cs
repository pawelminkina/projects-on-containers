using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Issues.API.Protos;
using Issues.Tests.Core.Base;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

namespace Issues.Tests.Functional.GroupOfIssue
{
    public class GroupOfIssueScenarios : IssuesTestServer
    {
        private GroupOfIssueService.GroupOfIssueServiceClient _grpcGroupOfIssueClient;
        private StatusFlowService.StatusFlowServiceClient _grpcStatusFlowClient;
        private TestServer _server;

        [SetUp]
        public void Setup()
        {
            _server = CreateServer();
            _grpcGroupOfIssueClient = new GroupOfIssueService.GroupOfIssueServiceClient(GetGrpcChannel(_server));
            _grpcStatusFlowClient = new StatusFlowService.StatusFlowServiceClient(GetGrpcChannel(_server));
        }

        [Test]
        public async Task ShouldStatusFlowBeCreatedForGroupOfIssuesWithDefaultStatuses()
        {
            //GIVEN expected group of issues
            var expectedGroupOfIssue = GetExpectedGroupOfIssue();

            //AND expected status flow
            var expectedStatusFlow = GetExpectedStatusFlow();

            //WHEN group is created
            var createRequest = new CreateGroupOfIssuesRequest() { Name = expectedGroupOfIssue.Name, ShortName = expectedGroupOfIssue.ShortName, TypeOfGroupId = expectedGroupOfIssue.TypeOfGroupId };
            var createResponse = await _grpcGroupOfIssueClient.CreateGroupOfIssuesAsync(createRequest);

            //AND retrieved from server
            var getGroupOfIssueRequest = new GetGroupOfIssuesRequest() { Id = createResponse.Id };
            var getGroupOfIssueResponse = await _grpcGroupOfIssueClient.GetGroupOfIssuesAsync(getGroupOfIssueRequest);
            var actualGroupOfIssue = getGroupOfIssueResponse.Group;

            //AND status flow is retrieved from server by group of issue id
            var getStatusFlowRequest = new GetStatusFlowForGroupOfIssuesRequest() { GroupOfIssuesId = actualGroupOfIssue.Id };
            var getStatusFlowResponse = await _grpcStatusFlowClient.GetStatusFlowForGroupOfIssuesAsync(getStatusFlowRequest);
            var actualStatusFlow = getStatusFlowResponse.Flow;
            var toDoStatus = actualStatusFlow.Statuses.FirstOrDefault(s => s.Name == "To do");
            var doneStatus = actualStatusFlow.Statuses.FirstOrDefault(s => s.Name == "Done");

            //AND id of actual status flow is assigned to expected
            expectedStatusFlow.Id = actualStatusFlow.Id;

            //THEN check equality of actual and expected status flow properties
            actualStatusFlow.Name.Should().Be(expectedStatusFlow.Name);
            actualStatusFlow.IsDefault.Should().Be(expectedStatusFlow.IsDefault);

            //AND check assigned status names
            actualStatusFlow.Statuses.Should().HaveCount(2);
            toDoStatus.Should().NotBeNull();
            doneStatus.Should().NotBeNull();
            
            //AND check status connections
            toDoStatus.ConnectedStatuses.Should().HaveCount(1);
            toDoStatus.ConnectedStatuses.Should().ContainSingle(s=>s.ConnectedStatusInFlowId == doneStatus.Id, "To do status has no connection to done status");

            doneStatus.ConnectedStatuses.Should().HaveCount(1);
            doneStatus.ConnectedStatuses.Should().ContainSingle(s => s.ConnectedStatusInFlowId == toDoStatus.Id, "Done status has no connection to to do status");
            
            
            #region Local methods

            API.Protos.StatusFlow GetExpectedStatusFlow() => new
                () {IsDefault = false, Name = "Status flow for: " + GetExpectedGroupOfIssue().Name};

            API.Protos.GroupOfIssue GetExpectedGroupOfIssue() =>
                new() { Name = "Expected group of issue", ShortName = "EXP", TypeOfGroupId = "001-001" };

            #endregion
        }
    }
}
