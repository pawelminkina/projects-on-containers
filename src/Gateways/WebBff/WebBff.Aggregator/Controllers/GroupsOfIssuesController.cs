using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebBff.Aggregator.Models.GroupOfIssues;
using WebBff.Aggregator.Services.GroupOfIssues;
using WebBff.Aggregator.Services.Issues;

namespace WebBff.Aggregator.Controllers;

[Authorize]
[Controller]
[Route("GroupsOfIssues")]
public class GroupsOfIssuesController : ControllerBase
{
    private readonly IGroupOfIssuesService _groupOfIssuesService;
    private readonly IIssuesService _issuesService;

    public GroupsOfIssuesController(IGroupOfIssuesService groupOfIssuesService, IIssuesService issuesService)
    {
        _groupOfIssuesService = groupOfIssuesService;
        _issuesService = issuesService;
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

        var issues = await _issuesService.GetIssuesForGroup(group.Id);
        var groupToReturn = new GroupOfIssuesWithIssuesDto(group) {Issues = issues};
        
        return Ok(groupToReturn);
    }

    [HttpPost]
    public async Task<ActionResult<GroupOfIssuesWithIssuesDto>> CreateGroupOfIssues([FromBody] GroupOfIssuesForCreationDto dto)
    {
        var newGroupOfIssuesId = await _groupOfIssuesService.CreateGroupOfIssues(dto);
        return CreatedAtAction(nameof(GetGroupOfIssuesWithIssues), new {id = newGroupOfIssuesId}, new {id = newGroupOfIssuesId});
    }

    [HttpPut("{id}/rename")]
    public async Task<ActionResult> RenameGroupOfIssues([FromRoute] string id, [FromBody] RenameGroupOfIssuesDto dto)
    {
        await _groupOfIssuesService.RenameGroupOfIssues(id, dto.NewName);
        return NoContent();
    }

    [HttpPut("{id}/changeshortname")]
    public async Task<ActionResult> ChangeGroupOfIssuesShortName([FromRoute] string id, [FromBody] ChangeShortNameForGroupOfIssuesDto dto)
    {
        await _groupOfIssuesService.ChangeShortNameForGroupOfIssues(id, dto.NewShortName);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteGroupOfIssues([FromRoute] string id)
    {
        await _groupOfIssuesService.DeleteGroupOfIssues(id);
        return NoContent();
    }
}