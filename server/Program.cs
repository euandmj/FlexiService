using core;
using server.Handlers;
using server.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddScoped<IFlexiHandlerService, FlexiHandlerService>(ctx => new FlexiHandlerService(Array.Empty<Assembly>(), new[] { typeof(SomeHandlers)}));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<FlexiServiceImpl>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
