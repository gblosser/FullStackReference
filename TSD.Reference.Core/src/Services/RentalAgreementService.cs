using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;
using TSD.Reference.Core.Exceptions;
using TSD.Reference.Core.Services.Validation;

namespace TSD.Reference.Core.Services
{
	public class RentalAgreementService
	{
		private readonly IRentalAgreementRepository _rentalAgreementRepository;
		private readonly ICustomerRepository _customerRepository;

		public RentalAgreementService(IRentalAgreementRepository theRentalAgreementRepository, ICustomerRepository theCustomerRepository)
		{
			_rentalAgreementRepository = theRentalAgreementRepository;
			_customerRepository = theCustomerRepository;
		}

		/// <summary>
		/// Saves a new rental agreement.
		/// </summary>
		/// <param name="theRentalAgreement">The rental agreement to save</param>
		/// <returns>The id of the rental agreement (maybe change to rental agreement with id?)</returns>
		public int AddRentalAgreement(RentalAgreement theRentalAgreement)
		{
			try
			{
				var aCustomer = _customerRepository.GetCustomer(theRentalAgreement.Customer);
				theRentalAgreement.Validate(aCustomer);
				return _rentalAgreementRepository.AddRentalAgreement(theRentalAgreement);
			}
			catch (InvalidRentalAgreementException)
			{
				// TODO: should not ever catch here, instead have this bubble up to application to deal with it
				return 0;
			}
		}

		public RentalAgreement GetRentalAgreement(int theRentalAgreementId)
		{
			return _rentalAgreementRepository.GetRentalAgreement(theRentalAgreementId);
		}

		public RentalAgreement UpdateRentalAgreement(RentalAgreement theRentalAgreement)
		{
			_rentalAgreementRepository.UpdateRentalAgreement(theRentalAgreement);
			return GetRentalAgreement(theRentalAgreement.Id);
		}
	}
}
