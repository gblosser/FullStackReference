using System.Collections.Generic;
using System.Threading.Tasks;
using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;
using TSD.Reference.Core.Services.Interfaces;

namespace TSD.Reference.Core.Services
{
	public class DriverService : IDriverService
	{
		private readonly IDriverRepository _repository;

		public DriverService(IDriverRepository theRepository)
		{
			_repository = theRepository;
		}

		public IEnumerable<Driver> GetDriverByLastName(string theLastName)
		{
			return _repository.GetDriverByLastName(theLastName);
		}

		public async Task<IEnumerable<Driver>> GetDriversByCustomerAsync(int theCustomerId)
		{
			return await _repository.GetDriversByCustomerAsync(theCustomerId);
		}

		/// <summary>
		/// Returns the driver for the customer
		/// </summary>
		/// <param name="theCustomerId"></param>
		/// <param name="theDriverId"></param>
		/// <returns></returns>
		public Driver GetDriver(int theCustomerId, int theDriverId)
		{
			var aDriver = _repository.GetDriver(theDriverId);

			return aDriver?.CustomerId == theCustomerId ? aDriver : null;
		}

		public async Task<Driver> GetDriverAsync(int theCustomerId, int id)
		{
			var aDriver = await _repository.GetDriverAsync(id);

			return aDriver?.CustomerId == theCustomerId ? aDriver : null;
		}

		public async Task<int> AddDriverAsync(Driver driver)
		{
			return await _repository.AddDriverAsync(driver);
		}

		public async Task UpdateDriverAsync(Driver driver)
		{
			await _repository.UpdateDriverAsync(driver);
		}

		public async Task DeleteDriverAsync(Driver driver)
		{
			await _repository.DeleteDriverAsync(driver);
		}

		public int AddDriver(Driver theDriver)
		{
			return _repository.AddDriver(theDriver);
		}
	}
}
