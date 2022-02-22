using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollyTest3 {
	public interface IPasswordProvider {
		Task<string> GetPasswordAsync(string appid);
		Task<string> ForceRefeshAsync(string appid, string password);
	}
}
