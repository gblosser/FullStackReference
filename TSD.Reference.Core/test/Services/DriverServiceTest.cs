using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;
using TSD.Reference.Core.Services;
using TSD.Reference.Core.Services.Interfaces;
using Xunit;

namespace TSD.Reference.Core.test.Services
{
	public class DriverServiceTest
	{
		private readonly IDriverService _svc;
		private readonly List<Driver> _driverList;

		public DriverServiceTest()
		{
			_driverList = new List<Driver>
			{
				new Driver
				{
					Address = "1 anystreet st",
					City = "anytown",
					Country = "United States",
					CustomerId = 1,
					FirstName = "Craig",
					LastName = "Smith",
					Id = 1,
					LicenseState = "MA",
					LicenseNumber = "12345",
					State = "MA",
					PostalCode = "12345"
				},
				new Driver
				{
					Address = "1 anystreet st",
					City = "anytown",
					Country = "United States",
					CustomerId = 1,
					FirstName = "Stephen",
					LastName = "Smith",
					Id = 2,
					LicenseState = "MA",
					LicenseNumber = "23456",
					State = "MA",
					PostalCode = "12345"
				}	   ,
				new Driver
				{
					Address = "1 mystreet st",
					City = "yourtown",
					Country = "United States",
					CustomerId = 2,
					FirstName = "Frank",
					LastName = "Sanderson",
					Id = 3,
					LicenseState = "NH",
					LicenseNumber = "ABSCD",
					State = "NH",
					PostalCode = "01234"
				}
			};

			var aDriverRepository = new Mock<IDriverRepository>();
			aDriverRepository.Setup(aItem => aItem.GetDriverByLastName(It.IsAny<string>())).Returns(
				(string theName) =>
					_driverList.Where(aItem => aItem.LastName == theName).ToList());
			aDriverRepository.Setup(aItem => aItem.GetDriver(It.IsAny<int>())).Returns(
				(int theId) => _driverList.FirstOrDefault(aItem => aItem.Id == theId));

			aDriverRepository.Setup(aItem => aItem.AddDriver(It.IsAny<Driver>())).Returns(
				(Driver theNewDriver) =>
				{
					theNewDriver.Id = _driverList.Count + 1;
					_driverList.Add(theNewDriver);
					return _driverList.Count;
				});

			aDriverRepository.Setup(aItem => aItem.GetDriversByCustomerAsync(1)).Returns((Task.FromResult(_driverList.AsEnumerable())));
			aDriverRepository.Setup(aItem => aItem.GetDriversByCustomerAsync(It.IsInRange(2, int.MaxValue, Range.Inclusive))).Returns(Task.FromResult(Enumerable.Empty<Driver>()));

			aDriverRepository.Setup(aItem => aItem.GetDriverAsync(1)).Returns(Task.FromResult(_driverList[0]));
			aDriverRepository.Setup(aItem => aItem.GetDriverAsync(2)).Returns(Task.FromResult(_driverList[1]));
			aDriverRepository.Setup(aItem => aItem.GetDriverAsync(3)).Returns(Task.FromResult(_driverList[2]));

			aDriverRepository.Setup(aItem => aItem.AddDriverAsync(It.IsAny<Driver>())).Returns(Task.FromResult(_driverList.Count + 1));

			aDriverRepository.Setup(aItem => aItem.UpdateDriverAsync(It.IsAny<Driver>())).Returns(Task.Run(()=> {}));
			aDriverRepository.Setup(aItem => aItem.DeleteDriverAsync(It.IsAny<Driver>())).Returns(Task.Run(() => { }));

			_svc = new DriverService(aDriverRepository.Object);
		}

		[Fact]
		public void GetDriverByLastNameTest()
		{
			const string aName = "Smith";

			var aResults = _svc.GetDriverByLastName(aName);

			Assert.Equal(2, aResults.Count());
		}

		[Fact]
		public void GetDriverTest()
		{
			var aDriver = _svc.GetDriver(1,2);

			Assert.NotNull(aDriver);
			Assert.Equal(2, aDriver.Id);
		}

		[Fact]
		public void AddDriverTest()
		{
			var aDriverCount = _driverList.Count;

			var aNewDriver = new Driver
			{
				Address = "1 anystreet st",
				City = "anytown",
				Country = "United States",
				CustomerId = 1,
				FirstName = "Stephanie",
				LastName = "Smith",
				LicenseState = "MA",
				LicenseNumber = "67890",
				State = "MA",
				PostalCode = "12345"
			};

			var aReturn = _svc.AddDriver(aNewDriver);

			Assert.Equal(aDriverCount+1, aReturn);
		}

		[Fact]
		public async Task GetDriversByCustomerAsync()
		{
			var aDrivers = await _svc.GetDriversByCustomerAsync(1);

			Assert.NotEmpty(aDrivers);
		}

		[Fact]
		public async Task GetDriversByCustomerEmptyAsync()
		{
			var aDrivers = await _svc.GetDriversByCustomerAsync(2);

			Assert.Empty(aDrivers);
		}

		[Fact]
		public async Task GetDriverAsyncTest()
		{
			var aDriver = await _svc.GetDriverAsync(1, 1);

			Assert.NotNull(aDriver);
		}

		[Fact]
		public async Task GetDriverAsyncNullTest()
		{
			var aDriver = await _svc.GetDriverAsync(3, 1);

			Assert.Null(aDriver);
		}

		[Fact]
		public async Task AddDriverAsyncTest()
		{
			var aDriverCount = _driverList.Count;

			var aNewDriver = new Driver
			{
				Address = "1 anystreet st",
				City = "anytown",
				Country = "United States",
				CustomerId = 1,
				FirstName = "Joan",
				LastName = "Jones",
				LicenseState = "MA",
				LicenseNumber = "67890",
				State = "MA",
				PostalCode = "12345"
			};

			var aReturn =await _svc.AddDriverAsync(aNewDriver);

			Assert.Equal(aDriverCount+1, aReturn);
		}

		[Fact]
		public async Task UpdateDriverAsyncTest()
		{
			var aDriver = await _svc.GetDriverAsync(1, 1);

			Assert.NotNull(aDriver);

			aDriver.LastName = "McTavish";

			await _svc.UpdateDriverAsync(aDriver);
		}

		[Fact]
		public async Task DeleteDriverAsyncTest()
		{
			var aDriver = await _svc.GetDriverAsync(1, 1);

			Assert.NotNull(aDriver);

			await _svc.DeleteDriverAsync(aDriver);
		}
    }
}
