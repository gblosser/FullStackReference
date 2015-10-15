using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using TSD.Reference.API.Extensions;
using TSD.Reference.Core.Entities;
using TSD.Reference.Core.Services.Interfaces;

namespace TSD.Reference.API.Controllers
{
	public class LocationController : ApiController
	{
		private readonly ILocationService _locationService;

		public LocationController(ILocationService theLocationService)
		{
			_locationService = theLocationService;
		}

		public async Task<IEnumerable<Location>> Get()
		{
			var aCustomerId = this.GetCustomerId();

			return await _locationService.GetLocationsForCustomerAsync(aCustomerId);
		}

		// GET: api/Location
		public async Task<Location> Get(int id)
		{
			return await _locationService.GetLocationAsync(id);
		}

		public async Task<int> Post(Location location)
		{
			return await _locationService.AddLocationAsync(location);
		}

		public async Task Put(Location location)
		{
			await _locationService.UpdateLocationAsync(location);
		}

		public async Task Delete(Location location)
		{
			await _locationService.DeleteLocationAsync(location);
		}
	}
}
