using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using PollyTest;
using PollyTest.Services;
using PollyTest3.Common.Extensions;
using PollyTest3.Services.Auth;
using System;
using Polly;
using Microsoft.Extensions.Http;
using Polly.Extensions.Http;
using PollyTest3.Services;

namespace PollyTest3 {
	class Program {
		static void Main(string[] args) {
			CreateHostBuilder(args).Build().StartAsync();
			Console.ReadLine();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
			.ConfigureAppConfiguration((option) =>
			{

			})
			.ConfigureLogging((context, logging) =>
			{

			})
			.ConfigureServices((services) =>
			{
				

				services.AddSingleton<IHost, Startup>();
				services.AddMemoryCache();
			
				services.AddHttpClient<IMsalHttpClientFactory, GraphAuthenticationHttpClientFactory>()
				.AddHttpMessageHandler(() =>
				{
					return new PolicyHttpMessageHandler((request) =>
					{
						return HttpPolicyExtensions.HandleTransientHttpError()								
						.WaitAndRetryAsync(5, (idx) => { return TimeSpan.FromSeconds(1); });
					});
				})
				.AddTeamsAuthenticationHandler();
				services.AddTransient<IGraphClient, GraphClient>();
				services.AddTransient<IPasswordProvider, PasswordProvider>();
				services.AddTransient<IAuthenticationProvider, GraphAPIAuthenticationProvider>();
				services.AddHttpClient<IGraphHttpClient, GraphHttpClient>()
				.AddTeamsAuthenticationHandler();
			});
	}
}
