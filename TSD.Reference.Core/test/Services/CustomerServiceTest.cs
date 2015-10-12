using System;
using Moq;
using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;
using TSD.Reference.Core.Services;
using Xunit;

namespace TSD.Reference.Core.test.Services
{
	public class CustomerServiceTest
	{
		private readonly CustomerService _svc;

		public CustomerServiceTest()
		{
			var aCustomerRepository = new Mock<ICustomerRepository>();
			aCustomerRepository.Setup(aItem => aItem.GetCustomerByName(It.IsAny<string>())).Returns(
				(string theName) =>
					new Customer
					{
						AllowsAdditions = false,
						AllowsAdditionalDrivers = false,
						HasMaxRentalDays = false,
						Id = 1,
						MaxRentalDays = 28,
						Name = theName
					});

			aCustomerRepository.Setup(aItem => aItem.AddCustomer(It.IsAny<Customer>())).Returns(
				(Customer theCustomer) => theCustomer.Id);

			_svc = new CustomerService(aCustomerRepository.Object);
		}

		[Fact]
		public void GetCustomerByNameTest()
		{
			var aCustomer = _svc.GetCustomerByName("ABC Rental");

			Assert.Equal("ABC Rental", aCustomer.Name);
		}

		[Fact]
		public void AddCustomerTest()
		{
			var aNewCustId = new Random(DateTime.Now.Millisecond).Next();
			var aNewCustomerId = _svc.AddCustomer(
				new Customer
				{
					AllowsAdditions = false,
					AllowsAdditionalDrivers = false,
					HasMaxRentalDays = false,
					Id = aNewCustId,
					MaxRentalDays = 28,
					Name = "ABC Rentals"
				});

			Assert.Equal(aNewCustId, aNewCustomerId);
		}

		[Fact]
		public void UpdateCustomerTest()
		{
			var aCustomer =
				new Customer
				{
					AllowsAdditions = false,
					AllowsAdditionalDrivers = false,
					HasMaxRentalDays = true,
					Id = 1,
					MaxRentalDays = 28,
					Name = "ABC Rentals"
				};

			_svc.UpdateCustomer(aCustomer);

			Assert.Equal(true, aCustomer.HasMaxRentalDays);		// not really testing anything here
		}

		[Fact]
		public void DeleteCustomerTest()
		{
			var aCustomer =
				new Customer
				{
					AllowsAdditions = false,
					AllowsAdditionalDrivers = false,
					HasMaxRentalDays = true,
					Id = 1,
					MaxRentalDays = 28,
					Name = "ABC Rentals"
				};

			_svc.DeleteCustomer(aCustomer);		// nothing to assert on here

		}
	}
}
