using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;
using TSD.Reference.Core.Exceptions;
using TSD.Reference.Core.Services.Interfaces;

namespace TSD.Reference.Core.Services
{
	public class RentalAgreementService : IRentalAgreementService
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

			if (!aCustomer.AllowsAdditionalDrivers && theRentalAgreement.AdditionalDrivers.Any())
				throw new InvalidRentalAgreementException("No additional drivers are allowed", theRentalAgreement);

			if (!aCustomer.AllowsAdditions && theRentalAgreement.Additions.Any())
				throw new InvalidRentalAgreementException("No additions are allowed", theRentalAgreement);

			if(theRentalAgreement.OutDate > theRentalAgreement.InDate)
				throw new InvalidRentalAgreementException("Rental agreement Out Date occurs after the In Date", theRentalAgreement);

			var aTimeSpan = theRentalAgreement.InDate.Subtract(theRentalAgreement.OutDate).Days;
			if (aCustomer.HasMaxRentalDays && aTimeSpan > aCustomer.MaxRentalDays)
				throw new InvalidRentalAgreementException("Rental agreement exceed the maximum number of rental days allowed for this customer", theRentalAgreement);

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

		public async Task<int> AddRentalAgreementAsync(RentalAgreement theRentalAgreement)
		{
			var aCustomer = await _customerRepository.GetCustomerAsync(theRentalAgreement.Customer);

			if (!aCustomer.AllowsAdditionalDrivers && theRentalAgreement.AdditionalDrivers.Any())
				throw new InvalidRentalAgreementException("No additional drivers are allowed", theRentalAgreement);

			if (!aCustomer.AllowsAdditions && theRentalAgreement.Additions.Any())
				throw new InvalidRentalAgreementException("No additions are allowed", theRentalAgreement);

			if (theRentalAgreement.OutDate > theRentalAgreement.InDate)
				throw new InvalidRentalAgreementException("Rental agreement Out Date occurs after the In Date", theRentalAgreement);

			var aTimeSpan = theRentalAgreement.InDate.Subtract(theRentalAgreement.OutDate).Days;
			if (aCustomer.HasMaxRentalDays && aTimeSpan > aCustomer.MaxRentalDays)
				throw new InvalidRentalAgreementException("Rental agreement exceed the maximum number of rental days allowed for this customer", theRentalAgreement);

			return await _rentalAgreementRepository.AddRentalAgreementAsync(theRentalAgreement);
		}

		public async Task<RentalAgreement> GetRentalAgreementAsync(int theRentalAgreementId)
		{
			return await _rentalAgreementRepository.GetRentalAgreementAsync(theRentalAgreementId);
		}

		public async Task<RentalAgreement> UpdateRentalAgreementAsync(RentalAgreement theRentalAgreement)
		{
			var aUser = await _userRepository.GetUserAsync(theRentalAgreement.EmployeeId);

			if (!aUser.IsEmployee)
				throw new InvalidPermissionsException(
					$"The user {aUser.LastName}, {aUser.FirstName} is not an employee and cannot modify a rental agreement");

			await _rentalAgreementRepository.UpdateRentalAgreementAsync(theRentalAgreement);

			return await GetRentalAgreementAsync(theRentalAgreement.Id);
		}

		public async Task<IEnumerable<RentalAgreement>> GetRentalAgreementsForCustomerAsync(int theCustomerId)
		{
			return await _rentalAgreementRepository.GetRentalAgreementsForCustomerAsync(theCustomerId);
		}

		public async Task DeleteRentalAgreementAsync(RentalAgreement theRentalAgreement)
		{
			await _rentalAgreementRepository.DeleteRentalAgreementAsync(theRentalAgreement);
		}
	}
}
