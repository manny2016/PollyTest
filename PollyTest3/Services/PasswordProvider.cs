using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace PollyTest3.Services {
	public class PasswordProvider : IPasswordProvider {
		private readonly IMemoryCache memoryCache;
		public PasswordProvider(IMemoryCache cache) {
			memoryCache = cache;
		}
		private string pwd = "sdfsdf";
		public async Task<string> ForceRefeshAsync(string appid, string password) {
			pwd = password;
			memoryCache.Remove(appid);
			return await GetPasswordAsync(appid);
		}

		public async Task<string> GetPasswordAsync(string appid) {
			return memoryCache.GetOrCreate<string>(appid, (entity) =>
			{
				entity.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);
				return pwd;
			});
		}
	}
}
