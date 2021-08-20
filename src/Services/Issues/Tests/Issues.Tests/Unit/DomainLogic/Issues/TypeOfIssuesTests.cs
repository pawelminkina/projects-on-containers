using System;
using Issues.Domain.Issues;
using Moq;
using Xunit;

namespace Issues.Tests.Unit.DomainLogic.Issues
{
    public class TypeOfIssuesTests
    {
        [Fact]
        public void Rename_Throws_Exception_Because_Requested_Name_Is_Empty_String()
        {
            var mock = new Mock<TypeOfIssues>();
            mock.SetupProperty(d => d.Name, "firstName");
            mock.Setup(d => d.ChangeStringProperty(It.IsAny<string>(), It.IsAny<string>())).CallBase();
            Assert.Throws<InvalidOperationException>(() => mock.Object.Rename(string.Empty));
        }

        [Fact]
        public void Rename_Throws_Exception_Because_Requested_Name_Is_The_Same_As_Current_Name()
        {
            var mock = new Mock<TypeOfIssues>();
            mock.SetupProperty(d => d.Name, "firstName");
            mock.Setup(d => d.ChangeStringProperty(It.IsAny<string>(), It.IsAny<string>())).CallBase();
            Assert.Throws<InvalidOperationException>(() => mock.Object.Rename("firstName"));
        }

        [Fact]
        public void Rename_Changes_Type_Of_Issue_Name_To_Requested()
        {
            var mock = new Mock<TypeOfIssues>();
            mock.SetupProperty(d => d.Name, string.Empty);
            mock.Setup(d => d.ChangeStringProperty(It.IsAny<string>(), It.IsAny<string>())).CallBase();
            mock.Object.Rename("secondName");
            Assert.True(mock.Object.Name == "secondName", "Parameter for rename was different than name after executing method");
        }

        [Fact]
        public void Change_Status_Flow_Throws_Exception_Because_Requested_Flow_Status_Id_Is_Empty_String()
        {
            var mock = new Mock<TypeOfIssues>();
            mock.SetupProperty(d => d.Name, "firstName");
            mock.Setup(d => d.ChangeStringProperty(It.IsAny<string>(), It.IsAny<string>())).CallBase();
            Assert.Throws<InvalidOperationException>(() => mock.Object.ChangeStatusFlow(string.Empty));
        }

        [Fact]
        public void Change_Status_Flow_Throws_Exception_Because_Requested_Flow_Status_Id_Is_The_Same_As_Current_Flow_Status_Id()
        {
            var mock = new Mock<TypeOfIssues>();
            mock.SetupProperty(d => d.Name, "firstStatusId");
            mock.Setup(d => d.ChangeStringProperty(It.IsAny<string>(), It.IsAny<string>())).CallBase();
            Assert.Throws<InvalidOperationException>(() => mock.Object.ChangeStatusFlow("firstStatusId"));
        }

        [Fact]
        public void Change_Status_Flow_Changes_Flow_Status_Id_To_Requested()
        {
            var mock = new Mock<TypeOfIssues>();
            mock.SetupProperty(d => d.StatusFlowId, string.Empty);
            mock.Setup(d => d.ChangeStringProperty(It.IsAny<string>(), It.IsAny<string>())).CallBase();
            mock.Object.ChangeStatusFlow("newStatus");
            Assert.True(mock.Object.StatusFlowId == "newStatus", "Parameter for rename was different than name after executing method");

        }

        [Fact]
        public void Archive_Sets_Is_Archived_Property_Value_To_True()
        {
            var mock = new Mock<TypeOfIssues>();
            mock.SetupProperty(d => d.IsArchived, false);
            mock.Object.Archive();
            Assert.True(mock.Object.IsArchived == true, "Archive method does not set IsArchived property to true");
        }

        [Fact]
        public void Un_Archive_Sets_Is_Archived_Property_Value_To_False()
        {
            var mock = new Mock<TypeOfIssues>();
            mock.SetupProperty(d => d.IsArchived, true);
            mock.Object.UnArchive();
            Assert.True(mock.Object.IsArchived == false, "UnArchive method does not set IsArchived property to false");
        }
    }
}