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

			_svc = new AutomobileService(aMockRepo.Object);
		}


		[Fact]
		public void AddAutomobileTest()
		{
			var aReturn = _svc.AddAutomobile(new Automobile());

			Assert.Equal(1, aReturn);
		}
	}
}
