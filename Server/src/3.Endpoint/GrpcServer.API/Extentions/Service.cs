namespace GrpcServer.API.Extentions;

using Microsoft.EntityFrameworkCore;
using Sky.Kernel.Filing.Wireup;
using Sky.Kernel.Identity.Wireup;
using Sky.Kernel.Hashing.Wireup;
using Sky.Kernel.Serializing.Wireup;
using Sky.App.Endpoint.Api.Extentions;
using Sky.App.Infra.Data.Sql.Command.Interceptors;
using Services.v1;
using Interceptors;
using Infra.Data.Sql.Query.Contexts;
using Infra.Data.Sql.Command.Contexts;
using GrpcServer.API.ServicesExposer;

internal static class Service
{
    internal static void Host(string[] args) => WebApplication.CreateBuilder(args).Services().Middlewares();

    private static WebApplication Services(this WebApplicationBuilder source)
    {
        var configuration = source.Configuration;

        var commandDbConn = configuration.GetConnectionString("GrpcServerCommandDbConn");
        var queryDbConn = configuration.GetConnectionString("GrpcServerQueryDbConn");

        source.Services.AddGrpc(_ =>
        {
            _.EnableDetailedErrors = true;
            _.Interceptors.Add<ServiceInterceptor>();
        });

        source
        .Services
        .AddGrpcReflection()
       .AddDbContext<GrpcServerCommandDbContext>(_ =>
       {
           _
           .UseSqlServer(commandDbConn)
           .AddInterceptors(new CommandDbContextInterceptor());
       })
       .AddDbContext<GrpcServerQueryDbContext>(_ =>
       {
           _.UseSqlServer(queryDbConn);
       })
       .FilerWireup()
       .UploaderWireup()
       .NewtonSoftSerializerWireup()
       .BCryptHashingWireup()
       .FakeUserServiceWireup()
       .WebApiWireup("Sky", "GrpcServer")
       .AddEndpointsApiExplorer()
       .AddSwaggerGen()
       .AddSingleton<ProtoFileExposer>()
       .AddHttpContextAccessor();

        return source.Build();
    }

    private static void Middlewares(this WebApplication source)
    {
        source.MapGrpcReflectionService();
        source.MapGrpcService<PersonGrpcService>();
        source.ProtoExposer();
        source.Run();
    }
}
