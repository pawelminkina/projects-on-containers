using Microsoft.AspNetCore.Mvc;
using WebBff.Aggregator.Models.GroupOfIssues;
using WebBff.Aggregator.Services.GroupOfIssues;

namespace WebBff.Aggregator.Controllers;

public class GroupOfIssuesController : ControllerBase
{
    private readonly IGroupOfIssuesService _groupOfIssuesService;

    public GroupOfIssuesController(IGroupOfIssuesService groupOfIssuesService)
    {
        _groupOfIssuesService = groupOfIssuesService;
    }

    [HttpGet]
    public async Task<ActionResult<GroupOfIssuesDto>> GetGroupsOfIssues()
    {
        var groups = await _groupOfIssuesService.GetGroupsOfIssues();
        return Ok(groups);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GroupOfIssuesWithIssuesDto>> GetGroupOfIssuesWithIssues([FromRoute] string id)
    {
        var group = await _groupOfIssuesService.GetGroupOfIssues(id);
        return Ok(group);
    }

    [HttpPost]
    public async Task<ActionResult<GroupOfIssuesWithIssuesDto>> CreateGroupOfIssues([FromBody] GroupOfIssuesForCreationDto dto)
    {
        var newGroupOfIssuesId = await _groupOfIssuesService.CreateGroupOfIssues(dto);
        return CreatedAtAction(nameof(GetGroupOfIssuesWithIssues), new {id = newGroupOfIssuesId}, new {id = newGroupOfIssuesId});
    }

    [HttpPut("rename")]
    public async Task<ActionResult> RenameGroupOfIssues([FromBody] RenameGroupOfIssuesDto dto)
    {
        await _groupOfIssuesService.RenameGroupOfIssues(dto);
        return NoContent();
    }

    [HttpPut("changeshortname")]
    public async Task<ActionResult> ChangeGroupOfIssuesShortName([FromBody] ChangeShortNameForGroupOfIssuesDto dto)
    {
        await _groupOfIssuesService.ChangeShortNameForGroupOfIssues(dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteGroupOfIssues([FromRoute] string id)
    {
        await _groupOfIssuesService.DeleteGroupOfIssues(id);
        return NoContent();
    }
}