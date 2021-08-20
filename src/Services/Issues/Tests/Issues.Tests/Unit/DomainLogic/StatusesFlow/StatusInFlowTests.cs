using System;
using System.Collections.Generic;
using FluentAssertions;
using Issues.Domain.Issues;
using Issues.Domain.StatusesFlow;
using Moq;
using Xunit;

namespace Issues.Tests.Unit.DomainLogic.StatusesFlow
{
    public class StatusInFlowTests
    {
        [Fact]
        public void Add_Connected_Status_Throws_Exception_Because_Given_Status_Is_Null()
        {
            var mock = new Mock<StatusInFlow>();
            Assert.Throws<InvalidOperationException>(() => mock.Object.AddConnectedStatus(null));
        }

        [Fact]
        public void Add_Connected_Status_Throws_Exception_Because_Given_Status_Is_Already_Added()
        {
            var mock = new Mock<StatusInFlow>();
            mock.Setup(s => s.ConnectedStatuses).Returns(new List<Status>() { new Mock<Status>().SetupProperty(s => s.Id, "someId").Object });
            Assert.Throws<InvalidOperationException>(() => mock.Object.AddConnectedStatus(new Mock<Status>().SetupProperty(s => s.Id, "someId").Object));
        }

        [Fact]
        public void Add_Connected_Status_Properly_Adds_Status_To_Collection()
        {
            var mock = new Mock<StatusInFlow>();
            mock.SetupAllProperties();
            var status = new Mock<Status>();
            status.Setup(d => d.Id).Returns("123");
            
            mock.Object.AddConnectedStatus(status.Object);

            mock.Object.ConnectedStatuses.Count.Should().Be(1);

        }

        [Fact]
        public void Delete_Connected_Status_Throws_Exception_Because_Status_Does_Not_Exist_In_Collection()
        {
            var mock = new Mock<StatusInFlow>();
            mock.Setup(s => s.ConnectedStatuses).Returns(new List<Status>() { new Mock<Status>().SetupProperty(s => s.Id, "someId").Object });
            Assert.Throws<InvalidOperationException>(() => mock.Object.DeleteConnectedStatus("someOtherId"));
        }

        [Fact]
        public void Delete_Connected_Status_Properly_Removes_Status_From_Collection()
        {
            var mock = new StatusInFlow
            {
                ConnectedStatuses = new List<Status>() {new Status() {Id = "123"}}
            };

            mock.ConnectedStatuses.Count.Should().Be(1);

            mock.DeleteConnectedStatus("123");

            mock.ConnectedStatuses.Should().HaveCount(0);
        }

        [Fact]
        public void Archive_Sets_Is_Archived_Property_Value_To_True()
        {
            var mock = new StatusInFlow
            {
                IsArchived = false
            };

            mock.IsArchived.Should().Be(false);
            mock.Archive();
            mock.IsArchived.Should().Be(true);
        }

        [Fact]
        public void Un_Archive_Sets_Is_Archived_Property_Value_To_False()
        {
            var mock = new StatusInFlow
            {
                IsArchived = true
            };

            mock.IsArchived.Should().Be(true);
            mock.UnArchive();
            mock.IsArchived.Should().Be(false);
        }
    }
}