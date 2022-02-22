using Microsoft.Extensions.Hosting;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using Polly;
using PollyTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PollyTest3 {
	public class Startup : IHost {
		public Startup(IServiceProvider services, IGraphClient client) {
			Services = services;
			graphClient = client;
		}
		public IServiceProvider Services { get; private set; }
		private IGraphClient graphClient;
		public void Dispose() {

		}

		public async Task StartAsync(CancellationToken cancellationToken = default) {
			var apps = await graphClient.GetApplicationsAsync();
		
			Console.WriteLine($"End...{string.Join(";", apps.Select(x => x.DisplayName))}");
			//apps = await client.Applications.Request().GetAsync();
		}

		public async Task StopAsync(CancellationToken cancellationToken = default) {

		}
	}
}
