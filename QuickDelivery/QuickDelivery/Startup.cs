using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using QuickDelivery.Filters;
using QuickDelivery.Providers;
using QuickDelivery.Repositories;
using QuickDelivery.Services;

namespace QuickDelivery
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ValidateModelFilter));
                options.Filters.Add(typeof(HttpResponseExceptionFilter));
            });

            services.AddRouting();

            services.AddOpenApiDocument();

            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IDeliveryDatesProvider, DeliveryDatesProvider>();
            services.AddSingleton<IProductRepository, ProductRepository>();
        }

        public void Configure(IApplicationBuilder app)
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
