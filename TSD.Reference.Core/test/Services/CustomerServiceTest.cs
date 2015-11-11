using System;
using System.Threading.Tasks;
using Moq;
using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;
using TSD.Reference.Core.Exceptions;
using TSD.Reference.Core.Services;
using Xunit;

namespace TSD.Reference.Core.test.Services
{
	public class CustomerServiceTest
	{
		private readonly CustomerService _svc;

		public CustomerServiceTest()
		{
			var aCustomer = new Customer
			{
				AllowsAdditions = false,
				AllowsAdditionalDrivers = false,
				HasMaxRentalDays = false,
				Id = 1,
				MaxRentalDays = 28,
				Name = "ABC Rental"
			};

			var aCustomerRepository = new Mock<ICustomerRepository>();
			aCustomerRepository.Setup(aItem => aItem.GetCustomerByName(It.IsAny<string>())).Returns(
				(string theName) =>
				{
					aCustomer.Name = theName;
					return aCustomer;
				});

			aCustomerRepository.Setup(aItem => aItem.AddCustomer(It.IsAny<Customer>())).Returns(
				(Customer theCustomer) => theCustomer.Id);

			aCustomerRepository.Setup(aItem => aItem.DeleteCustomerAsync(It.IsAny<Customer>())).Returns(Task.Run(() => { }));
			aCustomerRepository.Setup(aItem => aItem.GetCustomer(1)).Returns(aCustomer);
			aCustomerRepository.Setup(aItem => aItem.GetCustomer(It.IsInRange(2, int.MaxValue, Range.Inclusive))).Returns((Customer)null);
			aCustomerRepository.Setup(aItem => aItem.GetCustomerAsync(1)).Returns(Task.FromResult(aCustomer));
			aCustomerRepository.Setup(aItem => aItem.GetCustomerAsync(It.IsInRange(2, int.MaxValue, Range.Inclusive))).Returns(Task.FromResult((Customer)null));
			aCustomerRepository.Setup(aItem => aItem.GetCustomerByNameAsync("ABC Rental"))
				.Returns(Task.FromResult(aCustomer));
			aCustomerRepository.Setup(aItem => aItem.GetCustomerByNameAsync(It.IsNotIn("ABC Rental")))
				.Returns(Task.FromResult((Customer)null));
			aCustomerRepository.Setup(aItem => aItem.AddCustomerAsync(It.IsAny<Customer>())).Returns(Task.FromResult(100));
			aCustomerRepository.Setup(aItem => aItem.UpdateCustomerAsync(It.IsAny<Customer>())).Returns(Task.Run(() => { }));

			_svc = new CustomerService(aCustomerRepository.Object);
		}


		[Fact]
		public async Task DeleteCustomerAsync()
		{
			await _svc.DeleteCustomerAsync(new Customer());
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
					Name = "ABC Rental"
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
					Name = "ABC Rental"
				};

			_svc.UpdateCustomer(aCustomer);

			Assert.Equal(true, aCustomer.HasMaxRentalDays);     // not really testing anything here
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
					Name = "ABC Rental"
				};

			_svc.DeleteCustomer(aCustomer);     // nothing to assert on here

		}

		[Fact]
		public void GetCustomerByIdTest()
		{
			var aCustomer = _svc.GetCustomerById(1);

			Assert.NotNull(aCustomer);
		}

		[Fact]
		public void GetCustomerByIdNullTest()
		{
			var aCustomer = _svc.GetCustomerById(2);

			Assert.Null(aCustomer);
		}

		[Fact]
		public async Task GetCustomerByNameAsyncTest()
		{
			var aCustomer = await _svc.GetCustomerByNameAsync("ABC Rental");

			Assert.NotNull(aCustomer);
		}

		[Fact]
		public async Task GetCustomerByNameNullAsyncTest()
		{
			var aCustomer = await _svc.GetCustomerByNameAsync("XYZ Rental");

			Assert.Null(aCustomer);
		}

		[Fact]
		public async Task GetCustomerByIdAsyncTest()
		{
			var aCustomer = await _svc.GetCustomerByIdAsync(1);

			Assert.NotNull(aCustomer);
		}

		[Fact]
		public async Task GetCustomerByIdAsyncNullTest()
		{
			var aCustomer = await _svc.GetCustomerByIdAsync(2);

			Assert.Null(aCustomer);
		}

		[Fact]
		public async Task AddCustomerAsyncTest()
		{
			var aCustomer = new Customer() { Name = "XYZ Rental" };

			var aNewId = await _svc.AddCustomerAsync(aCustomer);
		}

		[Fact]
		public async Task AddCustomerAsyncThrowsTest()
		{
			var aCustomer = new Customer() { Name = "ABC Rental" };
			await Assert.ThrowsAsync<CustomerAddException>(() => _svc.AddCustomerAsync(aCustomer));
		}

		[Fact]
		public async Task UpdateCustomerAsyncTest()
		{
			var aCustomer = await _svc.GetCustomerByIdAsync(1);

			Assert.NotNull(aCustomer);

			aCustomer.Name = "ABC Rental Corporation";
			await _svc.UpdateCustomerAsync(aCustomer);
		}
    }
}
