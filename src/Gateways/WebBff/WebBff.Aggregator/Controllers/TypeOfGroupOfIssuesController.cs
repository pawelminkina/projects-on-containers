using Microsoft.AspNetCore.Mvc;
using WebBff.Aggregator.Services.TypeOfGroupOfIssues;

namespace WebBff.Aggregator.Controllers;

public class TypeOfGroupOfIssuesController : ControllerBase
{
    private readonly ITypeOfGroupOfIssuesService _typeOfGroupOfIssuesService;

    public TypeOfGroupOfIssuesController(ITypeOfGroupOfIssuesService typeOfGroupOfIssuesService)
    {
        _typeOfGroupOfIssuesService = typeOfGroupOfIssuesService;
    }
}