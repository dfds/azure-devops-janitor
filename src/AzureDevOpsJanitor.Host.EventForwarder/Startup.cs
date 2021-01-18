using AzureDevOpsJanitor.Host.EventForwarder.Enablers.ApiKey;
using AzureDevOpsJanitor.Host.EventForwarder.Enablers.Kafka;
using AzureDevOpsJanitor.Host.EventForwarder.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AzureDevOpsJanitor.Host.EventForwarder
{
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
            services.AddTransient<KafkaConfiguration>();
            services.AddTransient<KafkaProducerFactory>();
            
            services.AddSingleton<KafkaService>();
            services.AddSingleton<IHostedService>(p => p.GetService<KafkaService>());

            services.AddScoped<IApiKeyService, FileApiKeyService>();
            
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}