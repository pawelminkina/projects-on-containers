using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebBff.Api.Models.Issuses.TypeOfIssue;
using WebBff.Api.Services.Issues.TypeOfIssue;

namespace WebBff.Api.Controllers
{
    [Route("typeofiissue")]
    [Controller]
    public class TypeOfIssueController : ControllerBase
    {
        private readonly ITypeOfIssueService _service;

        public TypeOfIssueController(ITypeOfIssueService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeOfIssueDto>>> GetTypesOfIssues()
        {
            var types = await _service.GetTypesOfIssuesAsync();
            return Ok(types);
        }

        [HttpGet("{typeId}")]
        public async Task<ActionResult<TypeOfIssueDto>> GetTypeOfIssues([FromRoute] string typeId)
        {
            var type = await _service.GetTypeOfIssuesAsync(typeId);
            return Ok(type);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTypeOfIssues([FromBody] CreateTypeOfIssuesRequest request)
        {
            var res = await _service.CreateTypeOfIssuesAsync(new TypeOfIssueDto());
            return Ok(res);//TODO 201 WITH PARAMS
        }

        [HttpDelete("{typeId}")]
        public async Task<ActionResult> DeleteTypeOfIssues([FromRoute] string typeId)
        {
            await _service.DeleteTypeOfIssuesAsync(typeId);
            return Ok();
        }

        [HttpPut("{typeId}")]
        public async Task<ActionResult> RenameTypeOfIssues([FromRoute] string typeId, [FromQuery] string newName)
        {
            await _service.RenameTypeOfIssuesAsync(typeId, newName);
            return NoContent();
        }
    }
}