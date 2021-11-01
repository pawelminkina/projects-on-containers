using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebBff.Api.Models.Issuses.StatusFlow;
using WebBff.Api.Services.Issues.StatusFlows;

namespace WebBff.Api.Controllers
{
    [Route("statusflow")]
    [Controller]
    public class StatusFlowController : ControllerBase
    {
        private readonly IStatusFlowService _status;

        public StatusFlowController(IStatusFlowService status)
        {
            _status = status ?? throw new ArgumentNullException(nameof(status));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusFlowDto>>> GetFlows()
        {
            var flows = await _status.GetStatusFlowsAsync();
            return Ok(flows);
        }

        [HttpGet("{flowId}")]
        public async Task<ActionResult<StatusFlowDto>> GetFlow([FromRoute] string flowId)
        {
            var flow = await _status.GetStatusFlowAsync(flowId);
            return Ok(flow);
        }

        [HttpPost]
        public async Task<ActionResult> CreateFlow([FromBody] CreateStatusFlowRequest request)
        {
            var created = await _status.CreateStatusFlowAsync(request);
            return Ok(created);//TODO 201 WITH PARAMS
        }

        [HttpPost("{flowId}/status")]
        public async Task<ActionResult> AddStatusToFlow([FromRoute] string flowId, [FromBody] AddStatusToFlowRequest statusToAdd)
        {
            await _status.AddStatusToFlowAsync(statusToAdd.StatusId, flowId);
            return Ok();//TODO 201 WITH PARAMS
        }

        [HttpPost("{flowId}/status/{statusId}/connection/{connectedStatusId}")]
        public async Task<ActionResult> AddConnectionToStatus([FromRoute] string flowId, [FromRoute] string statusId, [FromRoute] string connectedStatusId)
        {
            await _status.AddConnectionToStatusInFlowAsync(statusId, connectedStatusId, flowId);
            return Ok();//TODO 201 WITH PARAMS
        }

        [HttpPut("{flowId}/rename")]
        public async Task<ActionResult> RenameFlow([FromRoute] string flowId, [FromQuery] string newName)
        {
            await _status.RenameStatusFlowAsync(flowId, newName);
            return NoContent();
        }

        [HttpDelete("{flowId}")]
        public async Task<ActionResult> DeleteFlow([FromRoute] string flowId)
        {
            await _status.DeleteStatusFlowAsync(flowId);
            return Ok();
        }

        [HttpDelete("{flowId}/status/{statusId}")]
        public async Task<ActionResult> DeleteStatusInFlow([FromRoute] string flowId, [FromRoute] string statusId)
        {
            await _status.DeleteStatusFromFlowAsync(statusId, flowId);
            return Ok();
        }

        [HttpDelete("{flowId}/status/{statusId}/connection")]
        public async Task<ActionResult> DeleteConnectionWithStatus([FromRoute] string flowId, [FromRoute] string statusId)
        {
            throw new NotImplementedException();
        }

    }
}