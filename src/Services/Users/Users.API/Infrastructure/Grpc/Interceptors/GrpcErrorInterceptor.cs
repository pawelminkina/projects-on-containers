using Architecture.DDD.Exceptions;
using Grpc.Core;
using Grpc.Core.Interceptors;
using User.Core.Exceptions;

namespace Users.API.Infrastructure.Grpc.Interceptors
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
                _logger.LogError(domainException, "Error thrown by {context.Method}. Exception:{ex}.", context.Method, domainException);
                throw new RpcException(new Status(StatusCode.Internal, domainException.Message));
            }
            catch (NotFoundException notFoundException)
            {
                _logger.LogError(notFoundException, "Error thrown by {context.Method}. Exception:{ex}.", context.Method, notFoundException);
                throw new RpcException(new Status(StatusCode.NotFound, notFoundException.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error thrown by {context.Method}. Exception:{ex}.", context.Method, ex);
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }

        private void LogCall(ServerCallContext context)
        {
            var httpContext = context.GetHttpContext();
            _logger.LogDebug($"Starting call. Request: {httpContext.Request.Path}");
        }
    }
}
