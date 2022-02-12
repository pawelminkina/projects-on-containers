using Microsoft.AspNetCore.Mvc;
using WebBff.Aggregator.Services.StatusFlow;

namespace WebBff.Aggregator.Controllers;

public class StatusFlowController : ControllerBase
{
    private readonly IStatusFlowService _statusFlowService;

    public StatusFlowController(IStatusFlowService statusFlowService)
    {
        _statusFlowService = statusFlowService;
    }
}