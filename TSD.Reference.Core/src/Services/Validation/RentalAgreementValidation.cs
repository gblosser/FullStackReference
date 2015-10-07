using System.Linq;
using TSD.Reference.Core.Entities;
using TSD.Reference.Core.Exceptions;

namespace TSD.Reference.Core.Services.Validation
{
	internal static class RentalAgreementValidationExtension
	{
		public static void Validate(this RentalAgreement theRentalAgreement, Customer theCustomer)
		{
			if(!theCustomer.AllowsAdditionalDrivers && theRentalAgreement.AdditionalDrivers.Any())
				throw new InvalidRentalAgreementException("No additional drivers are allowed", theRentalAgreement);

			if(!theCustomer.AllowsAdditions && theRentalAgreement.Additions.Any())
				throw new InvalidRentalAgreementException("No additions are allowed", theRentalAgreement);

			var aTimeSpan = theRentalAgreement.InDate.Subtract(theRentalAgreement.OutDate).Days;
			if (theCustomer.HasMaxRentalDays && aTimeSpan > theCustomer.MaxRentalDays)
				throw new InvalidRentalAgreementException("Rental agreement exceed the maximum number of rental days allowed for this customer", theRentalAgreement);
		}
	}
}
