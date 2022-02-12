using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using WebBff.Aggregator.Models.TypeOfGroupOfIssues;
using WebBff.Aggregator.Services.TypeOfGroupOfIssues;

namespace WebBff.Aggregator.Controllers;

public class TypeOfGroupOfIssuesController : ControllerBase
{
    private readonly ITypeOfGroupOfIssuesService _typeOfGroupOfIssuesService;

    public TypeOfGroupOfIssuesController(ITypeOfGroupOfIssuesService typeOfGroupOfIssuesService)
    {
        _typeOfGroupOfIssuesService = typeOfGroupOfIssuesService;
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
        var type = await _typeOfGroupOfIssuesService.GetTypeWithGroups(id);
        return Ok(type);
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