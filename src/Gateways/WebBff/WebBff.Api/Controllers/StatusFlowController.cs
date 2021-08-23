using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebBff.Api.Models.Issuses.StatusFlow;
using WebBff.Api.Services.Issues.StatusFlow;

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
        public async Task<ActionResult<StatusFlowDto>> GetFlow(string flowId)
        {
            var flow = await _status.GetStatusFlowAsync(flowId);
            return Ok(flow);
        }

        [HttpPost]
        public async Task<ActionResult> CreateFlow(CreateStatusFlowRequest request)
        {
            var created = await _status.CreateStatusFlowAsync(new StatusFlowDto());
            return Ok(created);
        }

        [HttpPost("{flowId}/status")]
        public async Task<ActionResult> AddStatusToFlow(string flowId, AddStatusToFlowRequest statusToAdd)
        {
            await _status.AddStatusToFlowAsync(statusToAdd.StatusId, flowId);
            return Ok();
        }

        [HttpPost("{flowId}/status/{statusId}/connection/{connectedStatusId}")]
        public async Task<ActionResult> AddConnectionToStatus(string flowId, string statusId, string connectedStatusId)
        {
            await _status.AddConnectionToStatusInFlowAsync(statusId, connectedStatusId, flowId);
            return Ok();
        }

        [HttpPut("{flowId}/rename")]
        public async Task<ActionResult> RenameFlow(string flowId, string newName)
        {
            await _status.RenameStatusFlowAsync(flowId, newName);
            return NoContent();
        }

        [HttpDelete("{flowId}")]
        public async Task<ActionResult> DeleteFlow(string flowId)
        {
            await _status.DeleteStatusFlowAsync(flowId);
            return Ok();
        }

        [HttpDelete("{flowId}/status/{statusId}")]
        public async Task<ActionResult> DeleteStatusInFlow(string flowId, string statusId)
        {
            await _status.DeleteStatusFromFlowAsync(statusId, flowId);
            return Ok();
        }

        [HttpDelete("{flowId}/status/{statusId}/connection/{connectedStatusId}")]
        public async Task<ActionResult> DeleteConnectionWithStatus(string flowId, string statusId, string connectedStatusId)
        {
            await _status.DeleteConnectionToStatusFromFlowAsync(statusId, connectedStatusId, flowId);
            return Ok();
        }

    }
}