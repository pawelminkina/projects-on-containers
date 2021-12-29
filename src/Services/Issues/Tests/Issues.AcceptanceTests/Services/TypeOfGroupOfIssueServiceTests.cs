using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Issues.AcceptanceTests.Base;
using Issues.API.Protos;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Issues.AcceptanceTests.Services
{
    public class TypeOfGroupOfIssueServiceTests : IssuesTestServer
    {
        private TypeOfGroupOfIssueService.TypeOfGroupOfIssueServiceClient _grpcClient;
        private TestServer _server;

        [SetUp]
        public void Setup()
        {
            _server = CreateServer();
            var channel = GetGrpcChannel(_server);
            _grpcClient = new TypeOfGroupOfIssueService.TypeOfGroupOfIssueServiceClient(channel);
        }

        [Test]
        public async Task ShouldReturnTypesOfGroupsOfIssues()
        {
            //GIVEN expected collection of types of groups
            var expectedTypesOfGroupOfIssues = GetExpectedTypesOfGroupOfIssues();
            
            //WHEN groups are retrieved from server
            var request = new GetTypesOfGroupsOfIssuesRequest();
            var response = await _grpcClient.GetTypesOfGroupsOfIssuesAsync(request);
            var actual = response.Types_;

            //THEN check equality of expected and actual items
            actual.Should().BeEquivalentTo(expectedTypesOfGroupOfIssues);

            #region Local methods

            IEnumerable<TypeOfGroupOfIssues> GetExpectedTypesOfGroupOfIssues() => new[]
            {
                new TypeOfGroupOfIssues() {Id = "001-001", IsArchived = false, Name = "Type Of Group Of Issues 1"},
                new TypeOfGroupOfIssues() {Id = "001-002", IsArchived = false, Name = "Type Of Group Of Issues 2"},
            };

            #endregion

        }
    }
}
