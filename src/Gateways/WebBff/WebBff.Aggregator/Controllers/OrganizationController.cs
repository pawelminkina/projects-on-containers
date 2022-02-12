using Microsoft.AspNetCore.Mvc;
using WebBff.Aggregator.Models.Organization;
using WebBff.Aggregator.Services.Organization;

namespace WebBff.Aggregator.Controllers;

public class OrganizationController : ControllerBase
{
    private readonly IOrganizationService _organizationService;

    public OrganizationController(IOrganizationService organizationService)
    {
        _organizationService = organizationService;
    }

    [HttpGet]
    public async Task<ActionResult<OrganizationDto>> GetOrganizations()
    {
        var organizations = await _organizationService.GetOrganizations();
        return Ok(organizations);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrganizationDto>> GetOrganization(string id)
    {
        var organization = await _organizationService.GetOrganization(id);
        return Ok(organization);
    }

    [HttpPost]
    public async Task<ActionResult<OrganizationDto>> CreateOrganization(OrganizationForCreationDto dto)
    {
        var newOrganizationId = await _organizationService.AddOrganization(dto);
        return CreatedAtAction(nameof(GetOrganization), new {id = newOrganizationId}, new {id = newOrganizationId});
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOrganization(string id)
    {
        await _organizationService.DeleteOrganization(id);
        return Ok();
    }
}