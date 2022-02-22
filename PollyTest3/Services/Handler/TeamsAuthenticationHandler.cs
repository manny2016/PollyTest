using Microsoft.Graph;
using PollyTest3;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;

namespace PollyTest.Handlers {
	public class TeamsAuthenticationHandler : DelegatingHandler {

		private readonly IPasswordProvider _passwordProvider;
		private readonly IAuthenticationProvider _authenticationProvider;
		public TeamsAuthenticationHandler(IPasswordProvider passwordProvider) {
			_passwordProvider = passwordProvider;
		}
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {

			//https://login.microsoftonline.com/common/discovery/instance?api-version=1.1&authorization_endpoint=https%3A%2F%2Flogin.microsoftonline.com%2F55b668c0-4163-4edf-a76c-1fb284cbd0a6%2Foauth2%2Fv2.0%2Fauthorize
			//var content = await request.Content.ReadAsStringAsync();
			var response = await base.SendAsync(request, cancellationToken);
			if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
			{
				var content = await request.Content.ReadAsStringAsync();
				var parameters = content.Split('&').Select(x => x.Split('='))
					.ToDictionary(x => x[0], x => x[1]);
				await _passwordProvider.ForceRefeshAsync(parameters["client_id"], Constant.Secret);				
			}
			return response;
		}

	}
}
