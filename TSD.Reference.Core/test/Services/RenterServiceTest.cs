using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using Moq;
using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;
using TSD.Reference.Core.Services;
using TSD.Reference.Core.Services.Interfaces;
using Xunit;

namespace TSD.Reference.Core.test.Services
{ 
	public class RenterServiceTest
	{
		private readonly IRenterService _service;

		public RenterServiceTest()
		{
			var aRepository = new Mock<IRenterRepository>();

			var aRenter = new Renter
			{
				Address = "1 Elm St",
				City = "North Andover",
				Country = "US",
				CustomerId = 1,
				FirstName = "Jon",
				LastName = "Anderson",
				Id = 1,
				InsuranceInfo = "Nationwide #B3214GH",
				LicenseNumber = "1234567890",
				State = "MA",
				PostalCode = "01845",
				LicenseState = "MA",
				PaymentInfo = "token %CT%$^C%G$WEFQ#$F%$"
			};

			aRepository.Setup(aItem => aItem.GetRenter(1)).Returns(aRenter);
			aRepository.Setup(aItem => aItem.GetRenterAsync(1)).ReturnsAsync(aRenter);
			aRepository.Setup(aItem => aItem.GetRentersForCustomerAsync(1))
				.ReturnsAsync(new List<Renter> {aRenter}.AsEnumerable());
			aRepository.Setup(aItem => aItem.UpdateRenterAsync(It.IsAny<Renter>())).Returns(Task.Run(() => { }));
			aRepository.Setup(aItem => aItem.AddRenterAsync(It.IsAny<Renter>())).ReturnsAsync(2);
			aRepository.Setup(aItem => aItem.DeleteRenterAsync(It.IsAny<Renter>())).Returns(Task.Run(() => { }));

			_service = new RenterService(aRepository.Object);
		}

		[Fact]
		public void GetRenterTest()
		{
			var aRenter = _service.GetRenter(1);

			Assert.Equal(1, aRenter.Id);
		}

		[Fact]
		public async Task GetRenterAsyncTest()
		{
			var aRenter = await _service.GetRenterAsync(1, 1);

			Assert.Equal(1, aRenter.Id);
		}

		[Fact]
		public async Task GetRenterAsyncNullTest()
		{
			var aRenter = await _service.GetRenterAsync(2, 1);

			Assert.Null(aRenter);
		}

		[Fact]
		public async Task GetRentersAsyncTest()
		{
			var aRenters = await _service.GetRentersAsync(1);

			Assert.NotEmpty(aRenters);
		}

		[Fact]
		public async Task AddRenterAsyncTest()
		{
			var aRenter = new Renter
			{
				Address = "1 Elm St",
				City = "North Andover",
				Country = "US",
				CustomerId = 1,
				FirstName = "Jon",
				LastName = "Anderson",
				Id = 1,
				InsuranceInfo = "Nationwide #B3214GH",
				LicenseNumber = "1234567890",
				State = "MA",
				PostalCode = "01845",
				LicenseState = "MA",
				PaymentInfo = "token %CT%$^C%G$WEFQ#$F%$"
			};

			var aResult = await _service.AddRenterAsync(aRenter);

			Assert.NotEqual(aResult, 0);
		}

		[Fact]
		public async Task UpdateRenterAsyncTest()
		{
			var aRenter = await _service.GetRenterAsync(1, 1);

			Assert.Equal(1, aRenter.Id);

			aRenter.Address = "456 Broadway";

			await _service.UpdateRenterAsync(aRenter);
		}

		[Fact]
		public async Task DeleteRenterAsyncTest()
		{
			var aRenter = await _service.GetRenterAsync(1, 1);

			Assert.Equal(1, aRenter.Id);

			await _service.DeleteRenterAsync(aRenter);
		}

	}
}
