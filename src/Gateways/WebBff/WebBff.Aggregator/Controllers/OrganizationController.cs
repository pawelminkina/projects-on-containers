using Microsoft.AspNetCore.Mvc;
using WebBff.Aggregator.Services.Organization;

namespace WebBff.Aggregator.Controllers;

public class OrganizationController : ControllerBase
{
    private readonly IOrganizationService _organizationService;

    public OrganizationController(IOrganizationService organizationService)
    {
        _organizationService = organizationService;
    }
}