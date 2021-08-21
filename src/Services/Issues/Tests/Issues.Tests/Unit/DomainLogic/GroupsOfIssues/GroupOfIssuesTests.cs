using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.Issues;
using Issues.Domain.StatusesFlow;
using Moq;
using Xunit;

namespace Issues.Tests.Unit.DomainLogic.GroupsOfIssues
{
    public class GroupOfIssuesTests
    {
        [Fact]
        public void Add_Issue_Creates_Issue_With_Correct_Properties()
        {
            var groupOfIssuesMock = new Mock<GroupOfIssues>();
            var statusInFlow = new Mock<StatusInFlow>();
            var statusFlow = new Mock<StatusFlow>();

            statusInFlow.Setup(s => s.ParentStatus.Id).Returns("someStatusId");
            statusFlow.Setup(s => s.StatusesInFlow).Returns(new List<StatusInFlow>() { statusInFlow.Object });
            groupOfIssuesMock.SetupProperty(d => d.Flow, statusFlow.Object);
            groupOfIssuesMock.SetupProperty(d => d.Issues, new List<Issue>());
            groupOfIssuesMock.SetupProperty(s => s.Id, "someGroupId");

            var createdIssue = groupOfIssuesMock.Object.AddIssue("someName", "user", "textContent", "typeOfIssue");

            Assert.Same(createdIssue.Content.TextContent, "textContent");
            Assert.Same(createdIssue.Name, "someName");
            Assert.Same(createdIssue.CreatingUserId, "user");
            Assert.Same(createdIssue.TypeOfIssueId, "typeOfIssue");
            Assert.Same(createdIssue.StatusId, "someStatusId");
        }

        [Fact]
        public void Add_Issue_Throws_Exception_Because_Flow_Does_Not_Have_Default_Status()
        {
            var groupOfIssuesMock = new Mock<GroupOfIssues>();
            var statusFlow = new Mock<StatusFlow>();

            statusFlow.Setup(s => s.StatusesInFlow).Returns(new List<StatusInFlow>());
            groupOfIssuesMock.SetupProperty(d => d.Flow, statusFlow.Object);

            Assert.Throws<InvalidOperationException>(() => groupOfIssuesMock.Object.AddIssue("someName", "user", "textContent", "typeOfIssue"));

        }

        [Fact]
        public void Add_Issue_Correctly_Adds_New_Item_To_Collection_Of_Issues_In_Group()
        {
            var groupOfIssuesMock = new Mock<GroupOfIssues>();
            var statusInFlow = new Mock<StatusInFlow>();
            var statusFlow = new Mock<StatusFlow>();

            statusInFlow.Setup(s => s.ParentStatus.Id).Returns("someStatusId");
            statusFlow.Setup(s => s.StatusesInFlow).Returns(new List<StatusInFlow>(){statusInFlow.Object});
            groupOfIssuesMock.SetupProperty(d => d.Flow, statusFlow.Object);
            groupOfIssuesMock.SetupProperty(d => d.Issues, new List<Issue>());

            groupOfIssuesMock.Object.AddIssue(string.Empty, string.Empty, string.Empty, string.Empty);

            Assert.True(groupOfIssuesMock.Object.Issues.Count == 1, "Issue has not been added properly to collection");
        }

        [Fact]
        public void Assign_Issue_To_Group_Correctly_Assigns_New_Item_To_Collection_Of_Issues_In_Group()
        {
            var groupOfIssuesMock = new GroupOfIssues
            {
                Issues = new List<Issue>(),
                Id = "4"
            };

            var assigned =groupOfIssuesMock.AssignIssueToGroup(new Issue() {Id = "123", StatusId = "134"}, "12");
            assigned.StatusId.Should().Be("12");
            assigned.Id.Should().Be("123");
            groupOfIssuesMock.Issues.Should().HaveCount(1);
        }

        [Fact]
        public void Assign_Issue_To_Group_Throws_Exception_Because_Given_Status_Id_Is_Empty_String()
        {
            var groupOfIssuesMock = new Mock<GroupOfIssues>();
            var issueMock = new Mock<Issue>();

            issueMock.SetupProperty(s => s.Id, "issueId");
            groupOfIssuesMock.SetupProperty(d => d.Issues, new List<Issue>());

            Assert.Throws<InvalidOperationException>(() => groupOfIssuesMock.Object.AssignIssueToGroup(issueMock.Object, string.Empty));
        }

        [Fact]
        public void Assign_Issue_To_Group_Throws_Exception_Because_Issue_Is_Already_Added_To_Group()
        {
            var groupOfIssuesMock = new Mock<GroupOfIssues>();
            var issueMock = new Mock<Issue>();
            var secondIssueMock = new Mock<Issue>();

            issueMock.SetupProperty(s => s.Id, "issueId");
            secondIssueMock.SetupProperty(s => s.Id, "issueId");
            groupOfIssuesMock.SetupProperty(d => d.Issues, new List<Issue>() { secondIssueMock.Object });

            Assert.Throws<InvalidOperationException>(() => groupOfIssuesMock.Object.AssignIssueToGroup(issueMock.Object, "newStatusId"));
        }

        [Fact]
        public void Rename_Throws_Exception_Because_Requested_Name_Is_Empty_String()
        {
            var mock = new Mock<GroupOfIssues>();
            mock.SetupProperty(d => d.Name, "firstName");
            mock.Setup(d => d.ChangeStringProperty(It.IsAny<string>(), It.IsAny<string>())).CallBase();
            Assert.Throws<InvalidOperationException>(() => mock.Object.Rename(string.Empty));

        }

        [Fact]
        public void Rename_Throws_Exception_Because_Requested_Name_Is_The_Same_As_Current_Name()
        {
            var mock = new Mock<GroupOfIssues>();
            mock.SetupProperty(d => d.Name, "firstName");
            mock.Setup(d => d.ChangeStringProperty(It.IsAny<string>(), It.IsAny<string>())).CallBase();
            Assert.Throws<InvalidOperationException>(() => mock.Object.Rename("firstName"));

        }

        [Fact]
        public void Rename_Changes_Group_Name_To_Requested()
        {
            var mock = new Mock<GroupOfIssues>();
            mock.SetupProperty(d => d.Name, string.Empty);
            mock.Setup(d => d.ChangeStringProperty(It.IsAny<string>(), It.IsAny<string>())).CallBase();
            mock.Object.Rename("secondName");
            Assert.True(mock.Object.Name == "secondName", "Parameter for rename was different than name after executing method");
        }

        [Fact]
        public void Archive_Sets_Is_Archived_Property_Value_To_True()
        {
            var mock = new Mock<GroupOfIssues>();
            mock.SetupProperty(d => d.IsArchived, false);
            mock.SetupProperty(s => s.Issues, new List<Issue>());
            mock.Object.Archive();

            Assert.True(mock.Object.IsArchived == true, "Archive method does not set IsArchived property to true");
        }

        [Fact]
        public void Un_Archive_Sets_Is_Archived_Property_Value_To_False()
        {
            var mock = new Mock<GroupOfIssues>();
            mock.SetupProperty(d => d.IsArchived, true);
            mock.Object.UnArchive();
            Assert.True(mock.Object.IsArchived == false, "UnArchive method does not set IsArchived property to false");
        }
    }
}