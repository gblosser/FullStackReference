using System;
using System.Collections.Generic;
using Moq;
using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;
using TSD.Reference.Core.Exceptions;
using TSD.Reference.Core.Services;
using Xunit;

namespace TSD.Reference.Core.test.Services
{
	public class RentalAgreementServiceTest
	{
		private readonly RentalAgreementService _service;
		private const int PassingCustomerId = 1;
		private const int NoAdditionalDriversCustomerId = 2;
		private const int NoAdditionsCustomerId = 3;
		private const int RentalAgreementId = 1000;
		private const int EmployeeUser = 10;
		private const int NonEmployeeUser = 20;

		public RentalAgreementServiceTest()
		{
			var aMockRentalAgreementRepository = new Mock<IRentalAgreementRepository>();
			aMockRentalAgreementRepository.Setup(aItem => aItem.AddRentalAgreement(It.IsAny<RentalAgreement>())).Returns(RentalAgreementId);
			aMockRentalAgreementRepository.Setup(aItem => aItem.GetRentalAgreement(It.IsAny<int>())).Returns(
				new RentalAgreement
				{
					Id = RentalAgreementId
				});

			// configure mock Customer repository to return various customers
			var aMockCustomerRepository = new Mock<ICustomerRepository>();
			aMockCustomerRepository.Setup(aItem => aItem.GetCustomer(PassingCustomerId)).Returns(
				new Customer
				{
					AllowsAdditionalDrivers = true,
					AllowsAdditions = true,
					HasMaxRentalDays = false,
					Id = PassingCustomerId,
					MaxRentalDays = 28,
					Name = "Good Customer"
				});
			aMockCustomerRepository.Setup(aItem => aItem.GetCustomer(NoAdditionalDriversCustomerId)).Returns(
				new Customer
				{
					AllowsAdditionalDrivers = false,
					AllowsAdditions = true,
					HasMaxRentalDays = false,
					Id = NoAdditionalDriversCustomerId,
					MaxRentalDays = 28,
					Name = "No Additional Drivers Customer"
				});
			aMockCustomerRepository.Setup(aItem => aItem.GetCustomer(NoAdditionsCustomerId)).Returns(
				new Customer
				{
					AllowsAdditionalDrivers = true,
					AllowsAdditions = false,
					HasMaxRentalDays = false,
					Id = NoAdditionsCustomerId,
					MaxRentalDays = 28,
					Name = "No Additions Customer"
				});

			var aMockUserRepository = new Mock<IUserRepository>();
			aMockUserRepository.Setup(aItem => aItem.GetUser(EmployeeUser)).Returns(
				new User
				{
					CustomerId = 1,
					Email = "a@b.co",
					FirstName = "first",
					LastName = "your last",
					Id = EmployeeUser,
					IsEmployee = true
				});
			aMockUserRepository.Setup(aItem => aItem.GetUser(NonEmployeeUser)).Returns(
				new User
				{
					CustomerId = 1,
					Email = "ricky@me.com",
					FirstName = "ricky",
					LastName = "bobby",
					Id = NonEmployeeUser,
					IsEmployee = false
				});


			_service = new RentalAgreementService(aMockRentalAgreementRepository.Object, aMockCustomerRepository.Object, aMockUserRepository.Object);
		}

		[Fact]
		public void AddValidRentalAgreementTest()
		{
			var aRentalAgreement = new RentalAgreement
			{
				AdditionalDrivers = new List<int> { 1, 2, 3, 4, 5 },
				Additions = new List<string> { "gps", "baby seat" },
				Automobile = 1,
				Customer = PassingCustomerId,
				EmployeeId = 1,
				InDate = DateTime.Now.AddDays(2),
				Location = 1,
				OutDate = DateTime.Now,
				Renter = 1,
				Status = "Reservation"
			};

			var aAgreementNumber = _service.AddRentalAgreement(aRentalAgreement);

			Assert.Equal(RentalAgreementId, aAgreementNumber);
		}

		[Fact]
		public void AddInvalidRentalAgreementNoAdditionalDriversTest()
		{
			var aRentalAgreement = new RentalAgreement
			{
				AdditionalDrivers = new List<int> { 1, 2, 3, 4, 5 },
				Additions = new List<string> { "gps", "baby seat" },
				Automobile = 1,
				Customer = NoAdditionalDriversCustomerId,
				EmployeeId = 1,
				InDate = DateTime.Now.AddDays(2),
				Location = 1,
				OutDate = DateTime.Now,
				Renter = 1,
				Status = "Reservation"
			};

			Assert.Throws<InvalidRentalAgreementException>(() => _service.AddRentalAgreement(aRentalAgreement));
		}

		[Fact]
		public void AddInvalidRentalAgreementNoAdditionsTest()
		{
			var aRentalAgreement = new RentalAgreement
			{
				AdditionalDrivers = new List<int> { 1, 2, 3, 4, 5 },
				Additions = new List<string> { "gps", "baby seat" },
				Automobile = 1,
				Customer = NoAdditionsCustomerId,
				EmployeeId = 1,
				InDate = DateTime.Now.AddDays(2),
				Location = 1,
				OutDate = DateTime.Now,
				Renter = 1,
				Status = "Reservation"
			};

			Assert.Throws<InvalidRentalAgreementException>(() => _service.AddRentalAgreement(aRentalAgreement));
		}

		[Fact]
		public void GetRentalAgreementTest()
		{
			var aRentalAgreement = _service.GetRentalAgreement(RentalAgreementId);

			Assert.Equal(RentalAgreementId, aRentalAgreement.Id);
		}

		[Fact]
		public void UpdateRentalAgreementPassingTest()
		{
			var aRentalAgreement = new RentalAgreement
			{
				AdditionalDrivers = new List<int> { 1, 2, 3, 4, 5 },
				Additions = new List<string> { "gps", "baby seat" },
				Automobile = 1,
				Customer = PassingCustomerId,
				EmployeeId = EmployeeUser,
				InDate = DateTime.Now.AddDays(2),
				Location = 1,
				OutDate = DateTime.Now,
				Renter = 1,
				Status = "Reservation"
			};

			_service.UpdateRentalAgreement(aRentalAgreement);
		}

		[Fact]
		public void UpdateRentalAgreementInvalidPermissionsTest()
		{
			var aRentalAgreement = new RentalAgreement
			{
				AdditionalDrivers = new List<int> { 1, 2, 3, 4, 5 },
				Additions = new List<string> { "gps", "baby seat" },
				Automobile = 1,
				Customer = PassingCustomerId,
				EmployeeId = NonEmployeeUser,
				InDate = DateTime.Now.AddDays(2),
				Location = 1,
				OutDate = DateTime.Now,
				Renter = 1,
				Status = "Reservation"
			};

			Assert.Throws<InvalidPermissionsException>(() => _service.UpdateRentalAgreement(aRentalAgreement));
		}
	}
}
