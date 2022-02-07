using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Users.Core.CQS.Organizations.Commands.AddOrganization;
using Users.Core.CQS.Organizations.Commands.DeleteOrganization;
using Users.Core.CQS.Organizations.Queries.ListOrganizations;
using Users.API.Protos;
using Users.Core.CQS.Organizations.Queries.GetOrganization;
using Users.Core.Domain;

namespace Users.API.GrpcServices
{
    public class GrpcOrganizationService : Protos.OrganizationService.OrganizationServiceBase
    {
        private readonly IMediator _mediator;

        public GrpcOrganizationService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public override async Task<AddOrganizationResponse> AddOrganization(AddOrganizationRequest request, ServerCallContext context)
        {
            var organizationId = await _mediator.Send(new AddOrganizationCommand(request.Name));
            return new AddOrganizationResponse() { OrganizationId = organizationId };
        }

        public override async Task<DeleteOrganizationResponse> DeleteOrganization(DeleteOrganizationRequest request, ServerCallContext context)
        {
            await _mediator.Send(new DeleteOrganizationCommand(request.OrganizationId));
            return new DeleteOrganizationResponse();
        }

        public override async Task<OrganizationResponse> GetOrganization(GetOrganizationRequest request, ServerCallContext context)
        {
            var organization = await _mediator.Send(new GetOrganizationQuery(request.OrganizationId));
            return MapToResponse(organization);
        }

        public override async Task<ListOrganizationsResponse> ListOrganizations(ListOrganizationsRequest request, ServerCallContext context)
        {
            var organizations = await _mediator.Send(new ListOrganizationsQuery());
            return new ListOrganizationsResponse()
            {
                Organizations = { organizations.Select(MapToResponse)}
            };
        }

        private OrganizationResponse MapToResponse(Organization organization) => new OrganizationResponse()
        {
            Enabled = organization.IsEnabled,
            Id = organization.Id,
            Name = organization.Name,
            CreationDate = Timestamp.FromDateTimeOffset(organization.TimeOfCreation)
        };
    }
}
