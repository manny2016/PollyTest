using Microsoft.Graph;
using System.Threading.Tasks;

namespace PollyTest {
	public interface IGraphClient {

		Task<GraphServiceClient> CreateAsync();
		Task<IGraphServiceApplicationsCollectionPage> GetApplicationsAsync();
	}
}
