using Auth.API.Infrastructure.Auth;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Auth.API.Infrastructure.Grpc.Interceptors
{
    public class JwtTokenForwardingInterceptor : Interceptor
    {
        private readonly ILogger<JwtTokenForwardingInterceptor> _logger;
        private readonly IInternalJwtTokenFactory _internalJwtTokenFactory;

        public JwtTokenForwardingInterceptor(ILogger<JwtTokenForwardingInterceptor> logger, IInternalJwtTokenFactory internalJwtTokenFactory)
        {
            _logger = logger;
            _internalJwtTokenFactory = internalJwtTokenFactory;
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context,
            AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            var internalJwtToken = _internalJwtTokenFactory.GetInternalJwtTokenAsync().Result;

            if (!string.IsNullOrWhiteSpace(internalJwtToken))
            {
                _logger.LogDebug("Forwarding JWT token for {Method}: {internalJwtToken}", context.Method.FullName, internalJwtToken);

                var callOptions = context.Options.WithHeaders(new Metadata()
                {
                    {"Authorization", $"{internalJwtToken}"}
                });

                var newContext = new ClientInterceptorContext<TRequest, TResponse>(context.Method, context.Host, callOptions);

                return base.AsyncUnaryCall(request, newContext, continuation);
            }

            return base.AsyncUnaryCall(request, context, continuation);
        }
    }
}
