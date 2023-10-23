using GrpcClient.Core.AppService;
using GrpcClient.Infra.Repositories;
using GrpcClient.Core.Contract.Infra;
using GrpcClient.Core.Contract.AppService.Services;
using static GrpcClient.Infra.Protos.v1.PersonService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IPersonRepository, PersonRepository>();
builder.Services.AddTransient<IPersonService, PersonService>();
builder.Services.AddGrpcClient<PersonServiceClient>(_ =>
{
    _.Address = new Uri("https://localhost:7440");
    _.ChannelOptionsActions.Add(opt =>
    {
        opt.MaxReceiveMessageSize = 5_242_880;
        opt.MaxSendMessageSize = 5_242_880;
        //channelOpt.LoggerFactory = ...;
        opt.HttpHandler = new SocketsHttpHandler
        {
            KeepAlivePingDelay = TimeSpan.FromSeconds(20),
            KeepAlivePingTimeout = TimeSpan.FromSeconds(10),
            PooledConnectionIdleTimeout = TimeSpan.FromMinutes(10),
            EnableMultipleHttp2Connections = true
        };
    });
});
builder.Services.AddRazorPages();


var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
