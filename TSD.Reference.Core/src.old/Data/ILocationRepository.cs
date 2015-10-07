using System.Threading.Tasks;
using TSD.Reference.Core.Entities;

namespace TSD.Reference.Core.Data
{
	public interface ILocationRepository
	{
		Location GetLocation(int theLocationId);
		int AddLocation(Location theLocation);
		void UpdateLocation(Location theLocation);
		void DeleteLocation(Location theLocation);

		Task<Location> GetLocationAsync(int theLocationId);
		Task<int> AddLocationAsync(Location theLocation);
		Task UpdateLocationAsync(Location theLocation);
		Task DeleteLocationAsync(Location theLocation);
	}
}