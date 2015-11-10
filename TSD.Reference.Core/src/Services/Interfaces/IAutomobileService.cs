using System.Collections.Generic;
using System.Threading.Tasks;
using TSD.Reference.Core.Entities;

namespace TSD.Reference.Core.Services.Interfaces
{
	public interface IAutomobileService
	{
		int AddAutomobile(Automobile theAutomobile);
		Task<int> AddAutomobileAsync(Automobile theAutomobile);
		IEnumerable<Automobile> GetAutomobiles(IEnumerable<int> theAutomobileIds);
		Task<IEnumerable<Automobile>> GetAutomobilesAsync(IEnumerable<int> theAutomobileIds);
		Task<IEnumerable<Automobile>> GetAutomobilesForCustomerAsync(int theCustomerId);
		Task<IEnumerable<Automobile>> GetAutomobilesForLocationAsync(int theCustomerId, int theLocationId);
		Task<Automobile> GetAutomobileAsync(int theCustomerId, int id);
		Task UpdateAutomobileAsync(int theCustomerId, Automobile theAutomobile);
	}
}