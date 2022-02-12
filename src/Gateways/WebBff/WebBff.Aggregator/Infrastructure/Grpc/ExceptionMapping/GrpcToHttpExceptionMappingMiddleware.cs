using System.Net;
using Grpc.Core;

namespace WebBff.Aggregator.Infrastructure.Grpc.ExceptionMapping
{
    public class GrpcToHttpExceptionMappingMiddleware
    {
        private readonly RequestDelegate _next;

        public GrpcToHttpExceptionMappingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ILogger<GrpcToHttpExceptionMappingMiddleware> logger)
        {
            try
            {
                await _next(httpContext);
            }
            catch (RpcException rpcException)
            {
                var defaultMappers = new Dictionary<StatusCode, HttpStatusCode>()
                {
                    {StatusCode.NotFound, HttpStatusCode.NotFound},
                    {StatusCode.InvalidArgument, HttpStatusCode.BadRequest},
                    {StatusCode.Unimplemented,HttpStatusCode.NotImplemented},
                    {StatusCode.Unauthenticated, HttpStatusCode.Unauthorized},
                    {StatusCode.PermissionDenied, HttpStatusCode.Unauthorized}
                };

                if (defaultMappers.ContainsKey(rpcException.StatusCode))
                {
                    logger.LogInformation("Handling Grpc Exception (Code: {code}): {@rpcException}", rpcException.StatusCode, rpcException);

                    httpContext.Response.StatusCode = (int)defaultMappers[rpcException.StatusCode];
                    if (!string.IsNullOrWhiteSpace(rpcException.Status.Detail))
                        await httpContext.Response.WriteAsJsonAsync(new { Details = rpcException.Status.Detail });
                }
                else
                    throw;
            }
        }
    }
}
