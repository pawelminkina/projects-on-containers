using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using Users.API.Protos;
using Users.Tests.Core.Base;

namespace Users.Tests.Acceptance.Services
{
    public class UserServiceTests : UsersTestServer
    {
        private UserService.UserServiceClient _grpcClient;
        private TestServer _server;

        [SetUp]
        public void Setup()
        {
            _server = CreateServer();
            var channel = GetGrpcChannel(_server);
            _grpcClient = new UserService.UserServiceClient(channel);
        }

        [Test]
        public async Task ShouldReturnUsersForOrganization()
        {
            //GIVEN expected users for organization

            //WHEN users are retrieved from server

            //THEN check equality of expected and actual items
            
            Assert.Fail();
        }

        [Test]
        public async Task ShouldReturnUserById()
        {
            //GIVEN expected user

            //WHEN user is retrieved from server by expected username

            //THEN check equality of expected and actual item

            Assert.Fail();
        }

        [Test]
        public async Task ShouldReturnUserByUsername()
        {
            //GIVEN expected user

            //WHEN user is retrieved from server by expected users username

            //THEN check equality of expected and actual item

            Assert.Fail();
        }

        [Test]
        public async Task ShouldCreateUser()
        {
            //GIVEN expected user

            //WHEN user is created

            //AND when created user is retrieved from server

            //AND id of actual is assigned to expected user

            //THEN check equality of expected and actual user
            Assert.Fail();

        }

        [Test]
        public async Task ShouldDeleteUser()
        {
            //GIVEN user to delete

            //WHEN user is deleted

            //AND retrieved from server

            //THEN check that user was not found
            Assert.Fail();

        }

        [Test]
        public async Task ShouldReturnTrueForCheckEmailAvailability()
        {
            //GIVEN unique email to check

            //WHEN email is checked

            //THEN check that response was true
            Assert.Fail();
        }

        [Test]
        public async Task ShouldReturnFalseForCheckEmailAvailability()
        {
            //GIVEN email which already exist in system to check

            //WHEN email is checked

            //THEN check that response was false
            Assert.Fail();
        }

        [Test]
        public async Task ShouldReturnTrueForCheckIdAndPasswordMatches()
        {
            //GIVEN correct id and password to check

            //WHEN check request is send

            //THEN check that response is true
            Assert.Fail();
        }

        [Test]
        public async Task ShouldReturnFalseForCheckIdAndPasswordMatches()
        {
            //GIVEN incorrect id and password to check

            //WHEN check request is send

            //THEN check that response is false
            Assert.Fail();
        }
    }
}
