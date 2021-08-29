using System;
using System.Collections.Generic;
using Castle.Components.DictionaryAdapter;
using FluentAssertions;
using Issues.Domain.StatusesFlow;
using Moq;
using Xunit;

namespace Issues.Tests.Unit.DomainLogic.StatusesFlow
{
    public class StatusFlowTests
    {
        [Fact]
        public void Add_New_Status_To_Flow_Throws_Exception_Because_Status_Already_Exist_In_Flow()
        {
            var mock = new StatusFlow() {StatusesInFlow = new List<StatusInFlow>(){new StatusInFlow(){ParentStatus = new Status(){Id = "123"}}}};
            var statusMock = new Status() {Id = "123"};
            Assert.Throws<InvalidOperationException>(() => mock.AddNewStatusToFlow(statusMock));
        }

        [Fact]
        public void Add_New_Status_To_Flow_Creates_Status_In_Flow_With_Requested_Properties()
        {
            var mock = new StatusFlow() { StatusesInFlow = new List<StatusInFlow>() { new StatusInFlow() { ParentStatus = new Status() { Id = "123" } } } };
            var statusMock = new Status() { Id = "1234" };
            var addedStatus = mock.AddNewStatusToFlow(statusMock);

            addedStatus.IndexInFlow.Should().Be(1);
            addedStatus.ParentStatus.Id.Should().Be("1234");
        }

        [Fact]
        public void Add_New_Status_To_Flow_Adds_New_Status_To_Collection()
        {
            var mock = new StatusFlow() { StatusesInFlow = new List<StatusInFlow>() { new StatusInFlow() { ParentStatus = new Status() { Id = "123" } } } };
            mock.StatusesInFlow.Should().HaveCount(1);
            var statusMock = new Status() { Id = "1234" };
            mock.AddNewStatusToFlow(statusMock);
            mock.StatusesInFlow.Should().HaveCount(2);
        }

        [Fact]
        public void Delete_Status_From_Flow_Throws_Exception_Because_Requested_Status_Does_Not_Exist()
        {
            var mock = new StatusFlow() { StatusesInFlow = new List<StatusInFlow>() { new StatusInFlow() { ParentStatus = new Status() { Id = "123" } } } };
            Assert.Throws<InvalidOperationException>(()=> mock.DeleteStatusFromFlow("1234"));
        }


        [Fact]
        public void Delete_Status_From_Flow_Removes_Requested_Status_From_Collection()
        {
            var mock = new StatusFlow() { StatusesInFlow = new List<StatusInFlow>() { new StatusInFlow() { ParentStatus = new Status() { Id = "123" }, Id = "1234"} } };
            mock.StatusesInFlow.Should().HaveCount(1);
            mock.DeleteStatusFromFlow("123");
            mock.StatusesInFlow.Should().HaveCount(0);
        }

        [Fact]
        public void Rename_Throws_Exception_Because_Requested_Name_Is_Empty_String()
        {
            var mock = new Mock<StatusFlow>();
            mock.SetupProperty(d => d.Name, "firstName");
            mock.Setup(d => d.ChangeStringProperty(It.IsAny<string>(), It.IsAny<string>())).CallBase();
            Assert.Throws<InvalidOperationException>(() => mock.Object.Rename(string.Empty));

        }

        [Fact]
        public void Rename_Throws_Exception_Because_Requested_Name_Is_The_Same_As_Current_Name()
        {
            var mock = new Mock<StatusFlow>();
            mock.SetupProperty(d => d.Name, "firstName");
            mock.Setup(d => d.ChangeStringProperty(It.IsAny<string>(), It.IsAny<string>())).CallBase();
            Assert.Throws<InvalidOperationException>(() => mock.Object.Rename("firstName"));

        }

        [Fact]
        public void Rename_Changes_Status_Flow_Name_To_Requested()
        {
            var mock = new Mock<StatusFlow>();
            mock.SetupProperty(d => d.Name, string.Empty);
            mock.Setup(d => d.ChangeStringProperty(It.IsAny<string>(), It.IsAny<string>())).CallBase();
            mock.Object.Rename("secondName");
            Assert.True(mock.Object.Name == "secondName", "Parameter for rename was different than name after executing method");
        }

        [Fact]
        public void Archive_Sets_Is_Archived_Property_Value_To_True()
        {
            var mock = new StatusFlow
            {
                IsArchived = false,
                StatusesInFlow = new List<StatusInFlow>()
            };

            mock.IsArchived.Should().Be(false);
            mock.Archive();
            mock.IsArchived.Should().Be(true);
        }

        [Fact]
        public void Un_Archive_Sets_Is_Archived_Property_Value_To_False()
        {
            var mock = new StatusFlow
            {
                IsArchived = true,
                StatusesInFlow = new List<StatusInFlow>()
            };

            mock.IsArchived.Should().Be(true);
            mock.UnArchive();
            mock.IsArchived.Should().Be(false);
        }
    }
}