using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TSD.Reference.Core.Entities;

namespace TSD.Reference.Core.Data
{
	public interface IRenterRepository
	{
		Renter GetRenter(int theRenterId);
		int AddRenter(Renter theRenter);
		void UpdateRenter(Renter theRenter);
		void DeleteRenter(Renter theRenter);

		Task<Renter> GetRenterAsync(int theRenterId);
		Task<int> AddRenterAsync(Renter theRenter);
		Task UpdateRenterAsync(Renter theRenter);
		Task DeleteRenterAsync(Renter theRenter);
		Task<IEnumerable<Renter>> GetRentersForCustomerAsync(int theCustomerId);
	}
}