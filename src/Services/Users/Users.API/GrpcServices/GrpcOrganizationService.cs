using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Users.Core.CQS.Organizations.Commands.AddOrganization;
using Users.Core.CQS.Organizations.Commands.DeleteOrganization;
using Users.Core.CQS.Organizations.Queries.ListOrganizations;
using Users.API.Protos;

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

        public override async Task<OrganizationResponse> GetOrganization(GetOrganizationRequest request, ServerCallContext context)
        {
            throw new RpcException(new Status(StatusCode.Unimplemented, ""));
        }

        public override async Task<ListOrganizationsResponse> ListOrganizations(ListOrganizationsRequest request, ServerCallContext context)
        {
            var organizations = await _mediator.Send(new ListOrganizationsQuery());
            return new ListOrganizationsResponse()
            {
                Organizations = { organizations.Select(o => new OrganizationResponse()
                {
                    Enabled = o.IsEnabled,
                    Id = o.Id,
                    Name = o.Name,
                    CreationDate = Timestamp.FromDateTimeOffset(o.TimeOfCreation)
                })}
            };
        }
    }
}
