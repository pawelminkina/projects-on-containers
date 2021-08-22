using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetGroupsOfIssue(string groupId)
        {
            var res = await _groupOfIssueService.GetGroupOfIssueAsync(groupId);
            return Ok(res);
        }
    }
}