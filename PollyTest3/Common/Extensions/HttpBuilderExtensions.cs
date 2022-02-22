using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;
using Polly;
using Polly.Extensions.Http;

using PollyTest.Handlers;
using System;
using System.Net.Http;

namespace PollyTest3.Common.Extensions {
	public static class HttpBuilderExtensions {
		public static IHttpClientBuilder AddTeamsAuthenticationHandler(this IHttpClientBuilder builder) {
			return builder.AddHttpMessageHandler((services) =>
			{
				var passwordProvider = services.GetService<IPasswordProvider>();
				
				return new TeamsAuthenticationHandler(passwordProvider);
			});
		}
	}
}
