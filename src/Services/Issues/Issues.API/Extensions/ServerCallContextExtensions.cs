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
            "pocOrgan-70cf-4a4d-bcd8-1241e3bcce0b";
        public static string GetUserId(this ServerCallContext context) =>
            "pocUserI-3933-4141-b747-58830e249a36";

    }
}