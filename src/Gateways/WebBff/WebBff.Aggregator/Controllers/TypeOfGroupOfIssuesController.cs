using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using WebBff.Aggregator.Models.TypeOfGroupOfIssues;
using WebBff.Aggregator.Services.GroupOfIssues;
using WebBff.Aggregator.Services.TypeOfGroupOfIssues;

namespace WebBff.Aggregator.Controllers;

public class TypeOfGroupOfIssuesController : ControllerBase
{
    private readonly ITypeOfGroupOfIssuesService _typeOfGroupOfIssuesService;
    private readonly IGroupOfIssuesService _groupOfIssuesService;

    public TypeOfGroupOfIssuesController(ITypeOfGroupOfIssuesService typeOfGroupOfIssuesService, IGroupOfIssuesService groupOfIssuesService)
    {
        _typeOfGroupOfIssuesService = typeOfGroupOfIssuesService;
        _groupOfIssuesService = groupOfIssuesService;
    }

    [HttpGet]
    public async Task<ActionResult<TypeOfGroupOfIssuesDto>> GetTypesOfGroupOfIssues()
    {
        var types = await _typeOfGroupOfIssuesService.GetTypes();
        return Ok(types);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TypeOfGroupOfIssuesWithGroupsDto>> GetTypeGroupOfIssuesWithGroups([FromRoute] string id)
    {
        var type = await _typeOfGroupOfIssuesService.GetType(id);
        var groups = await _groupOfIssuesService.GetGroupsOfIssues();
        var groupsForType = groups.Where(s => s.TypeOfGroupId == type.Id);
        return new TypeOfGroupOfIssuesWithGroupsDto(type) {GroupsOfIssues = groupsForType};
    }

    [HttpPost]
    public async Task<ActionResult<TypeOfGroupOfIssuesWithGroupsDto>> CreateTypeOfGroupOfIssues([FromBody] TypeOfGroupOfIssuesForCreationDto dto)
    {
        var newTypeId = await _typeOfGroupOfIssuesService.CreateTypeOfGroupOfIssues(dto);
        return CreatedAtAction(nameof(GetTypeGroupOfIssuesWithGroups), new { id = newTypeId }, new { id = newTypeId });
    }

    [HttpPut("rename")]
    public async Task<ActionResult> RenameTypeOfGroupOfIssues([FromBody] RenameTypeOfGroupOfIssuesDto dto)
    {
        await _typeOfGroupOfIssuesService.RenameTypeOfGroupOfIssues(dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTypeOfGroupOfIssues([FromRoute] string id)
    {
        await _typeOfGroupOfIssuesService.DeleteTypeOfGroupOfIssues(id);
        return NoContent();
    }

}