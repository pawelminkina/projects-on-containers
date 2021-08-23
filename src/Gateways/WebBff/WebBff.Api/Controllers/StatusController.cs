using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebBff.Api.Models.Issuses.Status;
using WebBff.Api.Services.Issues.Status;

namespace WebBff.Api.Controllers
{
    [Route("status")]
    [Controller]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _service;

        public StatusController(IStatusService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusDto>>> GetStatuses()
        {
            var statuses = await _service.GetStatusesAsync();
            return Ok(statuses);
        }

        [HttpGet("{statusId}")]
        public async Task<ActionResult<StatusDto>> GetStatus([FromRoute] string statusId)
        {
            var status = await _service.GetStatusAsync(statusId);
            return Ok(status);
        }

        [HttpPost]
        public async Task<ActionResult> CreateStatus([FromBody] CreateStatusRequest request)
        {
            var res = await _service.CreateStatusAsync(new StatusDto());
            return Ok();
        }

        [HttpDelete("{statusId}")]
        public async Task<ActionResult> DeleteStatus([FromRoute] string statusId)
        {
            await _service.DeleteStatusAsync(statusId);
            return Ok();
        }

        [HttpPut("{statusId}/rename")]
        public async Task<ActionResult> RenameStatus([FromRoute] string statusId, [FromQuery] string newName)
        {
            await _service.RenameStatusAsync(statusId, newName);
            return NoContent();
        }

    }
}