using HttpMouse.Client;
using HttpMouse.ClientHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((ctx,services) =>
    {
        services.AddHttpMouseClient(ctx.Configuration.GetSection("HttpMouse"));
        services.AddHostedService<HttpMouseClientHostedService>();
    })
    .ConfigureLogging((HostBuilderContext context, ILoggingBuilder logging) => {
        var enableFileLog = context.Configuration.GetSection("EnableFileLog")?.Get<bool>() ?? false;
        if (enableFileLog)
        {
            logging.ClearProviders();
            logging.SetMinimumLevel(LogLevel.Trace);
            logging.AddLog4Net();
        }
    })
    .Build();

await host.RunAsync();