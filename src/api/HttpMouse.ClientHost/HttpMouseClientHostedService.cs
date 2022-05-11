using HttpMouse.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HttpMouse.ClientHost
{
    sealed class HttpMouseClientHostedService : BackgroundService
    {
        private readonly IHttpMouseClientFactory httpMouseClientFactory;
        private readonly ILogger<HttpMouseClientHostedService> logger;
        private IHttpMouseClient client;

        public HttpMouseClientHostedService(
            IHttpMouseClientFactory httpMouseClientFactory,
            IOptionsMonitor<HttpMouseClientOptions> options,
            ILogger<HttpMouseClientHostedService> logger)
        {
            this.httpMouseClientFactory = httpMouseClientFactory;
            this.logger = logger;

            options.OnChange(this.HandleOnChange);
        }

        private void HandleOnChange(HttpMouseClientOptions arg1, string arg2)
        {
            client?.Close(default).Wait();
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await client?.Close(cancellationToken)!;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (stoppingToken.IsCancellationRequested == false)
            {
                try
                {
                    using var client = this.client = await httpMouseClientFactory.CreateAsync(stoppingToken);
                    this.logger.LogInformation($"连接到服务器成功");

                    this.logger.LogInformation($"等待数据传输..");
                    await client.ReceiveCMDAsync(stoppingToken);

                    this.logger.LogInformation($"数据传输结束");
                }
                catch (Exception ex)
                {
                    this.logger.LogWarning(ex.Message);
                    await Task.Delay(TimeSpan.FromSeconds(5d), stoppingToken);
                }
            }
        }
    }
}
