using System.Collections.Generic;
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
        public async Task<ActionResult<IEnumerable<GroupOfIssueDto>>> GetGroupsOfIssues()
        {
            var res = await _groupOfIssueService.GetGroupsOfIssuesAsync();
            return Ok(res);
        }

        [HttpGet("{groupId}")]
        public async Task<ActionResult<GroupOfIssueDto>> GetGroupsOfIssue([FromRoute] string groupId)
        {
            var res = await _groupOfIssueService.GetGroupOfIssueAsync(groupId);
            return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateGroupOfIssues([FromBody] CreateGroupOfIssuesRequest request)
        {
            var res = await _groupOfIssueService.CreateGroupOfIssueAsync(request);
            return CreatedAtAction("GetGroupsOfIssue", new { groupId  = res}, new GroupOfIssueDto(){Id = res, Name = request.Name, ShortName = request.ShortName, TypeOfGroupId = request.TypeOfGroupId});
        }

        [HttpDelete("{groupId}")]
        public async Task<ActionResult> DeleteGroupOfIssues([FromRoute] string groupId)
        {
            await _groupOfIssueService.DeleteGroupOfIssueAsync(groupId);
            return Ok();
        }

        [HttpPut("{groupId}")]
        public async Task<ActionResult> RenameGroupOfIssues([FromRoute] string groupId, [FromQuery] string newName)
        {
            await _groupOfIssueService.RenameGroupOfIssueAsync(groupId, newName);
            return NoContent();
        }
    }
}