using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebBff.Api.Models.Issuses.GroupOfIssue;
using WebBff.Api.Services.Issues.GroupOfIssue;

namespace WebBff.Api.Controllers
{
    [Route("groupofissue")]
    [Controller]
    public class GroupOfIssueController : ControllerBase
    {
        private readonly IGroupOfIssueService _groupOfIssueService;

        public GroupOfIssueController(IGroupOfIssueService groupOfIssueService)
        {
            _groupOfIssueService = groupOfIssueService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGroupsOfIssues()
        {
            var res = await _groupOfIssueService.GetGroupsOfIssuesAsync();
            return Ok(res);
        }

        [HttpGet("{groupId}")]
        public async Task<IActionResult> GetGroupsOfIssue([FromRoute] string groupId)
        {
            var res = await _groupOfIssueService.GetGroupOfIssueAsync(groupId);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroupOfIssues([FromBody] CreateGroupOfIssuesRequest request)
        {
            var res = await _groupOfIssueService.CreateGroupOfIssueAsync(new GroupOfIssueDto());
            //TODO Created at route with params
            return Ok(res);
        }

        [HttpDelete("{groupId}")]
        public async Task<IActionResult> DeleteGroupOfIssues([FromRoute] string groupId)
        {
            await _groupOfIssueService.DeleteGroupOfIssueAsync(groupId);
            return Ok();
        }

        [HttpPut("{groupId}")]
        public async Task<IActionResult> RenameGroupOfIssues([FromRoute] string groupId, [FromQuery] string newName)
        {
            await _groupOfIssueService.RenameGroupOfIssueAsync(groupId, newName);
            return NoContent();
        }
    }
}