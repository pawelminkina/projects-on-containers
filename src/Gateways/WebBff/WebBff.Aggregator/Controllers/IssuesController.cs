using Microsoft.AspNetCore.Mvc;
using WebBff.Aggregator.Services.Issues;

namespace WebBff.Aggregator.Controllers;

public class IssuesController : ControllerBase
{
    private readonly IIssuesService _issuesService;

    public IssuesController(IIssuesService issuesService)
    {
        _issuesService = issuesService;
    }
}