using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Users.Core.CQRS.Organizations.Commands.AddOrganization;
using Users.Core.CQRS.Organizations.Commands.DeleteOrganization;
using Users.API.Protos;
using Users.Core.CQRS.Organizations.Queries.GetOrganization;
using Users.Core.CQRS.Organizations.Queries.GetOrganizations;
using Users.Core.Domain;

namespace Users.API.GrpcServices
{
    [Authorize]
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

        public override async Task<GetOrganizationsResponse> GetOrganizations(GetOrganizationsRequest request, ServerCallContext context)
        {
            var organizations = await _mediator.Send(new GetOrganizationsQuery());
            return new GetOrganizationsResponse()
            {
                Organizations = { organizations.Select(MapToResponse) }
            };
        }

        public override async Task<GetOrganizationResponse> GetOrganization(GetOrganizationRequest request, ServerCallContext context)
        {
            var organization = await _mediator.Send(new GetOrganizationQuery(request.OrganizationId));
            return new GetOrganizationResponse() {Organization = MapToResponse(organization)};
        }

        private Protos.Organization MapToResponse(Core.Domain.Organization organization) => new()
        {
            Id = organization.Id,
            Name = organization.Name,
            CreationDate = Timestamp.FromDateTimeOffset(organization.TimeOfCreation),
        };
    }
}
