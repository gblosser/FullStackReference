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
			 var aMockRepo = new Mock<IAutomobileRepository>();
			aMockRepo.Setup(aItem => aItem.AddAutomobile(It.IsAny<Automobile>())).Returns(1);

			var aMockLocationRepo = new Mock<ILocationRepository>();

			aMockRepo.Setup(aItem => aItem.GetAutomobiles(It.IsAny<List<int>>())).Returns(
				new List<Automobile>
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
					}
				});

			aMockRepo.Setup(aItem => aItem.AddAutomobileAsync(It.IsAny<Automobile>())).Returns(Task.FromResult(100));

			aMockRepo.Setup(aItem => aItem.GetAutomobilesAsync(It.IsAny<List<int>>())).Returns(	Task.FromResult(
				new List<Automobile>
				{
					new Automobile
					{
						Class = "Full size",
						Code = "FCAR",
						Color = "Grey",
						Id = 1,
						LocationId = 1,
						Manufacturer = "Chevrolet",
						Model = "Impala",
						Name = "Chevy Impala",
						Style = "Sedan",
						VehicleNumber = "RENT-1",
						VIN = "XYZ123ABC789async"
					}
				}.AsEnumerable()));

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
			var aCar = _svc.GetAutomobiles(new List<int> {1});

			Assert.Equal(1, aCar.Count());
			Assert.Equal(aCar.First().VIN, "XYZ123ABC789chevy");
		}

		[Fact]
		public async Task AddAutomobileTestAsync()
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
		public async Task GetAutomobileTestAsync()
		{
			var aCar = await _svc.GetAutomobilesAsync(new List<int> { 1 });

			Assert.Equal(1, aCar.Count());
			Assert.Equal(aCar.First().VIN, "XYZ123ABC789async");
		}
	}
}
