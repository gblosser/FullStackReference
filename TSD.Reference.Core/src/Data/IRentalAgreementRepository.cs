using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TSD.Reference.Core.Entities;

namespace TSD.Reference.Core.Data
{
	public interface IRentalAgreementRepository
	{
		RentalAgreement GetRentalAgreement(int theRentalAgreementId);
		int AddRentalAgreement(RentalAgreement theRentalAgreement);
		void UpdateRentalAgreement(RentalAgreement theRentalAgreement);
		void DeleteRentalAgreement(RentalAgreement theRentalAgreement);

		Task<RentalAgreement> GetRentalAgreementAsync(int theRentalAgreementId);
		Task<int> AddRentalAgreementAsync(RentalAgreement theRentalAgreement);
		Task UpdateRentalAgreementAsync(RentalAgreement theRentalAgreement);
		Task DeleteRentalAgreementAsync(RentalAgreement theRentalAgreement);
		Task<IEnumerable<RentalAgreement>> GetRentalAgreementsForCustomerAsync(int theCustomerId);
	}
}