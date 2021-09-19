using AspectCore.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace FastFrame.WebHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
                 /*替换成AspectCore的容器实现，性能更高*/
                 .UseServiceProviderFactory(new ServiceContextProviderFactory())
                 //.UseServiceProviderFactory(new DynamicProxyServiceProviderFactory())
                 .ConfigureAppConfiguration((context, config) =>
                  {
                      config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                  })                 
                 .ConfigureWebHostDefaults(webBuilder =>
                   { 
                       webBuilder.UseStartup<Startup>();
                   });
    } 
}
