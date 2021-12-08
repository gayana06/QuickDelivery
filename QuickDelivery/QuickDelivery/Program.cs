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
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var url = (string)config.GetValue(typeof(string), "HostBaseUrl");

            var webHostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(s =>
                {
                    s.UseStartup<Startup>();
                    s.UseUrls(url);
                })
                .UseDefaultServiceProvider((context, options) => { options.ValidateOnBuild = true; });

            return webHostBuilder;
        }
    }
}
