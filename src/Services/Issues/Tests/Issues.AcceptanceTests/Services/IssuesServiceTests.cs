using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Issues.AcceptanceTests.Base;
using Issues.API.Protos;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

namespace Issues.AcceptanceTests.Services
{
    public class IssuesServiceTests : IssuesTestServer
    {
        private IssueService.IssueServiceClient _grpcClient;
        private TestServer _server;

        [SetUp]
        public void Setup()
        {
            _server = CreateServer();
            var channel = GetGrpcChannel(_server);
            _grpcClient = new IssueService.IssueServiceClient(channel);
        }

        [Test]
        public async Task ShouldReturnIssuesForGroup()
        {

        }

        [Test]
        public async Task ShouldReturnIssuesForUser()
        {

        }

        [Test]
        public async Task ShouldReturnIssueWithContent()
        {

        }

        [Test]
        public async Task ShouldCreateIssue()
        {

        }

        [Test]
        public async Task ShouldRenameIssue()
        {

        }

        [Test]
        public async Task ShouldUpdateIssueContent()
        {

        }

        [Test]
        public async Task ShouldSetIssueStatusToDeleted()
        {

        }
    }
}
