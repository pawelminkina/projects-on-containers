using Users.API.Protos;
using WebBff.Aggregator.Models.Organization;

namespace WebBff.Aggregator.Services.Organization;

public class GrpcOrganizationService : IOrganizationService
{
    private readonly OrganizationService.OrganizationServiceClient _grpcClient;

    public GrpcOrganizationService(OrganizationService.OrganizationServiceClient grpcClient)
    {
        _grpcClient = grpcClient;
    }
    public async Task<IEnumerable<OrganizationDto>> GetOrganizations()
    {
        var response = await _grpcClient.GetOrganizationsAsync(new GetOrganizationsRequest());
        return response.Organizations.Select(MapToDto);
    }

    public async Task<OrganizationDto> GetOrganization(string id)
    {
        var response = await _grpcClient.GetOrganizationAsync(new GetOrganizationRequest() {OrganizationId = id});
        return MapToDto(response.Organization);
    }

    public async Task<AddOrganizationResponseDto> AddOrganization(OrganizationForCreationDto dto)
    {
        var response = await _grpcClient.AddOrganizationAsync(new AddOrganizationRequest() {Name = dto.Name});
        return new AddOrganizationResponseDto()
        {
            DefaultUserName = response.DefaultUserName,
            DefaultUserPassword = response.DefaultUserPassword,
            OrganizationId = response.OrganizationId
        };
    }

    public async Task DeleteOrganization(string id)
    {
        await _grpcClient.DeleteOrganizationAsync(new DeleteOrganizationRequest() {OrganizationId = id});
    }

    private OrganizationDto MapToDto(Users.API.Protos.Organization organization)
    {
        return new OrganizationDto()
        {
            CreateDate = organization.CreationDate.ToDateTimeOffset(),
            Id = organization.Id,
            Name = organization.Name
        };
    }
}