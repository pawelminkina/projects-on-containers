using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebBff.Aggregator.Models.StatusFlow;
using WebBff.Aggregator.Services.StatusFlow;

namespace WebBff.Aggregator.Controllers;


[Authorize]
[Controller]
[Route("StatusFlows")]
public class StatusFlowsController : ControllerBase
{
    private readonly IStatusFlowService _statusFlowService;

    public StatusFlowsController(IStatusFlowService statusFlowService)
    {
        _statusFlowService = statusFlowService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StatusFlowDto>>> GetStatusFlows()
    {
        var statusFlows = await _statusFlowService.GetStatusFlows();
        return Ok(statusFlows);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StatusFlowWithStatusesDto>> GetStatusFlow([FromRoute] string id)
    {
        var statusFlow = await _statusFlowService.GetStatusFlowWithStatuses(id);
        return Ok(statusFlow);
    }

    [HttpGet("group/{id}")]
    public async Task<ActionResult<StatusFlowWithStatusesDto>> GetStatusFlowForGroupOfIssues([FromRoute] string id)
    {
        var statusFlow = await _statusFlowService.GetStatusFlowForGroupOfIssues(id);
        return Ok(statusFlow);
    }

    [HttpPost("status")]
    public async Task<ActionResult> AddStatusToFlow([FromBody] AddStatusToFlowDto dto)
    {
        await _statusFlowService.AddStatusToFlow(dto);
        return NoContent();
    }

    [HttpDelete("status/{statusInFlowId}")]
    public async Task<ActionResult> DeleteStatusFromFlow([FromRoute] string statusInFlowId)
    {
        await _statusFlowService.DeleteStatusFromFlow(statusInFlowId);
        return NoContent();
    }

    [HttpPost("status/{parentStatusInFlowId}/connection/{connectedStatusInFlowId}")]
    public async Task<ActionResult> AddConnectionToStatusInFlow([FromRoute] string parentStatusInFlowId, [FromRoute] string connectedStatusInFlowId)
    {
        await _statusFlowService.AddConnectionToStatusInFlow(parentStatusInFlowId, connectedStatusInFlowId);
        return NoContent();
    }

    [HttpDelete("status/{parentStatusInFlowId}/connection/{connectedStatusInFlowId}")]
    public async Task<ActionResult> RemoveConnectionFromStatusInFlow([FromRoute] string parentStatusInFlowId, [FromRoute] string connectedStatusInFlowId)
    {
        await _statusFlowService.RemoveConnectionFromStatusInFlow(parentStatusInFlowId, connectedStatusInFlowId);
        return NoContent();
    }

    [HttpPut("changeDefaultStatusInFlow")]
    public async Task<ActionResult> ChangeDefaultStatusInFlow([FromQuery] string newStatusInFlowId)
    {
        await _statusFlowService.ChangeDefaultStatusInFlow(newStatusInFlowId);
        return NoContent();
    }

}