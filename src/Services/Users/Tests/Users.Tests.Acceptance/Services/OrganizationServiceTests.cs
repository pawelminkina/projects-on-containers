using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore.Query.Internal;
using NUnit.Framework;
using Users.API.Protos;
using Users.Tests.Core.Base;

namespace Users.Tests.Acceptance.Services
{
    public class OrganizationServiceTests : UsersTestServer
    {
        private OrganizationService.OrganizationServiceClient _grpcClient;
        private TestServer _server;

        [SetUp]
        public void Setup()
        {
            _server = CreateServer();
            var channel = GetGrpcChannel(_server);
            _grpcClient = new OrganizationService.OrganizationServiceClient(channel);
        }

        #region Get All

        [Test]
        public async Task ShouldReturnOrganizations()
        {
            //GIVEN expected collection of organizations
            var expectedGroupOfIssues = GetExpectedOrganizations();

            //WHEN organizations are retrieved from server
            var request = new GetOrganizationsRequest();
            var response = await _grpcClient.GetOrganizationsAsync(request);
            var actual = response.Organizations;

            //THEN check equality of expected and actual items
            actual.Should().BeEquivalentTo(expectedGroupOfIssues);

            #region Local methods

            IEnumerable<Organization> GetExpectedOrganizations() =>
                new[]
                {
                    new Organization() {Enabled = true, Id = "BaseOrganization1", Name = "Base Organization 1", CreationDate = new DateTimeOffset(new DateTime(2020,12,22), new TimeSpan(0,1,0,0)).ToTimestamp()},
                    new Organization() {Enabled = true, Id = "BaseOrganization2", Name = "Base Organization 2", CreationDate = new DateTimeOffset(new DateTime(2021,12,22), new TimeSpan(0,1,0,0)).ToTimestamp()}
                };

            #endregion
        }

        #endregion

        #region Get Single

        [Test]
        public async Task ShouldReturnOrganization()
        {
            //GIVEN expected organization
            var expected = GetExpectedOrganization();

            //WHEN organization is retrieved from server
            var getRequest = new GetOrganizationRequest() { OrganizationId = expected.Id };
            var getResponse = await _grpcClient.GetOrganizationAsync(getRequest);
            var actual = getResponse.Organization;

            //THEN check equality of expected and actual organization
            actual.Should().BeEquivalentTo(expected);

            #region Local methods

            Organization GetExpectedOrganization() =>
                new()
                {
                    Enabled = true,
                    Id = "BaseOrganization1",
                    Name = "Base Organization 1",
                    CreationDate = new DateTimeOffset(new DateTime(2020, 12, 22), new TimeSpan(0, 1, 0, 0)).ToTimestamp()
                };

            #endregion
        }

        #endregion

        #region Create

        [Test]
        public async Task ShouldCreateOrganization()
        {
            //GIVEN expected organization to create
            var expected = GetExpectedOrganization();

            //WHEN organization is created
            var createRequest = new AddOrganizationRequest() { Name = expected.Name };
            var createResponse = await _grpcClient.AddOrganizationAsync(createRequest);

            //AND retrieved from server
            var getRequest = new GetOrganizationRequest() { OrganizationId = createResponse.OrganizationId };
            var getResponse = await _grpcClient.GetOrganizationAsync(getRequest);
            var actual = getResponse.Organization;

            //AND id and date of created is assigned to expected
            expected.Id = actual.Id;
            expected.CreationDate = actual.CreationDate;

            //THEN check equality of expected and actual organization
            actual.Should().BeEquivalentTo(expected);

            //AND check that date of creation is today
            actual.CreationDate.ToDateTime().Date.Should().Be(DateTime.UtcNow.Date);

            #region Local methods

            Organization GetExpectedOrganization() =>
                new()
                {
                    Enabled = true,
                    Name = "New Organization",
                    CreationDate = DateTimeOffset.UtcNow.ToTimestamp()
                };

            #endregion
        }

        #endregion

        #region Delete

        [Test]
        public async Task ShouldDeleteOrganization()
        {
            //GIVEN organization to delete
            var organizationToDelete = "BaseOrganization1";

            //WHEN organization is deleted
            var deleteRequest = new DeleteOrganizationRequest() { OrganizationId = organizationToDelete };
            var deleteResponse = await _grpcClient.DeleteOrganizationAsync(deleteRequest);

            //AND it's retrieved from server
            var getRequest = new GetOrganizationRequest() { OrganizationId = organizationToDelete };
            var exception = Assert.ThrowsAsync<RpcException>(async () => await _grpcClient.GetOrganizationAsync(getRequest));

            //THEN check does it exist
            exception.Status.StatusCode.Should().Be(StatusCode.NotFound);
        }

        #endregion

    }
}
