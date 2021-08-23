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

        [HttpGet("{typeid}")]
        public async Task<ActionResult<TypeOfIssueDto>> GetTypeOfIssues(string typeid)
        {
            var type = await _service.GetTypeOfIssuesAsync(typeid);
            return Ok(type);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTypeOfIssues(CreateTypeOfIssuesRequest request)
        {
            var res = await _service.CreateTypeOfIssuesAsync(new TypeOfIssueDto());
            return Ok(res);
        }

        [HttpDelete("{typeid}")]
        public async Task<ActionResult> DeleteTypeOfIssues(string typeid)
        {
            await _service.DeleteTypeOfIssuesAsync(typeid);
            return Ok();
        }

        [HttpPut("{typeid}")]
        public async Task<ActionResult> RenameTypeOfIssues(string typeid, string newName)
        {
            await _service.RenameTypeOfIssuesAsync(typeid, newName);
            return NoContent();
        }
    }
}