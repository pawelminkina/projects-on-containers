using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebBff.Api.Models.Issuses.Issues;
using WebBff.Api.Services.Issues.Issues;
using CreateIssueRequest = Issues.API.Protos.CreateIssueRequest;

namespace WebBff.Api.Controllers
{
    [Route("issue")]
    [Controller]
    public class IssueController : ControllerBase
    {
        private readonly IIssueService _service;

        public IssueController(IIssueService service)
        {
            _service = service;
        }

        [HttpGet("group/{groupId}")]
        public async Task<IActionResult> GetIssuesForGroup(string groupId)
        {
            var res  = await _service.GetIssuesForGroupAsync(groupId);
            return Ok(res);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetIssuesForUser(string userId)
        {
            var res = await _service.GetIssuesForUserAsync(userId);
            return Ok(res);
        }

        [HttpGet("{issueId}")]
        public async Task<IActionResult> GetIssueWithContent(string issueId)
        {
            var res = await _service.GetIssueWithContentAsync(issueId);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIssue([FromBody] CreateIssueRequest request)
        {
            var res = await _service.CreateIssueAsync(new IssueDto());
            return Ok(res); //TODO 201 WITH PARAMS
        }

        [HttpDelete("{issueId}")]
        public async Task<IActionResult> DeleteIssue([FromRoute] string issueId)
        {
            await _service.DeleteIssueAsync(issueId);
            return NoContent();
        }

        [HttpPut("{issueId}/rename")]
        public async Task<IActionResult> RenameIssue([FromRoute] string issueId, [FromQuery] string newName)
        {
            await _service.RenameIssueAsync(issueId, newName);
            return NoContent();
        }

        [HttpPut("{issueId}/content")]
        public async Task<IActionResult> UpdateIssueContent([FromRoute] string issueId, [FromBody] string content)
        {
            await _service.UpdateIssueContentAsync(issueId, content);
            return NoContent();
        }

        [HttpPut("{issueId}/status")]
        public async Task<IActionResult> UpdateIssueStatus([FromRoute] string issueId, [FromQuery] string statusId)
        {
            await _service.UpdateIssueStatusAsync(issueId, statusId);
            return NoContent();
        }
    }
}