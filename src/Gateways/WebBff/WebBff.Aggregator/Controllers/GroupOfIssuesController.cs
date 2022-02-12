using Microsoft.AspNetCore.Mvc;
using WebBff.Aggregator.Services.GroupOfIssues;

namespace WebBff.Aggregator.Controllers;

public class GroupOfIssuesController : ControllerBase
{
    private readonly IGroupOfIssuesService _groupOfIssuesService;

    public GroupOfIssuesController(IGroupOfIssuesService groupOfIssuesService)
    {
        _groupOfIssuesService = groupOfIssuesService;
    }
}