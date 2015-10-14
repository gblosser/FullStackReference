using System.Collections.Generic;
using System.Threading.Tasks;
using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;
using TSD.Reference.Core.Services.Interfaces;

namespace TSD.Reference.Core.Services
{
	public class LocationService : ILocationService
	{
		private readonly ILocationRepository _repository;
		public LocationService(ILocationRepository theLocationRepository)
		{
			_repository = theLocationRepository;
		}

		public async Task DeleteLocationAsync(Location theLocation)
		{
			await _repository.DeleteLocationAsync(theLocation);
		}

		public Location GetLocation(int theLocationId)
		{
			return _repository.GetLocation(theLocationId);
		}

		public async Task<Location> GetLocationAsync(int theLocationId)
		{
			return await _repository.GetLocationAsync(theLocationId);
		}

		public int AddLocation(Location theLocation )
		{
			return _repository.AddLocation(theLocation);
		}

		public async Task<int> AddLocationAsync(Location theLocation)
		{
			return await _repository.AddLocationAsync(theLocation);
		}

		public void UpdateLocation(Location theLocation)
		{
			_repository.UpdateLocation(theLocation);
		}

		public async Task UpdateLocationAsync(Location theLocation)
		{
			await _repository.UpdateLocationAsync(theLocation);
		}

		public async Task<IEnumerable<Location>> GetLocationsForCustomerAsync(int theCustomerId)
		{
			return await _repository.GetLocationsForCustomerAsync(theCustomerId);
		}

		public void DeleteLocation(Location theLocation)
		{
			_repository.DeleteLocation(theLocation);
		}
	}
}
