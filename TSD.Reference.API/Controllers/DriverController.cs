using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using TSD.Reference.API.Extensions;
using TSD.Reference.Core.Entities;
using TSD.Reference.Core.Services.Interfaces;

namespace TSD.Reference.API.Controllers
{
	public class DriverController : ApiController
	{
		private readonly IDriverService _driverService;

		public DriverController(IDriverService theDriverService)
		{
			_driverService = theDriverService;
		}

		// GET: api/Driver
		public async Task< IEnumerable<Driver>> Get()
		{
			var aCustomerId = this.GetCustomerId();

			return await _driverService.GetDriversByCustomerAsync(aCustomerId);
		}

		// GET: api/Driver/5
		public async Task<Driver> Get(int id)
		{
			var aCustomerId = this.GetCustomerId();

			return await _driverService.GetDriverAsync(aCustomerId, id);
		}

		// POST: api/Driver
		public async Task<int> Post(Driver driver)
		{
			return await _driverService.AddDriverAsync(driver);
		}

		// PUT: api/Driver/5
		public async Task Put(Driver driver)
		{
			await _driverService.UpdateDriverAsync(driver);
		}

		// DELETE: api/Driver/5
		public async Task Delete(Driver driver)
		{
			await _driverService.DeleteDriverAsync(driver);
		}
	}
}
