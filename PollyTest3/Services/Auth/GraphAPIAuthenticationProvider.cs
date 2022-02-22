using Microsoft.Graph;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Polly;


namespace PollyTest3.Services.Auth {
	public class GraphAPIAuthenticationProvider : IAuthenticationProvider {


		private readonly string[] scopes = new string[] { "https://graph.microsoft.com/.default" };
		private readonly IPasswordProvider _passwordProvider;
		private readonly IMsalHttpClientFactory _factory;

		public GraphAPIAuthenticationProvider(IMsalHttpClientFactory factory, IPasswordProvider passwordProvider) {
			_factory = factory;
			_passwordProvider=passwordProvider;
		}
		public async Task AuthenticateRequestAsync(HttpRequestMessage request) {
			try
			{
				
			}
			catch(AggregateException ex)
			{

			}
		

			Policy.Handle<AggregateException>()
				.OrInner<MsalServiceException>()
				.Retry(1)
				.Execute(()=>{
					var password = _passwordProvider.GetPasswordAsync(Constant.AppID).Result;
					var client = ConfidentialClientApplicationBuilder.Create(Constant.AppID)
						.WithClientSecret(password)
						.WithTenantId(Constant.TenantId)
						.WithHttpClientFactory(_factory)
						.Build();

					var authenticateResult = client.AcquireTokenForClient(scopes).ExecuteAsync().Result;
					request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authenticateResult.AccessToken);
				});
					
		}
	}

}
