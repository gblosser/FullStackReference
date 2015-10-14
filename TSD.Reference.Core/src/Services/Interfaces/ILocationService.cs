using System.Collections.Generic;
using System.Threading.Tasks;
using TSD.Reference.Core.Entities;

namespace TSD.Reference.Core.Services.Interfaces
{
	public interface ILocationService
	{
		int AddLocation(Location theLocation);
		Task<int> AddLocationAsync(Location theLocation);
		void DeleteLocation(Location theLocation);
		Task DeleteLocationAsync(Location theLocation);
		Location GetLocation(int theLocationId);
		Task<Location> GetLocationAsync(int theLocationId);
		void UpdateLocation(Location theLocation);
		Task UpdateLocationAsync(Location theLocation);
		Task<IEnumerable<Location>> GetLocationsForCustomerAsync(int theCustomerId);
	}
}