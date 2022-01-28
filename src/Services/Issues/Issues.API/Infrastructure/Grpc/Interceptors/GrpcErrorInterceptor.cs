using System;
using System.Threading.Tasks;
using Architecture.DDD.Exceptions;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Issues.Application.Common.Exceptions;
using Microsoft.Extensions.Logging;

namespace Issues.API.Infrastructure.Grpc.Interceptors
{
    public class GrpcErrorInterceptor : Interceptor
    {
        private readonly ILogger<GrpcErrorInterceptor> _logger;

        public GrpcErrorInterceptor(ILogger<GrpcErrorInterceptor> logger)
        {
            _logger = logger;
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            LogCall(context);

            try
            {
                return await continuation(request, context);
            }
            catch (DomainException domainException)
            {
                throw new RpcException(new Status(StatusCode.Internal, domainException.Message));
            }
            catch (NotFoundException notFoundException)
            {
                throw new RpcException(new Status(StatusCode.NotFound, notFoundException.Message));
            }
            catch (PermissionDeniedException permissionDeniedException)
            {
                throw new RpcException(new Status(StatusCode.PermissionDenied, permissionDeniedException.Message));
            }
            catch (Exception ex)
            {
                // Note: The gRPC framework also logs exceptions thrown by handlers to .NET Core logging.
                _logger.LogError(ex, $"Error thrown by {context.Method}.");

                throw new RpcException(Status.DefaultCancelled, ex.Message);
            }
        }

        private void LogCall(ServerCallContext context)
        {
            var httpContext = context.GetHttpContext();
            _logger.LogDebug($"Starting call. Request: {httpContext.Request.Path}");
        }
    }
}
