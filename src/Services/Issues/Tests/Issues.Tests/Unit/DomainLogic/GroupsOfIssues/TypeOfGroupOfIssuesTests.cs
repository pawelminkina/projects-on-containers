using System;
using Issues.Domain.GroupsOfIssues;
using Moq;
using Xunit;

namespace Issues.Tests.Unit.DomainLogic.GroupsOfIssues
{
    public class TypeOfGroupOfIssuesTests
    {
        [Fact]
        public void Rename_Throws_Exception_Because_Requested_Name_Is_Empty_String()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Rename_Throws_Exception_Because_Requested_Name_Is_The_Same_As_Current_Name()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Rename_Changes_Type_Of_Group_Name_To_Requested()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Archive_Triggers_Archive_Method_In_Type_Group_Of_Issues_Archive_Policy()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Archive_Sets_Is_Archived_Property_Value_To_True()
        {
            throw new NotImplementedException();

        }

        [Fact]
        public void Un_Archive_Sets_Is_Archived_Property_Value_To_False()
        {
            var mock = new Mock<TypeOfGroupOfIssues>();
            mock.SetupGet(a => a.IsArchived).Returns(true);
            var obj = mock.Object;
            obj.UnArchive();
            Assert.True(false == obj.IsArchived);
        }
    }
}