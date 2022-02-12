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
        public async Task ShouldCreateStatusFlowForGroupOfIssuesWithDefaultStatuses()
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
            toDoStatus.ConnectedStatusesId.Should().HaveCount(1);
            toDoStatus.ConnectedStatusesId.Should().ContainSingle(s=>s == doneStatus.Id, "To do status has no connection to done status");

            doneStatus.ConnectedStatusesId.Should().HaveCount(1);
            doneStatus.ConnectedStatusesId.Should().ContainSingle(s => s == toDoStatus.Id, "Done status has no connection to to do status");
            
            
            #region Local methods

            API.Protos.StatusFlow GetExpectedStatusFlow() => new
                () {IsDefault = false, Name = "Status flow for: " + GetExpectedGroupOfIssue().Name};

            API.Protos.GroupOfIssue GetExpectedGroupOfIssue() =>
                new() { Name = "Expected group of issue", ShortName = "EXP", TypeOfGroupId = "001-001" };

            #endregion
        }

        [Test]
        public async Task ShouldRenameStatusFlowWhenGroupOfIssuesIsRenamed()
        {
            //GIVEN expected group of issues
            var groupOfIssues = GetExpectedGroupOfIssue();

            //AND expected new name for status flow
            var expectedStatusFlowName = GetExpectedStatusFlowName();

            //WHEN group of issues is renamed
            var renameRequest = new RenameGroupOfIssuesRequest() {Id = groupOfIssues.Id, NewName = groupOfIssues.Name};
            var renameResponse = await _grpcGroupOfIssueClient.RenameGroupOfIssuesAsync(renameRequest);

            //AND status flow is retrieved from group of issues
            var getStatusFlowRequest = new GetStatusFlowForGroupOfIssuesRequest() { GroupOfIssuesId = groupOfIssues.Id };
            var getStatusFlowResponse = await _grpcStatusFlowClient.GetStatusFlowForGroupOfIssuesAsync(getStatusFlowRequest);
            var actualStatusFlow = getStatusFlowResponse.Flow;

            //THEN check equality of new status flow name with expected
            actualStatusFlow.Name.Should().Be(expectedStatusFlowName);

            #region Local methods

            API.Protos.GroupOfIssue GetExpectedGroupOfIssue() =>
                new() { Name = "New Group Of Issue name", Id  = "002-003" };

            string GetExpectedStatusFlowName() => "Status flow for: " + GetExpectedGroupOfIssue().Name;

            #endregion
        }

        [Test]
        public async Task ShouldDeleteStatusFlowWhenGroupOfIssuesIsDeleted()
        {
            //GIVEN group of issues to delete
            var groupOfIssues = "002-003";

            //WHEN group of issues is deleted
            var deleteRequest = new DeleteGroupOfIssuesRequest() {Id = groupOfIssues};
            var deleteResponse = await _grpcGroupOfIssueClient.DeleteGroupOfIssuesAsync(deleteRequest);

            //AND status flow for group of issues is retrieved from server
            var getStatusFlowRequest = new GetStatusFlowForGroupOfIssuesRequest() { GroupOfIssuesId = groupOfIssues };
            var getStatusFlowResponse = await _grpcStatusFlowClient.GetStatusFlowForGroupOfIssuesAsync(getStatusFlowRequest);
            var actualStatusFlow = getStatusFlowResponse.Flow;

            //THEN check that status flow has is deleted flag set to true
            actualStatusFlow.IsDeleted.Should().BeTrue();
        }

        
    }
}
