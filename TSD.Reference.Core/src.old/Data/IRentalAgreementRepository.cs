using System;
using TSD.Reference.Core.Entities;

namespace TSD.Reference.Core.Data
{
	public interface IRentalAgreementRepository
	{
		RentalAgreement GetRentalAgreement(int theRentalAgreementId);
		int AddRentalAgreement(RentalAgreement theRentalAgreement);
		void UpdateRentalAgreement(RentalAgreement theRentalAgreement);
		void DeleteRentalAgreement(int theRentalAgreementId);
	}
}