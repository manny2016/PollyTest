using Microsoft.Identity.Client;
using System.Net.Http;

namespace PollyTest.Services {
	public class GraphAuthenticationHttpClientFactory : IMsalHttpClientFactory {
		private readonly HttpClient  httpClient;
		public GraphAuthenticationHttpClientFactory(HttpClient client) {
			httpClient = client;
		}
		public HttpClient GetHttpClient() {
			
			return httpClient;
		}
	}
}
