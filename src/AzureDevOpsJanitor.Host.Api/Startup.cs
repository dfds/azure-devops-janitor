using AzureDevOpsJanitor.Application;
using AzureDevOpsJanitor.Host.Api.Infrastructure.Middleware;
using AzureDevOpsJanitor.Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace AzureDevOpsJanitor.Host.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			AddHostServices(services);

			DependencyInjection.AddApplication(services, options =>
			{
				Configuration.Bind(options);

				if (options.ConnectionStrings.Exists())
				{
					return;
				}

				options.ConnectionStrings = new ConfigurationSection((IConfigurationRoot)Configuration, "ConnectionStrings")
				{
					[nameof(DomainContext)] = "Filename=:memory:;"
				};
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseRouting();
			app.UseCors("open");
			app.UseMiddleware<VstsCallbackMiddleware>();
			app.UseAuthentication();
			app.UseAuthorization();	
			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Broker API V1");
			});
		}

		protected virtual void AddHostServices(IServiceCollection services)
		{
			services.AddControllers();

			services.AddCors(options =>
			{
				options.DefaultPolicyName = "open";
				options.AddDefaultPolicy(p =>
				{
					p.AllowAnyHeader();
					p.AllowAnyMethod();
					p.AllowCredentials();
					p.WithExposedHeaders("X-Pagination");
				});
			});

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "Broker API",
					Version = "v1"
				});
			});

			services.AddTransient<VstsCallbackMiddleware>();
			services.AddSingleton<IMemoryCache, MemoryCache>();

			AddHostAuthentication(services);
		}

		protected virtual void AddHostAuthentication(IServiceCollection services)
		{
			services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
					.AddAzureADBearer(options => Configuration.Bind("AzureAd", options));
		}
	}
}
