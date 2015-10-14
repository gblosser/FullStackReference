using System.Collections.Generic;
using System.Threading.Tasks;
using TSD.Reference.Core.Entities;

namespace TSD.Reference.Core.Services.Interfaces
{
	public interface IRentalAgreementService
	{
		int AddRentalAgreement(RentalAgreement theRentalAgreement);
		RentalAgreement GetRentalAgreement(int theRentalAgreementId);
		RentalAgreement UpdateRentalAgreement(RentalAgreement theRentalAgreement);

		Task<int> AddRentalAgreementAsync(RentalAgreement theRentalAgreement);
		Task<RentalAgreement> GetRentalAgreementAsync(int theRentalAgreementId);
		Task<RentalAgreement> UpdateRentalAgreementAsync(RentalAgreement theRentalAgreement);
		Task<IEnumerable<RentalAgreement>> GetRentalAgreementsForCustomerAsync(int theCustomerId);
		Task DeleteRentalAgreementAsync(RentalAgreement theRentalAgreement);
	}
}