using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
		private const int HasMaxRentalDaysCustomerId = 4;

		private const int RentalAgreementId = 1000;
		private const int EmployeeUser = 10;
		private const int NonEmployeeUser = 20;


		public RentalAgreementServiceTest()
		{
			var aMockRentalAgreementRepository = new Mock<IRentalAgreementRepository>();
			aMockRentalAgreementRepository.Setup(aItem => aItem.AddRentalAgreement(It.IsAny<RentalAgreement>())).Returns(RentalAgreementId);
			aMockRentalAgreementRepository.Setup(aItem => aItem.AddRentalAgreementAsync(It.IsAny<RentalAgreement>())).Returns(Task.FromResult(RentalAgreementId));
			aMockRentalAgreementRepository.Setup(aItem => aItem.GetRentalAgreement(It.IsAny<int>())).Returns(
				new RentalAgreement
				{
					Id = RentalAgreementId
				});
			aMockRentalAgreementRepository.Setup(aItem => aItem.GetRentalAgreementAsync(It.IsAny<int>())).Returns(Task.FromResult(
				new RentalAgreement
				{
					Id = RentalAgreementId
				}));
			aMockRentalAgreementRepository.Setup(aItem => aItem.GetRentalAgreementsForCustomerAsync(It.IsAny<int>()))
				.Returns(Task.FromResult(new List<RentalAgreement> { new RentalAgreement { Id = RentalAgreementId } }.AsEnumerable()));
			aMockRentalAgreementRepository.Setup(aItem => aItem.DeleteRentalAgreementAsync(It.IsAny<RentalAgreement>()))
				.Returns(Task.Run(() => { }));

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
			aMockCustomerRepository.Setup(aItem => aItem.GetCustomerAsync(PassingCustomerId)).Returns(Task.FromResult(
				new Customer
				{
					AllowsAdditionalDrivers = true,
					AllowsAdditions = true,
					HasMaxRentalDays = false,
					Id = PassingCustomerId,
					MaxRentalDays = 28,
					Name = "Good Customer"
				}));
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
			aMockCustomerRepository.Setup(aItem => aItem.GetCustomerAsync(NoAdditionalDriversCustomerId)).Returns(Task.FromResult(
				new Customer
				{
					AllowsAdditionalDrivers = false,
					AllowsAdditions = true,
					HasMaxRentalDays = false,
					Id = NoAdditionalDriversCustomerId,
					MaxRentalDays = 28,
					Name = "No Additional Drivers Customer"
				}));
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
			aMockCustomerRepository.Setup(aItem => aItem.GetCustomerAsync(NoAdditionsCustomerId)).Returns(Task.FromResult(
				new Customer
				{
					AllowsAdditionalDrivers = true,
					AllowsAdditions = false,
					HasMaxRentalDays = false,
					Id = NoAdditionsCustomerId,
					MaxRentalDays = 28,
					Name = "No Additions Customer"
				}));
			aMockCustomerRepository.Setup(aItem => aItem.GetCustomer(HasMaxRentalDaysCustomerId)).Returns(
				new Customer
				{
					AllowsAdditionalDrivers = true,
					AllowsAdditions = true,
					HasMaxRentalDays = true,
					Id = NoAdditionsCustomerId,
					MaxRentalDays = 28,
					Name = "Has Max Rental Days Customer"
				});
			aMockCustomerRepository.Setup(aItem => aItem.GetCustomerAsync(HasMaxRentalDaysCustomerId)).Returns(Task.FromResult(
				new Customer
				{
					AllowsAdditionalDrivers = true,
					AllowsAdditions = true,
					HasMaxRentalDays = true,
					Id = NoAdditionsCustomerId,
					MaxRentalDays = 28,
					Name = "Has Max Rental Days Customer"
				}));



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
			aMockUserRepository.Setup(aItem => aItem.GetUserAsync(EmployeeUser)).Returns(Task.FromResult(
				new User
				{
					CustomerId = 1,
					Email = "a@b.co",
					FirstName = "first",
					LastName = "your last",
					Id = EmployeeUser,
					IsEmployee = true
				}));
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
			aMockUserRepository.Setup(aItem => aItem.GetUserAsync(NonEmployeeUser)).Returns(Task.FromResult(
				new User
				{
					CustomerId = 1,
					Email = "ricky@me.com",
					FirstName = "ricky",
					LastName = "bobby",
					Id = NonEmployeeUser,
					IsEmployee = false
				}));


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
		public async Task AddValidRentalAgreementAsyncTest()
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

			var aAgreementNumber = await _service.AddRentalAgreementAsync(aRentalAgreement);

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
		public async Task AddInvalidRentalAgreementNoAdditionalDriversAsyncTest()
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

			await Assert.ThrowsAsync<InvalidRentalAgreementException>(() => _service.AddRentalAgreementAsync(aRentalAgreement));
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
		public async Task AddInvalidRentalAgreementNoAdditionsAsyncTest()
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

			await Assert.ThrowsAsync<InvalidRentalAgreementException>(() => _service.AddRentalAgreementAsync(aRentalAgreement));
		}


		[Fact]
		public void AddInvalidRentalAgreementInvalidDatesTest()
		{
			var aRentalAgreement = new RentalAgreement
			{
				AdditionalDrivers = new List<int> { 1, 2, 3, 4, 5 },
				Additions = new List<string> { "gps", "baby seat" },
				Automobile = 1,
				Customer = PassingCustomerId,
				EmployeeId = 1,
				InDate = DateTime.Now,
				Location = 1,
				OutDate = DateTime.Now.AddDays(2),
				Renter = 1,
				Status = "Reservation"
			};

			Assert.Throws<InvalidRentalAgreementException>(() => _service.AddRentalAgreement(aRentalAgreement));
		}

		[Fact]
		public async Task AddInvalidRentalAgreementInvalidDatesAsyncTest()
		{
			var aRentalAgreement = new RentalAgreement
			{
				AdditionalDrivers = new List<int> { 1, 2, 3, 4, 5 },
				Additions = new List<string> { "gps", "baby seat" },
				Automobile = 1,
				Customer = PassingCustomerId,
				EmployeeId = 1,
				InDate = DateTime.Now,
				Location = 1,
				OutDate = DateTime.Now.AddDays(2),
				Renter = 1,
				Status = "Reservation"
			};

			await Assert.ThrowsAsync<InvalidRentalAgreementException>(() => _service.AddRentalAgreementAsync(aRentalAgreement));
		}

		[Fact]
		public void AddInvalidRentalAgreementMaxLenghtExceededTest()
		{
			var aRentalAgreement = new RentalAgreement
			{
				AdditionalDrivers = new List<int> { 1, 2, 3, 4, 5 },
				Additions = new List<string> { "gps", "baby seat" },
				Automobile = 1,
				Customer = HasMaxRentalDaysCustomerId,
				EmployeeId = 1,
				InDate = DateTime.Now.AddDays(50),
				Location = 1,
				OutDate = DateTime.Now.AddDays(2),
				Renter = 1,
				Status = "Reservation"
			};

			Assert.Throws<InvalidRentalAgreementException>(() => _service.AddRentalAgreement(aRentalAgreement));
		}

		[Fact]
		public async Task AddInvalidRentalAgreementMaxLenghtExceededAsyncTest()
		{
			var aRentalAgreement = new RentalAgreement
			{
				AdditionalDrivers = new List<int> { 1, 2, 3, 4, 5 },
				Additions = new List<string> { "gps", "baby seat" },
				Automobile = 1,
				Customer = HasMaxRentalDaysCustomerId,
				EmployeeId = 1,
				InDate = DateTime.Now.AddDays(50),
				Location = 1,
				OutDate = DateTime.Now.AddDays(2),
				Renter = 1,
				Status = "Reservation"
			};

			await Assert.ThrowsAsync<InvalidRentalAgreementException>(() => _service.AddRentalAgreementAsync(aRentalAgreement));
		}

		[Fact]
		public void GetRentalAgreementTest()
		{
			var aRentalAgreement = _service.GetRentalAgreement(RentalAgreementId);

			Assert.Equal(RentalAgreementId, aRentalAgreement.Id);
		}

		[Fact]
		public async Task GetRentalAgreementAsyncTest()
		{
			var aRentalAgreement = await _service.GetRentalAgreementAsync(RentalAgreementId);

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
		public async Task UpdateRentalAgreementPassingAsyncTest()
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

			await _service.UpdateRentalAgreementAsync(aRentalAgreement);
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

		[Fact]
		public async Task UpdateRentalAgreementInvalidPermissionsAsyncTest()
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

			await Assert.ThrowsAsync<InvalidPermissionsException>(() => _service.UpdateRentalAgreementAsync(aRentalAgreement));
		}

		[Fact]
		public async Task GetRentalAgreementsForCustomerAsyncTest()
		{
			var aRentalAgreements = await _service.GetRentalAgreementsForCustomerAsync(1);

			Assert.NotEmpty(aRentalAgreements);
		}

		[Fact]
		public async Task DeleteRentalAgreementAsyncTest()
		{
			var aRentalAgreement = await _service.GetRentalAgreementAsync(RentalAgreementId);

			Assert.Equal(RentalAgreementId, aRentalAgreement.Id);

			await _service.DeleteRentalAgreementAsync(aRentalAgreement);
		}
	}
}
