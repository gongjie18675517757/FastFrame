using HttpMouse;
using HttpMouse.Implementions;
using HttpMouse.ServerHost;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading;


var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

services.AddHttpMouse<ProxyConfigStoreProvider>();
services.AddLogging(r => r.AddLog4Net());

var app = builder.Build();
var applicationLifetime = app.Lifetime;

app.UseHttpMouse();
app.UseRouting();
app.MapReverseProxy(endpoints =>
{
    //endpoints.UseEndpoints(endpoints =>
    //{
    //    endpoints.MapFallback(async context =>
    //    {
    //        context.Response.StatusCode = 502;
    //        context.Response.ContentType = "text/html";
    //        await context.Response.WriteAsync(Resources.Page_NotFund, CancellationToken.None);
    //    });
    //});
});


applicationLifetime.ApplicationStarted.Register(InitProxyConfig, app);



void InitProxyConfig(object? app)
{
    if (app is IApplicationBuilder builder)
    {
        builder.ApplicationServices.GetService<IProxyConfigStoreProvider>()?.UpdateProxys(new[] {
                    new ProxyConfig("httpmouse","test.test.cc","http://127.0.0.1:80")
                });
    }
}


app.Run();