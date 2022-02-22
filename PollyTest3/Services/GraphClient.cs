using Microsoft.Graph;
using Microsoft.Identity.Client;
using Polly;
using Polly.Extensions.Http;
using PollyTest3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PollyTest.Services {
	public class GraphClient : IGraphClient {

		private IAuthenticationProvider authenticationProvider;
		private IGraphHttpClient graphHttpClient;
		public GraphClient(IAuthenticationProvider authentication, IGraphHttpClient httpClient) {
			authenticationProvider = authentication;
			graphHttpClient = httpClient;
		}

		public async Task<GraphServiceClient> CreateAsync() {


			var httpProvider = new SimpleHttpProvider(graphHttpClient.HttpClient);
			return new GraphServiceClient(authenticationProvider, httpProvider);

		}

		public async Task<IGraphServiceApplicationsCollectionPage> GetApplicationsAsync() {


			var client = CreateAsync().Result;
			return await client.Applications.Request().GetAsync();



		}
	}
}
