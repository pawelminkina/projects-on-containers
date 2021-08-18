using System;
using System.Runtime.CompilerServices;
using Issues.Domain.GroupsOfIssues;
using Moq;
using Moq.Protected;
using Xunit;

namespace Issues.Tests.Unit.DomainLogic.GroupsOfIssues
{
    public class TypeOfGroupOfIssuesTests
    {
        [Fact]
        public void Rename_Throws_Exception_Because_Requested_Name_Is_Empty_String()
        {
            var mock = new Mock<TypeOfGroupOfIssues>(string.Empty, "firstName");
            mock.SetupProperty(d => d.Name, "firstName");
            Assert.Throws<InvalidOperationException>(() => mock.Object.RenameGroup(string.Empty));
        }

        [Fact]
        public void Rename_Throws_Exception_Because_Requested_Name_Is_The_Same_As_Current_Name()
        {
            var mock = new Mock<TypeOfGroupOfIssues>(string.Empty, "firstName");
            mock.SetupProperty(d => d.Name, "firstName");
            Assert.Throws<InvalidOperationException>(() => mock.Object.RenameGroup("firstName"));
        }

        [Fact]
        public void Rename_Changes_Type_Of_Group_Name_To_Requested()
        {
            var mock = new Mock<TypeOfGroupOfIssues>(string.Empty, string.Empty);
            mock.SetupProperty(d => d.Name);
            mock.Object.RenameGroup("secondName");
            Assert.True(mock.Object.Name == "secondName", "Parameter for rename was different than name after executing method");
        }

        [Fact]
        public void Archive_Triggers_Archive_Method_In_Type_Group_Of_Issues_Archive_Policy()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Archive_Sets_Is_Archived_Property_Value_To_True()
        {
            var policyMock = new Mock<ITypeGroupOfIssuesArchivePolicy>();
            policyMock.Setup(d => d.Archive()).Returns(true);

            var mock = new Mock<TypeOfGroupOfIssues>(string.Empty, string.Empty);
            mock.SetupProperty(d => d.IsArchived, false);
            mock.Object.Archive(policyMock.Object);

            Assert.True(mock.Object.IsArchived == true, "Archive method does not set IsArchived property to true");
        }

        [Fact]
        public void Un_Archive_Sets_Is_Archived_Property_Value_To_False()
        {
            var mock = new Mock<TypeOfGroupOfIssues>(string.Empty, string.Empty);
            mock.SetupProperty(d => d.IsArchived, true);
            mock.Object.UnArchive();
            Assert.True(mock.Object.IsArchived == false, "UnArchive method does not set IsArchived property to false");
        }
    }
}