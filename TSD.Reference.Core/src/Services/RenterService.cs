using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;

namespace TSD.Reference.Core.Services
{
	public class RenterService
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
	}
}
