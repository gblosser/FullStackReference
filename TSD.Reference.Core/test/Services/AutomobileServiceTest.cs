using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;
using TSD.Reference.Core.Services;
using Xunit;

namespace TSD.Reference.Core.test.Services
{
	public class AutomobileServiceTest
	{
		private readonly AutomobileService _svc;

		public AutomobileServiceTest()
		{
			var aAutomobiles = new List<Automobile>
			{
				new Automobile
				{
					Class = "Full Size",
					Code = "FCAR",
					Color = "Grey",
					Id = 1,
					LocationId = 1,
					Manufacturer = "Chevrolet",
					Model = "Impala",
					Name = "Chevy Impala",
					Style = "Sedan",
					VehicleNumber = "RENT-1",
					VIN = "XYZ123ABC789chevy"
				},
				new Automobile
				{
					Class = "Compact",
					Code = "ECAR",
					Color = "White",
					Id = 2,
					LocationId = 1,
					Manufacturer = "Chevrolet",
					Model = "Cruze",
					Name = "Chevy Cruze",
					Style = "Hatchback",
					VehicleNumber = "RENT-2",
					VIN = "XYZ123ABC543chevy"
				},
				new Automobile
				{
					Class = "Compact",
					Code = "ECAR",
					Color = "Red",
					Id = 3,
					LocationId = 1,
					Manufacturer = "Chevrolet",
					Model = "Cruze",
					Name = "Chevy Cruze",
					Style = "Hatchback",
					VehicleNumber = "RENT-3",
					VIN = "XYZ123ABC328chevy"
				}
			};

			var aMockRepo = new Mock<IAutomobileRepository>();
			aMockRepo.Setup(aItem => aItem.AddAutomobile(It.IsAny<Automobile>())).Returns(1);

			aMockRepo.Setup(aItem => aItem.GetAutomobiles(It.IsAny<List<int>>())).Returns(aAutomobiles);

			aMockRepo.Setup(aItem => aItem.AddAutomobileAsync(It.IsAny<Automobile>())).Returns(Task.FromResult(100));

			aMockRepo.Setup(aItem => aItem.GetAutomobilesAsync(It.IsAny<List<int>>())).Returns(Task.FromResult(aAutomobiles.AsEnumerable()));

			aMockRepo.Setup(aItem => aItem.GetAutomobilesForLocationAsync(1))
				.Returns(Task.FromResult(aAutomobiles.AsEnumerable()));

			aMockRepo.Setup(aItem => aItem.GetAutomobilesForLocationAsync(It.IsInRange(2, int.MaxValue, Range.Inclusive)))
			  .Returns(Task.FromResult(Enumerable.Empty<Automobile>()));

			aMockRepo.Setup(aItem => aItem.GetAutomobilesForLocationsAsync(It.IsAny<IEnumerable<int>>()))
				.Returns(Task.FromResult(aAutomobiles.AsEnumerable()));

			aMockRepo.Setup(aItem => aItem.GetAutomobileAsync(1))
				.Returns(Task.FromResult(aAutomobiles[0]));

			aMockRepo.Setup(aItem => aItem.UpdateAutomobileAsync(It.IsIn(aAutomobiles.AsEnumerable())));
			aMockRepo.Setup(aItem => aItem.UpdateAutomobileAsync(It.IsNotIn(aAutomobiles.AsEnumerable()))).Throws<ApplicationException>();

			var aMockLocationRepo = new Mock<ILocationRepository>();
			aMockLocationRepo.Setup(aItem => aItem.GetLocationsForCustomerAsync(1)).Returns(Task.FromResult(
				new List<Location>
				{
					 new Location
					 {
						 Address = "address",
						 City = "city",
						 Country = "country",
						 CustomerId = 1,
						 Id=1,
						 Name="Location 1",
						 PostalCode = "11111",
						 State = "New York"
					 } ,
					 new Location
						{
							Address = "address 2",
							City = "city 2",
							Country = "country",
							CustomerId = 1,
							Id = 2,
							Name = "Location 2",
							PostalCode = "11121",
							State = "New York"
						}
				}.AsEnumerable()));

			aMockLocationRepo.Setup(aItem => aItem.GetLocationsForCustomerAsync(2))
				.Returns(Task.FromResult(Enumerable.Empty<Location>()));

			_svc = new AutomobileService(aMockRepo.Object, aMockLocationRepo.Object);
		}

		[Fact]
		public void AddAutomobileTest()
		{
			var aReturn = _svc.AddAutomobile(new Automobile());

			Assert.Equal(1, aReturn);
		}

		[Fact]
		public void GetAutomobileTest()
		{
			var aCar = _svc.GetAutomobiles(new List<int> { 1 });

			Assert.Equal(aCar.First().VIN, "XYZ123ABC789chevy");
		}

		[Fact]
		public async Task AddAutomobileAsyncTest()
		{
			var aResponse = await _svc.AddAutomobileAsync(new Automobile
			{
				Class = "Full size",
				Code = "FCAR",
				Color = "Grey",
				LocationId = 1,
				Manufacturer = "Chevrolet",
				Model = "Impala",
				Name = "Chevy Impala",
				Style = "Sedan",
				VehicleNumber = "RENT-1",
				VIN = "XYZ123ABC789chevy"
			});

			Assert.Equal(100, aResponse);
		}


		[Fact]
		public async Task GetAutomobileAsyncTest()
		{
			var aCar = await _svc.GetAutomobilesAsync(new List<int> { 1 });

			Assert.Equal(aCar.First().VIN, "XYZ123ABC789chevy");
		}

		[Fact]
		public async Task GetAutomobilesForLocationAsyncTest()
		{
			var aCars = await _svc.GetAutomobilesForLocationAsync(1, 1);

			Assert.NotEmpty(aCars);
		}

		[Fact]
		public async Task GetAutomobilesForLocationAsyncEmptyTest()
		{
			var aCars = await _svc.GetAutomobilesForLocationAsync(1, 2);

			Assert.Empty(aCars);
		}

		[Fact]
		public async Task GetAutomobilesForCustomerWithNoLocationsThrowsTest()
		{
			await Assert.ThrowsAsync<ApplicationException>(() => _svc.GetAutomobilesForLocationAsync(2, 2));
		}

		[Fact]
		public async Task GetAutomobilesForCustomerForWrongLocationThrowsTest()
		{
			await Assert.ThrowsAsync<ApplicationException>(() => _svc.GetAutomobilesForLocationAsync(1, 10));
		}

		[Fact]
		public async Task UpdateAutmobileAsyncTest()
		{
			var aCar = await _svc.GetAutomobileAsync(1, 1);

			Assert.Equal("Grey", aCar.Color);

			aCar.Color = "White";

			await _svc.UpdateAutomobileAsync(1, aCar);
		}

		[Fact]
		public async Task UpdateAutmobileAsyncThrowsTest()
		{
			var aCar = await _svc.GetAutomobileAsync(1, 1);

			Assert.Equal("Grey", aCar.Color);

			aCar.LocationId = 3;

			await Assert.ThrowsAsync<ApplicationException>(() => _svc.UpdateAutomobileAsync(1, aCar));
		}

		[Fact]
		public async Task GetAutomobilesForCustomerAsyncTest()
		{
			var aCars = await _svc.GetAutomobilesForCustomerAsync(1);

			Assert.NotEmpty(aCars);
		}

		[Fact]
		public async Task GetAutomobilesForCustomerWithNoLocationsAsyncTest()
		{
			var aCars = await _svc.GetAutomobilesForCustomerAsync(2);

			Assert.Empty(aCars);
		}
	}
}
