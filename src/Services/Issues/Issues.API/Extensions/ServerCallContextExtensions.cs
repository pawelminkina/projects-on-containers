using System.Security.Claims;
using Grpc.Core;

namespace Issues.API.Extensions
{
    public static class ServerCallContextExtensions
    {
        public static string GetOrganizationId(this ServerCallContext context)
        {
            var user = context.GetHttpContext().User;
            return user.FindFirstValue("organizationId");
        }
        public static string GetUserId(this ServerCallContext context) =>
            context.GetHttpContext().User.FindFirstValue("sub");

        //public static string GetOrganizationId(this ServerCallContext context) =>
        //    "BaseOrganizationId";
        //public static string GetUserId(this ServerCallContext context) =>
        //    "BaseUserId";

    }
}