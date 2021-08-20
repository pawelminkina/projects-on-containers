using System;
using Issues.Domain.StatusesFlow;
using Moq;
using Xunit;

namespace Issues.Tests.Unit.DomainLogic.StatusesFlow
{
    public class StatusTests
    {

        [Fact]
        public void Rename_Throws_Exception_Because_Requested_Name_Is_Empty_String()
        {
            var mock = new Mock<Status>();
            mock.SetupProperty(d => d.Name, "firstName");
            mock.Setup(d => d.ChangeStringProperty(It.IsAny<string>(), It.IsAny<string>())).CallBase();
            Assert.Throws<InvalidOperationException>(() => mock.Object.Rename(string.Empty));
        }

        [Fact]
        public void Rename_Throws_Exception_Because_Requested_Name_Is_The_Same_As_Current_Name()
        {
            var mock = new Mock<Status>();
            mock.SetupProperty(d => d.Name, "firstName");
            mock.Setup(d => d.ChangeStringProperty(It.IsAny<string>(), It.IsAny<string>())).CallBase();
            Assert.Throws<InvalidOperationException>(() => mock.Object.Rename("firstName"));
        }

        [Fact]
        public void Rename_Changes_Status_Name_To_Requested()
        {
            var mock = new Mock<Status>();
            mock.SetupProperty(d => d.Name, string.Empty);
            mock.Setup(d => d.ChangeStringProperty(It.IsAny<string>(), It.IsAny<string>())).CallBase();
            mock.Object.Rename("secondName");
            Assert.True(mock.Object.Name == "secondName", "Parameter for rename was different than name after executing method");
        }

        [Fact]
        public void Archive_Triggers_Archive_Method_In_Status_Archive_Policy()
        {
            var policyMock = new Mock<IStatusArchivePolicy>();
            bool hasBeenActivated = false;
            Func<bool> Activate() => () => true;
            policyMock.Setup(d => d.Archive()).Callback(() => { hasBeenActivated = Activate().Invoke(); });

            var mock = new Mock<Status>();
            mock.SetupProperty(d => d.IsArchived, false);
            mock.Object.Archive(policyMock.Object);

            Assert.True(hasBeenActivated, "Callback was not invoked");
        }

        [Fact]
        public void Archive_Sets_Is_Archived_Property_Value_To_True()
        {
            var policyMock = new Mock<IStatusArchivePolicy>();
            policyMock.Setup(d => d.Archive()).Returns(true);

            var mock = new Mock<Status>();
            mock.SetupProperty(d => d.IsArchived, false);
            mock.Object.Archive(policyMock.Object);

            Assert.True(mock.Object.IsArchived == true, "Archive method does not set IsArchived property to true");
        }

        [Fact]
        public void Un_Archive_Sets_Is_Archived_Property_Value_To_False()
        {
            var mock = new Mock<Status>();
            mock.SetupProperty(d => d.IsArchived, true);
            mock.Object.UnArchive();
            Assert.True(mock.Object.IsArchived == false, "UnArchive method does not set IsArchived property to false");
        }
    }
}