namespace GrpcServer.API.Interceptors;

using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;

public class ServiceInterceptor : Interceptor
{
    private readonly ILogger<ServiceInterceptor> _logger;

    public ServiceInterceptor(ILogger<ServiceInterceptor> logger) =>
        _logger = logger;

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
    {
        try
        {
            // Do Somthing
            return await continuation(request, context);
            // Do Somthing
        }
        catch (Exception ex)
        {
            var correlationId = Guid.NewGuid().ToString();
            var trailers = new Metadata();
            trailers.Add("CorrelationId", correlationId);
            throw new RpcException(new Status(StatusCode.Internal, ex.Message), trailers, "...");
        }
    }

    public override async Task<TResponse> ClientStreamingServerHandler<TRequest, TResponse>(IAsyncStreamReader<TRequest> requestStream, ServerCallContext context, ClientStreamingServerMethod<TRequest, TResponse> continuation)
    {
        try
        {
            // Do Somthing
            return await continuation(requestStream, context);
            // Do Somthing
        }
        catch (Exception ex)
        {
            var correlationId = Guid.NewGuid().ToString();
            var trailers = new Metadata();
            trailers.Add("CorrelationId", correlationId);
            throw new RpcException(new Status(StatusCode.Internal, ex.Message), trailers, "...");
        }
    }

    public override async Task ServerStreamingServerHandler<TRequest, TResponse>(TRequest request, IServerStreamWriter<TResponse> responseStream, ServerCallContext context, ServerStreamingServerMethod<TRequest, TResponse> continuation)
    {
        try
        {
            // Do Somthing
            await continuation(request, responseStream, context);
            // Do Somthing
        }
        catch (Exception ex)
        {
            var correlationId = Guid.NewGuid().ToString();
            var trailers = new Metadata();
            trailers.Add("CorrelationId", correlationId);
            throw new RpcException(new Status(StatusCode.Internal, ex.Message), trailers, "...");
        }
    }

    public override async Task DuplexStreamingServerHandler<TRequest, TResponse>(IAsyncStreamReader<TRequest> requestStream, IServerStreamWriter<TResponse> responseStream, ServerCallContext context, DuplexStreamingServerMethod<TRequest, TResponse> continuation)
    {
        try
        {
            // Do Somthing
            await continuation(requestStream, responseStream, context);
            // Do Somthing
        }
        catch (Exception ex)
        {
            var correlationId = Guid.NewGuid().ToString();
            var trailers = new Metadata();
            trailers.Add("CorrelationId", correlationId);
            throw new RpcException(new Status(StatusCode.Internal, ex.Message), trailers, "...");
        }
    }
}
