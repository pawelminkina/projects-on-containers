using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
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

        #region Get All

        [Test]
        public async Task ShouldReturnUsersForOrganization()
        {
            //GIVEN expected users for organization
            var expected = GetExpectedUsers();

            //AND organization id for which users will be returned
            var organizationId = "BaseOrganization1";

            //WHEN users are retrieved from server
            var getRequest = new GetUsersForOrganizationRequest() { OrganizationId = organizationId };
            var getResponse = await _grpcClient.GetUsersForOrganizationAsync(getRequest);

            //THEN check equality of expected and actual items
            getResponse.Users.Should().BeEquivalentTo(expected);

            #region Local methods

            IEnumerable<User> GetExpectedUsers() =>
                new[]
                {
                    new User() {Id= "BaseUser1", OrganizationId = "BaseOrganization1", Fullname = "Base User1", Username = "baseuser1@poc.com", CreatedDate = new DateTimeOffset(new DateTime(2020,12,22), new TimeSpan(0,1,0,0)).ToTimestamp()},
                    new User() {Id= "BaseUser2", OrganizationId = "BaseOrganization1", Fullname = "Base User2", Username = "baseuser2@poc.com", CreatedDate = new DateTimeOffset(new DateTime(2021,12,22), new TimeSpan(0,1,0,0)).ToTimestamp()}
                };

            #endregion
        }

        #endregion

        #region Get Single


        [Test]
        public async Task ShouldReturnUserById()
        {
            //GIVEN expected user
            var expected = GetExpectedUser();

            //WHEN user is retrieved from server by expected username
            var getRequest = new GetUserByIdRequest() { UserId = expected.Id };
            var getResponse = await _grpcClient.GetUserByIdAsync(getRequest);

            //THEN check equality of expected and actual item
            getResponse.User.Should().BeEquivalentTo(expected);

            #region Local methods

            User GetExpectedUser() =>
                new User() { Id = "BaseUser2", OrganizationId = "BaseOrganization1", Fullname = "Base User2", Username = "baseuser2@poc.com", CreatedDate = new DateTimeOffset(new DateTime(2021, 12, 22), new TimeSpan(0, 1, 0, 0)).ToTimestamp() };

            #endregion        
        }

        [Test]
        public async Task ShouldReturnUserByUsername()
        {
            //GIVEN expected user
            var expected = GetExpectedUser();

            //WHEN user is retrieved from server by expected users username
            var getRequest = new GetUserByUsernameRequest() { Username = expected.Username };
            var getResponse = await _grpcClient.GetUserByUsernameAsync(getRequest);

            //THEN check equality of expected and actual item
            getResponse.User.Should().BeEquivalentTo(expected);

            #region Local methods

            User GetExpectedUser() =>
                new User() { Id = "BaseUser2", OrganizationId = "BaseOrganization1", Fullname = "Base User2", Username = "baseuser2@poc.com", CreatedDate = new DateTimeOffset(new DateTime(2021, 12, 22), new TimeSpan(0, 1, 0, 0)).ToTimestamp() };

            #endregion   
        }

        #endregion

        #region Create

        [Test]
        public async Task ShouldCreateUser()
        {
            //GIVEN expected user
            var expected = GetExpectedUser();

            //AND password with which user will be created
            var password = "SomeComplex123!@#Password";

            //WHEN user is created
            var createRequest = new CreateUserRequest() { Email = expected.Username, OrganizationId = expected.OrganizationId, Password = password, Fullname = expected.Fullname };
            var createResponse = await _grpcClient.CreateUserAsync(createRequest);

            //AND when created user is retrieved from server
            var getRequest = new GetUserByIdRequest() { UserId = createResponse.UserId };
            var getResponse = await _grpcClient.GetUserByIdAsync(getRequest);
            var actual = getResponse.User;

            //AND id and created date of actual is assigned to expected user
            expected.Id = actual.Id;
            expected.CreatedDate = actual.CreatedDate;

            //THEN check equality of expected and actual user
            actual.Should().BeEquivalentTo(expected);

            //AND check that date of creation is today
            actual.CreatedDate.ToDateTime().Date.Should().Be(DateTime.UtcNow.Date);

            #region Local methods

            User GetExpectedUser() =>
                new User() { OrganizationId = "BaseOrganization2", Fullname = "Base User3", Username = "baseuser3@poc.com" };

            #endregion  
        }

        #endregion

        #region Delete

        [Test]
        public async Task ShouldDeleteUser()
        {
            //GIVEN user to delete
            var userToDelete = "BaseUser1";

            //WHEN user is deleted
            var deleteRequest = new DeleteUserRequest() { UserId = userToDelete };
            var deleteResponse = await _grpcClient.DeleteUserAsync(deleteRequest);

            //AND retrieved from server
            var getRequest = new GetUserByIdRequest() { UserId = userToDelete };
            var exception = Assert.ThrowsAsync<RpcException>(async () => await _grpcClient.GetUserByIdAsync(getRequest));

            //THEN check that user was not found
            exception.Status.StatusCode.Should().Be(StatusCode.NotFound);
        }

        #endregion

        #region Change password

        [Test]
        public async Task ShouldChangeUserPassword()
        {
            //GIVEN user for which password will be changed
            var userToChange = "BaseUser1";

            //AND old password
            var oldPassword = "1234";

            //AND new password
            var newPassword = "TurboNewPassword123!@#";

            //WHEN password is changed
            var changePasswordRequest = new ChangePasswordRequest() { NewPassword = newPassword, OldPassword = oldPassword, UserId = userToChange };
            var changePasswordResponse = await _grpcClient.ChangePasswordAsync(changePasswordRequest);

            //AND check id with password match request is send to server
            var checkRequest = new CheckIdAndPasswordMatchesRequest() { Password = newPassword, UserId = userToChange };
            var checkResponse = await _grpcClient.CheckIdAndPasswordMatchesAsync(checkRequest);

            //THEN check response is true
            checkResponse.PasswordMatches.Should().BeTrue();
        }

        #endregion

        #region Email Availability

        [Test]
        public async Task ShouldReturnTrueForCheckEmailAvailability()
        {
            //GIVEN unique email to check
            var email = "emailwhichdontexist@email.com";

            //WHEN email is checked
            var checkRequest = new CheckEmailAvailabilityRequest() { Email = email };
            var checkResponse = await _grpcClient.CheckEmailAvailabilityAsync(checkRequest);

            //THEN check that response was true
            checkResponse.IsAvailable.Should().BeTrue();
        }

        [Test]
        public async Task ShouldReturnFalseForCheckEmailAvailability()
        {
            //GIVEN email which already exist in system to check
            var email = "baseuser1@poc.com";

            //WHEN email is checked
            var checkRequest = new CheckEmailAvailabilityRequest() { Email = email };
            var checkResponse = await _grpcClient.CheckEmailAvailabilityAsync(checkRequest);

            //THEN check that response was false
            checkResponse.IsAvailable.Should().BeFalse();
        }

        #endregion

        #region Id And Password Matches

        [Test]
        public async Task ShouldReturnTrueForCheckIdAndPasswordMatches()
        {
            //GIVEN correct id and password to check
            var id = "BaseUser1";
            var password = "1234";

            //WHEN check request is send
            var checkRequest = new CheckIdAndPasswordMatchesRequest() { Password = password, UserId = id };
            var checkResponse = await _grpcClient.CheckIdAndPasswordMatchesAsync(checkRequest);

            //THEN check that response is true
            checkResponse.PasswordMatches.Should().BeTrue();
        }

        [Test]
        public async Task ShouldFailCheckIdAndPasswordMatchesIncorrectId()
        {
            //GIVEN incorrect id and password to check
            var id = "BaseUser2115";
            var password = "1234";

            //WHEN check request is send
            var checkRequest = new CheckIdAndPasswordMatchesRequest() { Password = password, UserId = id };
            var checkResponse = await _grpcClient.CheckIdAndPasswordMatchesAsync(checkRequest);

            //THEN check that response is false
            checkResponse.PasswordMatches.Should().BeFalse();
        }

        [Test]
        public async Task ShouldFailCheckIdAndPasswordMatchesIncorrectPassword()
        {
            //GIVEN incorrect id and password to check
            var id = "BaseUser1";
            var password = "12345";

            //WHEN check request is send
            var checkRequest = new CheckIdAndPasswordMatchesRequest() { Password = password, UserId = id };
            var checkResponse = await _grpcClient.CheckIdAndPasswordMatchesAsync(checkRequest);

            //THEN check that response is false
            checkResponse.PasswordMatches.Should().BeFalse();
        }

        #endregion
    }
}
