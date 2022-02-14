using WebBff.Aggregator.Models.Organization;

namespace WebBff.Aggregator.Services.Organization
{
    public interface IOrganizationService
    {
        Task<IEnumerable<OrganizationDto>> GetOrganizations();
        Task<OrganizationDto> GetOrganization(string id);
        Task<AddOrganizationResponseDto> AddOrganization(OrganizationForCreationDto dto);
        Task DeleteOrganization(string id);
    }
}
