using System.Collections.Generic;
using System.Threading.Tasks;
using TSD.Reference.Core.Entities;

namespace TSD.Reference.Core.Services.Interfaces
{
	public interface IDriverService
	{
		int AddDriver(Driver theDriver);
		IEnumerable<Driver> GetDriverByLastName(string theLastName);
		Task<IEnumerable<Driver>> GetDriversByCustomerAsync(int theCustomerId);
		Driver GetDriver(int theCustomerId, int id);
		Task<Driver> GetDriverAsync(int theCustomerId, int id);
		Task<int> AddDriverAsync(Driver driver);
		Task UpdateDriverAsync(Driver driver);
		Task DeleteDriverAsync(Driver driver);
	}
}