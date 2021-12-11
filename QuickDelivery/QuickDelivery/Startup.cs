using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuickDelivery.Filters;
using QuickDelivery.Providers;
using QuickDelivery.Repositories;
using QuickDelivery.Services;

namespace QuickDelivery
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ValidateModelFilter));
            });

            services.AddRouting();

            services.AddOpenApiDocument();

            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IDeliveryDatesProvider, DeliveryDatesProvider>();
            services.AddSingleton<IProductRepository, ProductRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}
