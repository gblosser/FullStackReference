using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;
using TSD.Reference.Core.Services.Interfaces;

namespace TSD.Reference.Core.Services
{
	public class AutomobileService : IAutomobileService
	{
		private readonly IAutomobileRepository _autoRepository;
		private readonly ILocationRepository _locationRepository;

		public AutomobileService(IAutomobileRepository theAutoRepository, ILocationRepository theLocationRepository)
		{
			_autoRepository = theAutoRepository;
			_locationRepository = theLocationRepository;
		}

		public IEnumerable<Automobile> GetAutomobiles(IEnumerable<int> theAutomobileIds)
		{
			return _autoRepository.GetAutomobiles(theAutomobileIds);
		}

		public async Task<IEnumerable<Automobile>> GetAutomobilesAsync(IEnumerable<int> theAutomobileIds)
		{
			return await _autoRepository.GetAutomobilesAsync(theAutomobileIds);
		}

		public async Task<IEnumerable<Automobile>> GetAutomobilesForCustomerAsync(int theCustomerId)
		{
			// get location ids for customer
			var aLocations = await _locationRepository.GetLocationsForCustomerAsync(theCustomerId);
			var aLocationsList = aLocations.ToList();
			if(!aLocationsList.Any())
				return Enumerable.Empty<Automobile>();

			// get automobiles for all customer locations
			return await _autoRepository.GetAutomobilesForLocationsAsync(aLocationsList.Select(aItem => aItem.Id));
		}

		public async Task<IEnumerable<Automobile>> GetAutomobilesForLocationAsync(int theCustomerId, int theLocationId)
		{
			var aCustomerLocations = await _locationRepository.GetLocationsForCustomerAsync(theCustomerId);
			var aCustomerLocationsList = aCustomerLocations.ToList();

			if (!aCustomerLocationsList.Any())
			{
				throw new ApplicationException("Customer has no locations");
			}

			if (aCustomerLocationsList.All(aItem => aItem.Id != theLocationId))
			{
				throw new ApplicationException("Customer has no locations with this id");
			}

			return await _autoRepository.GetAutomobilesForLocationAsync(theLocationId);
		}

		public async Task<Automobile> GetAutomobileAsync(int theCustomerId, int id)
		{
			var aAutomobile =  await _autoRepository.GetAutomobileAsync(id);
			var aLocations = await _locationRepository.GetLocationsForCustomerAsync(theCustomerId);

			// validate that customer has car on one of their locations before returning it
			// otherwise, return null
			return aLocations.Any(aItem => aItem.Id == aAutomobile.LocationId) ? aAutomobile : null;
		}

		public async Task UpdateAutomobileAsync(int theCustomerId, Automobile theAutomobile)
		{
			var aLocations = await _locationRepository.GetLocationsForCustomerAsync(theCustomerId);
			if (aLocations.All(aItem => aItem.Id != theAutomobile.LocationId))
				throw new ApplicationException("automobile does not belong to customer");
			_autoRepository.UpdateAutomobile(theAutomobile);
		}

		public int AddAutomobile(Automobile theAutomobile)
		{
			return _autoRepository.AddAutomobile(theAutomobile);
		}

		public async Task<int> AddAutomobileAsync(Automobile theAutomobile)
		{
			return await _autoRepository.AddAutomobileAsync(theAutomobile);
		}
	}
}
