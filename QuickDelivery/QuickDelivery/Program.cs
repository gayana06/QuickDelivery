using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace QuickDelivery
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            var webHostBuilder = new HostBuilder()
                .ConfigureWebHost(s =>
                {
                    s.UseKestrel();
                    s.UseIIS();
                    s.UseIISIntegration();
                    s.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration((context, configBuilder) =>
                {
                    configBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    configBuilder.AddEnvironmentVariables();
                })
                .UseDefaultServiceProvider((context, options) => { options.ValidateOnBuild = true; });

            return webHostBuilder;
        }
    }
}
