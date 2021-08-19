using System;
using Issues.Domain.Issues;
using Moq;
using Xunit;

namespace Issues.Tests.Unit.DomainLogic.Issues
{
    public class IssueTests
    {
        [Fact]
        public void Add_Content_Throws_Exception_Because_Content_Is_Already_Added()
        {
            var mock = new Mock<Issue>();

            mock.SetupProperty(s => s.Content, new Mock<IssueContent>().SetupAllProperties().Object);

            Assert.Throws<InvalidOperationException>(() => mock.Object.AddContent("new content"));
        }

        [Fact]
        public void Add_Content_Creates_Content_With_Correct_Text_And_Issue_Reference()
        {
            var mock = new Mock<Issue>();
            mock.SetupProperty(s => s.Content);
            var addedContent = mock.Object.AddContent("new content");

            Assert.Same("new content",addedContent.TextContent);
            Assert.Equal(mock.Object, addedContent.ParentIssue);
        }

        [Fact]
        public void Rename_Throws_Exception_Because_Requested_Name_Is_Empty_String()
        {
            var mock = new Mock<Issue>();
            mock.SetupProperty(d => d.Name, "firstName");
            mock.Setup(d => d.ChangeStringProperty(It.IsAny<string>(), It.IsAny<string>())).CallBase();
            Assert.Throws<InvalidOperationException>(() => mock.Object.Rename(string.Empty));
        }

        [Fact]
        public void Rename_Throws_Exception_Because_Requested_Name_Is_The_Same_As_Current_Name()
        {
            var mock = new Mock<Issue>();
            mock.SetupProperty(d => d.Name, "firstName");
            mock.Setup(d => d.ChangeStringProperty(It.IsAny<string>(), It.IsAny<string>())).CallBase();
            Assert.Throws<InvalidOperationException>(() => mock.Object.Rename("firstName"));
        }

        [Fact]
        public void Rename_Changes_Issue_Name_To_Requested()
        {
            var mock = new Mock<Issue>();
            mock.SetupProperty(d => d.Name, string.Empty);
            mock.Setup(d => d.ChangeStringProperty(It.IsAny<string>(), It.IsAny<string>())).CallBase();
            mock.Object.Rename("secondName");
            Assert.True(mock.Object.Name == "secondName", "Parameter for rename was different than name after executing method");
        }

        [Fact]
        public void Change_Status_Throws_Exception_Because_Requested_Status_Id_Is_Empty_String()
        {
            var mock = new Mock<Issue>();
            mock.SetupProperty(d => d.Name, "firstName");
            mock.Setup(d => d.ChangeStringProperty(It.IsAny<string>(), It.IsAny<string>())).CallBase();
            Assert.Throws<InvalidOperationException>(() => mock.Object.ChangeStatus(string.Empty));
        }

        [Fact]
        public void Change_Status_Throws_Exception_Because_Requested_Status_Id_Is_The_Same_As_Current_Status_Id()
        {
            var mock = new Mock<Issue>();
            mock.SetupProperty(d => d.Name, "firstStatusId");
            mock.Setup(d => d.ChangeStringProperty(It.IsAny<string>(), It.IsAny<string>())).CallBase();
            Assert.Throws<InvalidOperationException>(() => mock.Object.ChangeStatus("firstStatusId"));
        }

        [Fact]
        public void Change_Status_Changes_Issue_Status_Id_To_Requested()
        {
            var mock = new Mock<Issue>();
            mock.SetupProperty(d => d.StatusId, string.Empty);
            mock.Setup(d => d.ChangeStringProperty(It.IsAny<string>(), It.IsAny<string>())).CallBase();
            mock.Object.ChangeStatus("newStatus");
            Assert.True(mock.Object.StatusId == "newStatus", "Parameter for rename was different than name after executing method");

        }

        [Fact]
        public void Archive_Sets_Triggers_Archive_On_Content()
        {
            var mock = new Mock<Issue>();
            
            bool hasBeenActivated = false;
            Func<bool> Activate() => () => true;

            mock.Setup(d => d.Content.Archive()).Callback(() => { hasBeenActivated = Activate().Invoke(); });
            mock.SetupProperty(d => d.IsArchived, false);

            mock.Object.Archive();

            Assert.True(hasBeenActivated, "Callback was not invoked");
        }

        [Fact]
        public void Un_Archive_Sets_Triggers_Un_Archive_On_Content()
        {
            var mock = new Mock<Issue>();

            bool hasBeenActivated = false;
            Func<bool> Activate() => () => true;

            mock.Setup(d => d.Content.UnArchive()).Callback(() => { hasBeenActivated = Activate().Invoke(); });
            mock.SetupProperty(d => d.IsArchived, false);

            mock.Object.UnArchive();

            Assert.True(hasBeenActivated, "Callback was not invoked");
        }

        [Fact]
        public void Archive_Sets_Is_Archived_Property_Value_To_True()
        {
            var mock = new Mock<Issue>();

            mock.SetupProperty(d => d.IsArchived, false);
            mock.Setup(d => d.Content.Archive()).CallBase();

            mock.Object.Archive();

            Assert.True(mock.Object.IsArchived == true, "Archive method does not set IsArchived property to true");
        }

        [Fact]
        public void Un_Archive_Sets_Is_Archived_Property_Value_To_False()
        {
            var mock = new Mock<Issue>();

            mock.SetupProperty(d => d.IsArchived, true);
            mock.Setup(d => d.Content.UnArchive()).CallBase();

            mock.Object.UnArchive();

            Assert.True(mock.Object.IsArchived == false, "UnArchive method does not set IsArchived property to false");
        }
    }
}