using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ResourceProvisioning.Broker.Application;

namespace ResourceProvisioning.Broker.Host.Worker
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
				.ConfigureServices((hostContext, services) =>
				{
					services.AddHostedService<Worker>();

					services.AddProvisioningBroker(options =>
					{
						hostContext.Configuration.Bind(options);
					});
				});
	}
}
