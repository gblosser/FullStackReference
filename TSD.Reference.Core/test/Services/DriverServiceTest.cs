using System.Collections.Generic;
using System.Linq;
using Moq;
using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;
using TSD.Reference.Core.Services;
using Xunit;

namespace TSD.Reference.Core.test.Services
{
	public class DriverServiceTest
	{
		private readonly DriverService _svc;

		public DriverServiceTest()
		{
			var aDriverList = new List<Driver>
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
				}
			};

			var aDriverRepository = new Mock<IDriverRepository>();
			aDriverRepository.Setup(aItem => aItem.GetDriverByLastName(It.IsAny<string>())).Returns(
				(string theName) =>
					aDriverList);
			aDriverRepository.Setup(aItem => aItem.GetDriver(It.IsAny<int>())).Returns(
				(int theId) => aDriverList.FirstOrDefault(aItem => aItem.Id == theId));

			aDriverRepository.Setup(aItem => aItem.AddDriver(It.IsAny<Driver>())).Returns(
				(Driver theNewDriver) =>
				{
					theNewDriver.Id = aDriverList.Count + 1;
					aDriverList.Add(theNewDriver);
					return aDriverList.Count;
				});

			_svc = new DriverService(aDriverRepository.Object);
		}

		[Fact]
		public void GetDriverByLastNameTest()
		{
			const string aName = "Smith";

			var aResults = _svc.GetDriverByLastName(aName);

			Assert.Equal(true, aResults.Any());
			Assert.True(aResults.All(aItem => aItem.LastName == aName));
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

			Assert.Equal(3, aReturn);
		}
	}
}
