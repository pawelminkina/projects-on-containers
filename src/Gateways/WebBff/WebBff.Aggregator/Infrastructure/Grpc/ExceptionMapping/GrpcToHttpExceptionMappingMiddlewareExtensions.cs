namespace WebBff.Aggregator.Infrastructure.Grpc.ExceptionMapping
{
    public static class GrpcToHttpExceptionMappingMiddlewareExtensions
    {
        public static IApplicationBuilder UseGrpcToHttpExceptionMapping(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GrpcToHttpExceptionMappingMiddleware>();
        }
    }
}
