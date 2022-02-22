using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PollyTest {
	public interface IGraphHttpClient  {
		HttpClient HttpClient { get; }
	}
}
