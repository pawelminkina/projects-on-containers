using Grpc.Core;
using Grpc.Core.Interceptors;
using WebBff.Aggregator.Infrastructure.Auth;

namespace WebBff.Aggregator.Infrastructure.Grpc.Interceptors
{
    public class JwtTokenForwardingInterceptor : Interceptor
    {

        private readonly IInternalJwtTokenFactory _internalJwtTokenFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<JwtTokenForwardingInterceptor> _logger;

        public JwtTokenForwardingInterceptor(IInternalJwtTokenFactory internalJwtTokenFactory, IHttpContextAccessor httpContextAccessor, ILogger<JwtTokenForwardingInterceptor> logger)
        {
            _internalJwtTokenFactory = internalJwtTokenFactory;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context,
            AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            var jwtToken = _httpContextAccessor.HttpContext?.Request.Headers
                .FirstOrDefault(h => h.Key.ToUpper().Equals("Authorization".ToUpper()))
                .Value.ToString()
                ?.Split(' ')
                ?.ElementAtOrDefault(1);

            if (string.IsNullOrWhiteSpace(jwtToken))
                return base.AsyncUnaryCall(request, context, continuation);
            
            var internalJwtToken = _internalJwtTokenFactory.GetInternalJwtTokenAsync(jwtToken).Result;

            _logger.LogDebug("Forwarding JWT token for {Method}: {jwtToken} | {internalJwtToken}", context.Method.FullName, jwtToken, internalJwtToken);

            var callOptions = context.Options.WithHeaders(new Metadata()
            {
                {"Authorization", $"{internalJwtToken}"}
            });

            var newContext = new ClientInterceptorContext<TRequest, TResponse>(context.Method, context.Host, callOptions);

            return base.AsyncUnaryCall(request, newContext, continuation);

        }
    }
}
