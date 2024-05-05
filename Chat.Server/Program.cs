using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000);
    // WORKAROUND: Accept HTTP/2 only to allow insecure HTTP/2 connections during development.
    options.ConfigureEndpointDefaults(endpointOptions =>
    {
        endpointOptions.Protocols = HttpProtocols.Http2;
    });
});
builder.Services.AddGrpc();  // MagicOnion depends on ASP.NET Core gRPC service.
builder.Services.AddMagicOnion();

var app = builder.Build();
app.MapMagicOnionService();

app.Run();