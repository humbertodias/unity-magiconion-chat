using Grpc.Net.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();

        services.AddGrpc(); // MagicOnion depends on ASP.NET Core gRPC service.
        services.AddMagicOnion();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapMagicOnionHttpGateway("_", app.ApplicationServices.GetService<MagicOnion.Server.MagicOnionServiceDefinition>().MethodHandlers, GrpcChannel.ForAddress("https://localhost:5001"));
            endpoints.MapMagicOnionSwagger("swagger", app.ApplicationServices.GetService<MagicOnion.Server.MagicOnionServiceDefinition>().MethodHandlers, "/_/");

            endpoints.MapMagicOnionService();
        });
    }
}