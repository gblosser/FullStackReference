using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using Moq;
using TSD.Reference.API.Controllers;
using TSD.Reference.Core.Entities;
using TSD.Reference.Core.Services.Interfaces;
using Xunit;

namespace TSD.Reference.API.test.Controllers
{
	public class AutomobileControllerTest
	{
		private readonly AutomobileController _controller;

		public AutomobileControllerTest()
		{
			var aAutomobileList = new List<Automobile>
			{
				new Automobile
				{
					Id = 5,
					VIN = "PDQ123ABC789chevy",
					VehicleNumber = "RENT-2",
					Name = "Chevy Impala",
					Class = "Full Size",
					Style = "Sedan",
					Color = "Red",
					Manufacturer = "Chevrolet",
					Model = "Impala",
					Code = "FCAR",
					LocationId = 1
				},
				new Automobile
				{
					Id = 6,
					VIN = "QRS123ABC7890ford",
					VehicleNumber = "RENT-3",
					Name = "Ford Taurus",
					Class = "Full Size",
					Style = "Sedan",
					Color = "Green",
					Manufacturer = "Ford",
					Model = "Taurus",
					Code = "FCAR",
					LocationId = 1
				},
				new Automobile
				{
					Id = 1,
					VIN = "XYZ123ABC789chevy",
					VehicleNumber = "RENT-1",
					Name = "Chevy Impala",
					Class = "Full Size",
					Style = "Sedan",
					Color = "Grey",
					Manufacturer = "Chevrolet",
					Model = "Impala",
					Code = "FCAR",
					LocationId = 1
				}
			};

			var aAutomobileService = new Mock<IAutomobileService>();
			aAutomobileService.Setup(aItem => aItem.GetAutomobilesForCustomerAsync(1))
				.ReturnsAsync(aAutomobileList.AsEnumerable());
			aAutomobileService.Setup(aItem => aItem.GetAutomobilesForCustomerAsync(2))
				.ReturnsAsync(Enumerable.Empty<Automobile>());
			aAutomobileService.Setup(aItem => aItem.GetAutomobileAsync(1, 1))
				.ReturnsAsync(aAutomobileList.FirstOrDefault(aItem => aItem.Id == 1));
			aAutomobileService.Setup(aItem => aItem.GetAutomobileAsync(2, 1))
				.ReturnsAsync(null);

			/*********************************************************************************************
				build mock data for ControllerContext so that context-dependent properties can be tested 
				(headers, routing, etc...)
			*********************************************************************************************/
			var config = new HttpConfiguration();
			var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/automobile");
			var route = config.Routes.MapHttpRoute("DefaultApi", "api/v{version}/{controller}/{id}");
			var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "Automobile" } });

			_controller = new AutomobileController(aAutomobileService.Object)
			{
				ControllerContext = new HttpControllerContext(config, routeData, request),
				Request = request
			};
			_controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
		}

		[Fact]
		public async Task GetCarsTest()
		{
			_controller.Request.Properties["custid"] = 1;
			var aCars = await _controller.Get();

			Assert.NotEmpty(aCars);
		}

		[Fact]
		public async Task GetCarsEmptyTest()
		{
			_controller.Request.Properties["custid"] = 2;

			var aCars = await _controller.Get();

			Assert.Empty(aCars);
		}

		[Fact]
		public async Task GetCarByIdTest()
		{
			_controller.Request.Properties["custid"] = 1;

			var aCar = await _controller.Get(1);

			Assert.Equal(1, aCar.Id);
		}

		[Fact]
		public async Task GetCarByIdNullTest()
		{
			_controller.Request.Properties["custid"] = 1;

			var aCar = await _controller.Get(2);

			Assert.Null(aCar);
		}
	}
}
