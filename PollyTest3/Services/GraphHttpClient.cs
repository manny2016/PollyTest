using System.Net.Http;

namespace PollyTest.Services {
	public class GraphHttpClient : IGraphHttpClient {
		private readonly HttpClient httpClient;
		public GraphHttpClient(HttpClient client) {
			httpClient = client;
		}
		public HttpClient HttpClient => httpClient;
	}
}
