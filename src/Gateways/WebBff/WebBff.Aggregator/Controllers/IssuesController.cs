using Microsoft.AspNetCore.Mvc;
using WebBff.Aggregator.Models.Issues;
using WebBff.Aggregator.Services.Issues;

namespace WebBff.Aggregator.Controllers;

public class IssuesController : ControllerBase
{
    private readonly IIssuesService _issuesService;

    public IssuesController(IIssuesService issuesService)
    {
        _issuesService = issuesService;
    }

    [HttpGet("user/{id}")]
    public async Task<ActionResult<IEnumerable<IssueReferenceDto>>> GetIssuesForUser([FromRoute] string id)
    {
        var issues = await _issuesService.GetIssuesForUser(id);
        return Ok(issues);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IssueWithContentDto>> GetIssueWithContent([FromRoute] string id)
    {
        var issue = await _issuesService.GetIssueWithContent(id);
        return Ok(issue);
    }

    [HttpPost]
    public async Task<ActionResult<IssueWithContentDto>> CreateIssue([FromBody] IssueForCreationDto dto)
    {
        var newIssueId = await _issuesService.CreateIssue(dto);
        return CreatedAtAction(nameof(GetIssueWithContent), new {id = newIssueId}, new {id = newIssueId});
    }

    [HttpPut("rename")]
    public async Task<ActionResult> RenameIssue([FromBody] RenameIssueDto dto)
    {
        await _issuesService.RenameIssue(dto);
        return NoContent();
    }

    [HttpPut("textContent")]
    public async Task<ActionResult> UpdateTextContent([FromBody] UpdateTextContentOfIssueDto dto)
    {
        await _issuesService.UpdateTextContentOfIssue(dto);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> DeleteIssue([FromRoute] string id)
    {
        await _issuesService.DeleteIssue(id);
        return NoContent();
    }
}