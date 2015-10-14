using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TSD.Reference.Core.Entities;

namespace TSD.Reference.Core.Data
{
	public interface IDriverRepository
	{
		Driver GetDriver(int theDriverId);
		IEnumerable<Driver> GetDriverByLastName(string theDriverName);
		int AddDriver(Driver theDriver);
		void UpdateDriver(Driver theDriver);
		void DeleteDriver(Driver theDriver);

		Task<Driver> GetDriverAsync(int theDriverId);
		Task<IEnumerable<Driver>> GetDriverByLastNameAsync(string theDriverName);
		Task<int> AddDriverAsync(Driver theDriver);
		Task UpdateDriverAsync(Driver theDriver);
		Task DeleteDriverAsync(Driver theDriver);
		Task<IEnumerable<Driver>> GetDriversByCustomerAsync(int theCustomerId);
	}
}