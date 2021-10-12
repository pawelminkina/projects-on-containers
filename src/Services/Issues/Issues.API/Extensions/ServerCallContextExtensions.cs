using System.Security.Claims;
using Grpc.Core;

namespace Issues.API.Extensions
{
    public static class ServerCallContextExtensions
    {
        //public static string GetOrganizationId(this ServerCallContext context) =>
        //    context.GetHttpContext().User.FindFirstValue("organizationId");
        //public static string GetUserId(this ServerCallContext context) =>
        //    context.GetHttpContext().User.FindFirstValue("sub");

        public static string GetOrganizationId(this ServerCallContext context) =>
            "MOCKEDORGANIZATION";
        public static string GetUserId(this ServerCallContext context) =>
            "MOCKEDUSER";

    }
}