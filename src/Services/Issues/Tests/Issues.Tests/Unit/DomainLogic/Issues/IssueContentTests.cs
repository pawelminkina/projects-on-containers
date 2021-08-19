using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Issues.Domain.Issues;
using Moq;
using Xunit;

namespace Issues.Tests.Unit.DomainLogic.Issues
{
    public class IssueContentTests
    {
        [Fact]
        public void Change_Text_Content_Sets_Correct_Text()
        {
            var mock = new Mock<IssueContent>();
            mock.SetupProperty(d => d.TextContent, "someText");

            mock.Object.ChangeTextContent("newText");

            Assert.Same("newText", mock.Object.TextContent);
        }

        [Fact]
        public void Change_Text_Content_With_Null_Sets_Empty_Text()
        {
            var mock = new Mock<IssueContent>();
            mock.SetupProperty(d => d.TextContent, "someText");

            mock.Object.ChangeTextContent(null);

            Assert.Same(string.Empty, mock.Object.TextContent); }

        [Fact]
        public void Archive_Sets_Is_Archived_Property_Value_To_True()
        {
            var mock = new Mock<IssueContent>();
            mock.SetupProperty(d => d.IsArchived, false);
            mock.Setup(s => s.Archive()).CallBase();
            mock.Object.Archive();

            Assert.True(mock.Object.IsArchived == true, "Archive method does not set IsArchived property to true");
        }

        [Fact]
        public void Un_Archive_Sets_Is_Archived_Property_Value_To_False()
        {
            var mock = new Mock<IssueContent>();
            mock.SetupProperty(d => d.IsArchived, true);
            mock.Setup(s => s.UnArchive()).CallBase();

            mock.Object.UnArchive();

            Assert.True(mock.Object.IsArchived == false, "UnArchive method does not set IsArchived property to false");
        }
    }
}
