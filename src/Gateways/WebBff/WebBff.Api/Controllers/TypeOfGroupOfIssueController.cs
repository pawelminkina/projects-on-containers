using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebBff.Api.Models.Issuses.TypeOfGroupOfIssue;
using WebBff.Api.Services.Issues.TypeOfGroupOfIssue;

namespace WebBff.Api.Controllers
{
    [Route("typeofgroupofissue")]
    [Controller]
    public class TypeOfGroupOfIssueController : ControllerBase
    {
        private readonly ITypeOfGroupOfIssueService _service;

        public TypeOfGroupOfIssueController(ITypeOfGroupOfIssueService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeOfGroupOfIssueDto>>> GetTypesOfGroupsOfIssues()
        {
            var types = await _service.GetTypesOfGroupsOfIssuesAsync();
            return Ok(types);
        }

        [HttpGet("{typeId}")]
        public async Task<ActionResult<TypeOfGroupOfIssueDto>> GetTypeOfGroupsOfIssues([FromRoute] string typeId)
        {
            var type = await _service.GetTypeOfGroupsOfIssuesAsync(typeId);
            return Ok(type);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTypeOfGroupOfIssues([FromBody] CreateTypeOfGroupOfIssueRequest request)
        {
            var type = await _service.CreateTypeOfGroupOfIssueAsync(new TypeOfGroupOfIssueDto(){Name = request.Name});
            return CreatedAtAction("GetTypeOfGroupsOfIssues", new {typeId = type}, new TypeOfGroupOfIssueDto(){Id = type, Name = request.Name});//TODO 201 WITH PARAMS
        }

        [HttpPut("{typeId}/rename")]
        public async Task<ActionResult> RenameTypeOfGroupOfIssues([FromRoute] string typeId, [FromQuery] string newName)
        {
            await _service.RenameTypeOfGroupOfIssueAsync(typeId, newName);
            return NoContent();
        }

        [HttpDelete("{typeId}")]
        public async Task<ActionResult> DeleteTypeOfGroupOfIssues([FromRoute] string typeId)
        {
            await _service.DeleteTypeOfGroupOfIssuesAsync(typeId);
            return Ok();
        }
    }
}