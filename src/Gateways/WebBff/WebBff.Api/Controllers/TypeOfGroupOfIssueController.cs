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

        [HttpGet("{typeid}")]
        public async Task<ActionResult<TypeOfGroupOfIssueDto>> GetTypeOfGroupsOfIssues(string typeid)
        {
            var type = await _service.GetTypeOfGroupsOfIssuesAsync(typeid);
            return Ok(typeid);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTypeOfGroupOfIssues(CreateTypeOfGroupOfIssueRequest request)
        {
            var type = await _service.CreateTypeOfGroupOfIssueAsync(new TypeOfGroupOfIssueDto());
            return Ok(type);
        }

        [HttpPut("{typeid}/rename")]
        public async Task<ActionResult> RenameTypeOfGroupOfIssues(string typeId, string newName)
        {
            await _service.RenameTypeOfGroupOfIssueAsync(typeId, newName);
            return NoContent();
        }

        [HttpDelete("{typeid}")]
        public async Task<ActionResult> DeleteTypeOfGroupOfIssues(string typeid)
        {
            await _service.DeleteTypeOfGroupOfIssuesAsync(typeid);
            return Ok();
        }
    }
}