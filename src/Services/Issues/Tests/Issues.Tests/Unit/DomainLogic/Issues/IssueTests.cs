using System;
using Xunit;

namespace Issues.Tests.Unit.DomainLogic.Issues
{
    public class IssueTests
    {
        [Fact]
        public void Add_Content_Throws_Exception_Because_Content_Is_Already_Added()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Add_Content_Creates_Content_With_Correct_Text_And_Issue_Reference()
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
        public void Rename_Changes_Issue_Name_To_Requested()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Change_Status_Throws_Exception_Because_Requested_Status_Id_Is_Empty_String()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Change_Status_Throws_Exception_Because_Requested_Status_Id_Is_The_Same_As_Current_Status_Id()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Change_Status_Changes_Issue_Status_Id_To_Requested()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Change_Group_Of_Issue_Throws_Exception_Because_Requested_Group_Of_Issue_Id_Is_Empty_String()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Change_Group_Of_Issue_Throws_Exception_Because_Requested_Group_Of_Issue_Id_Is_The_Same_As_Current_Group_Of_Issue_Id()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Change_Group_Of_Issue_Changes_Group_Of_Issue_Id_To_Requested()
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