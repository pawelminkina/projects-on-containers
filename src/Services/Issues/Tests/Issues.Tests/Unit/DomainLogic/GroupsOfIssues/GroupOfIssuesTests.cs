using System;
using Xunit;

namespace Issues.Tests.Unit.DomainLogic.GroupsOfIssues
{
    public class GroupOfIssuesTests
    {
        [Fact]
        public void Add_Issue_Creates_Issue_With_Correct_Properties()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Add_Issue_Throws_Exception_Because_Flow_Does_Not_Have_Default_Status()
        {
            throw new NotImplementedException();

        }

        [Fact]
        public void Add_Issue_Correctly_Adds_New_Item_To_Collection_Of_Issues_In_Group()
        {
            throw new NotImplementedException();

        }

        [Fact]
        public void Assign_Issue_To_Group_Correctly_Assigns_New_Item_To_Collection_Of_Issues_In_Group()
        {
            throw new NotImplementedException();

        }

        [Fact]
        public void Assign_Issue_To_Group_Throws_Exception_Because_Issue_Is_Already_Added_To_Group()
        {
            throw new NotImplementedException();

        }

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
        public void Rename_Changes_Group_Name_To_Requested()
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
            throw new NotImplementedException();
        }
    }
}