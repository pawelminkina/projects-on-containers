using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.TestHost;
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
             new []
             {
                 new Organization() {Enabled = true, Id = "BaseOrganization1", Name = "Base Organization 1", CreationDate = new DateTimeOffset(new DateTime(2020,12,22), new TimeSpan(0,1,0,0)).ToTimestamp()},
                 new Organization() {Enabled = true, Id = "BaseOrganization2", Name = "Base Organization 2", CreationDate = new DateTimeOffset(new DateTime(2021,12,22), new TimeSpan(0,1,0,0)).ToTimestamp()}
             };

            #endregion
        }

    }
}
