using Grpc.Core;
using Users.API.Protos;

namespace Users.API.GrpcServices
{
    public class GrpcOrganizationService : Protos.OrganizationService.OrganizationServiceBase
    {
        public override async Task<AddOrganizationResponse> AddOrganization(AddOrganizationRequest request, ServerCallContext context)
        {
            return await base.AddOrganization(request, context);
        }

        public override async Task<DeleteOrganizationResponse> DeleteOrganization(DeleteOrganizationRequest request, ServerCallContext context)
        {
            return await base.DeleteOrganization(request, context);
        }

        public override async Task<OrganizationResponse> GetOrganization(GetOrganizationRequest request, ServerCallContext context)
        {
            return await base.GetOrganization(request, context);
        }

        public override async Task<ListOrganizationsResponse> ListOrganizations(ListOrganizationsRequest request, ServerCallContext context)
        {
            return await base.ListOrganizations(request, context);
        }
    }
}
