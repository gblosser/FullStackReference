using System.Collections.Generic;
using System.Threading.Tasks;
using TSD.Reference.Core.Entities;

namespace TSD.Reference.Core.Services.Interfaces
{
	public interface IRenterService
	{
		Renter GetRenter(int theRenterId);
		Task<IEnumerable<Renter>> GetRentersAsync(int aCustomerId);
		Task<Renter> GetRenterAsync(int aCustomerId, int id);
		Task<int> AddRenterAsync(Renter renter);
		Task UpdateRenterAsync(Renter renter);
		Task DeleteRenterAsync(Renter renter);
	}
}