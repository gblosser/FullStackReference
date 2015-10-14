using System.Collections.Generic;
using System.Threading.Tasks;
using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;
using TSD.Reference.Core.Services.Interfaces;

namespace TSD.Reference.Core.Services
{
	public class RenterService : IRenterService
	{
		private readonly IRenterRepository _renterRepository;

		public RenterService(IRenterRepository theRenterRepository)
		{
			_renterRepository = theRenterRepository;
		}

		public Renter GetRenter(int theRenterId)
		{
			return _renterRepository.GetRenter(theRenterId);
		}

		public async Task<IEnumerable<Renter>> GetRentersAsync(int aCustomerId)
		{
			return await _renterRepository.GetRentersForCustomerAsync(aCustomerId);
		}

		public async Task<Renter> GetRenterAsync(int aCustomerId, int id)
		{
			var aRenter = await _renterRepository.GetRenterAsync(id);

			return aRenter.CustomerId == aCustomerId ? aRenter : null;
		}

		public async Task<int> AddRenterAsync(Renter renter)
		{
			return await _renterRepository.AddRenterAsync(renter);
		}

		public async Task UpdateRenterAsync(Renter renter)
		{
			await _renterRepository.UpdateRenterAsync(renter);
		}

		public async Task DeleteRenterAsync(Renter renter)
		{
			await _renterRepository.DeleteRenterAsync(renter);
		}
	}
}
