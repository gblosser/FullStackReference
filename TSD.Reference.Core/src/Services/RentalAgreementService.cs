﻿using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;
using TSD.Reference.Core.Exceptions;
using TSD.Reference.Core.Services.Validation;

namespace TSD.Reference.Core.Services
{
	public class RentalAgreementService
	{
		private readonly IRentalAgreementRepository _rentalAgreementRepository;
		private readonly ICustomerRepository _customerRepository;
		private readonly IUserRepository _userRepository;

		public RentalAgreementService(IRentalAgreementRepository theRentalAgreementRepository,
			ICustomerRepository theCustomerRepository, IUserRepository theUserRepository)
		{
			_rentalAgreementRepository = theRentalAgreementRepository;
			_customerRepository = theCustomerRepository;
			_userRepository = theUserRepository;
		}

		/// <summary>
		/// Saves a new rental agreement.
		/// </summary>
		/// <exception cref="InvalidRentalAgreementException">Will throw InvalidRentalAgreementException if there is a validation error</exception>
		/// <param name="theRentalAgreement">The rental agreement to save</param>
		/// <returns>The id of the rental agreement (maybe change to rental agreement with id?)</returns>
		public int AddRentalAgreement(RentalAgreement theRentalAgreement)
		{
			var aCustomer = _customerRepository.GetCustomer(theRentalAgreement.Customer);
			theRentalAgreement.Validate(aCustomer);		// validate the agreement using the business rules codified in the validation class.
			return _rentalAgreementRepository.AddRentalAgreement(theRentalAgreement);
		}

		/// <summary>
		/// Gets a rental agreement
		/// </summary>
		/// <param name="theRentalAgreementId">The Id of the rental agreement to get</param>
		/// <returns>The RentalAgreement with the Id passed in</returns>
		public RentalAgreement GetRentalAgreement(int theRentalAgreementId)
		{
			return _rentalAgreementRepository.GetRentalAgreement(theRentalAgreementId);
		}

		/// <summary>
		/// Updates an existing rental agreement.
		/// </summary>
		/// <param name="theRentalAgreement">The modified rental agreement</param>
		/// <exception cref="InvalidPermissionsException">InvalidPermissionsException will be thrown 
		/// if the user attempting to modify the rental agreement is not an employee</exception>
		/// <returns></returns>
		public RentalAgreement UpdateRentalAgreement(RentalAgreement theRentalAgreement)
		{
			var aUser = _userRepository.GetUser(theRentalAgreement.EmployeeId);

			if (!aUser.IsEmployee)
				throw new InvalidPermissionsException(
					$"The user {aUser.LastName}, {aUser.FirstName} is not an employee and cannot modify a rental agreement");

			_rentalAgreementRepository.UpdateRentalAgreement(theRentalAgreement);

			return GetRentalAgreement(theRentalAgreement.Id);
		}
	}
}
