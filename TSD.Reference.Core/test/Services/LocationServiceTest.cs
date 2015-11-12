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
	public class LocationServiceTest
	{
		private readonly ILocationService _service;

		public LocationServiceTest()
		{
			var aRepository = new Mock<ILocationRepository>();

			var aLocation = new Location
			{
				Address = "1 Main St",
				City = "Anytown",
				Country = "US",
				CustomerId = 1,
				Id = 1,
				Name = "Store #1",
				PostalCode = "12345",
				State = "NY"
			};

			aRepository.Setup(aItem => aItem.GetLocation(1)).Returns(aLocation);
			aRepository.Setup(aItem => aItem.GetLocationAsync(1)).Returns(Task.FromResult(aLocation));
			aRepository.Setup(aItem => aItem.AddLocation(It.IsAny<Location>())).Returns((Location theLocation) => theLocation.Id);
			aRepository.Setup(aItem => aItem.AddLocationAsync(It.IsAny<Location>())).Returns(Task.FromResult(2));
			aRepository.Setup(aItem => aItem.DeleteLocation(It.IsAny<Location>()));
			aRepository.Setup(aItem => aItem.DeleteLocationAsync(It.IsAny<Location>())).Returns(Task.Run(() => { }));
			aRepository.Setup(aItem => aItem.UpdateLocation(It.IsAny<Location>()));
			aRepository.Setup(aItem => aItem.UpdateLocationAsync(It.IsAny<Location>())).Returns(Task.Run(() => { }));
			aRepository.Setup(aItem => aItem.GetLocationsForCustomerAsync(1))
				.Returns(Task.FromResult(new List<Location> {aLocation}.AsEnumerable()));

			_service = new LocationService(aRepository.Object);
		}

		[Fact]
		public void GetLocationTest()
		{
			var aLocation = _service.GetLocation(1);

			Assert.NotNull(aLocation);
		}

		[Fact]
		public async Task GetLocationAsyncTest()
		{
			var aLocation = await _service.GetLocationAsync(1);

			Assert.NotNull(aLocation);
		}

		[Fact]
		public void AddLocationTest()
		{
			var aLocation = new Location
			{
				Address = "2 Main St",
				City = "Anytown",
				Country = "US",
				CustomerId = 1,
				Id = 2,
				Name = "Store #2",
				PostalCode = "12345",
				State = "NY"
			};
			var aResult = _service.AddLocation(aLocation);

			Assert.Equal(2, aResult);
		}

		[Fact]
		public async Task AddLocationAsyncTest()
		{
			var aLocation = new Location
			{
				Address = "2 Main St",
				City = "Anytown",
				Country = "US",
				CustomerId = 1,
				Id = 2,
				Name = "Store #2",
				PostalCode = "12345",
				State = "NY"
			};
			var aResult = await _service.AddLocationAsync(aLocation);

			Assert.Equal(2, aResult);
		}

		[Fact]
		public void DeleteLocationTest()
		{
			var aLocation = _service.GetLocation(1);

			Assert.NotNull(aLocation);

			_service.DeleteLocation(aLocation);
		}

		[Fact]
		public async Task DeleteLocationAsyncTest()
		{
			//var aLocation = await _service.GetLocationAsync(1);

			//Assert.NotNull(aLocation);

			var aLocation = new Location
			{
				Address = "2 Main St",
				City = "Anytown",
				Country = "US",
				CustomerId = 1,
				Id = 2,
				Name = "Store #2",
				PostalCode = "12345",
				State = "NY"
			};

			await _service.DeleteLocationAsync(aLocation);
		}

		[Fact]
		public void UpdateLocationTest()
		{
			var aLocation = _service.GetLocation(1);

			Assert.NotNull(aLocation);

			_service.UpdateLocation(aLocation);
		}

		[Fact]
		public async Task UpdateLocationAsyncTest()
		{
			var aLocation = await _service.GetLocationAsync(1);

			Assert.NotNull(aLocation);

			await _service.UpdateLocationAsync(aLocation);
		}

		[Fact]
		public async Task GetLocationsForCustomerAsyncTest()
		{
			var aResults = await _service.GetLocationsForCustomerAsync(1);

			Assert.NotEmpty(aResults);
		}
    }
}
