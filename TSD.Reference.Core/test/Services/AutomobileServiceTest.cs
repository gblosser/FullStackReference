using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;
using TSD.Reference.Core.Services;

namespace TSD.Reference.Core.test.Services
{
	[TestClass]
	public class AutomobileServiceTest
	{
		private AutomobileService _svc;

		[TestInitialize]
		public void Initialize()
		{
			var aMockRepo = new Mock<IAutomobileRepository>();
			aMockRepo.Setup(aItem => aItem.AddAutomobile(It.IsAny<Automobile>())).Returns(1);

			_svc = new AutomobileService(aMockRepo.Object);
		}

		[TestMethod]
		public void AddAutomobileTest()
		{
			var aReturn = _svc.AddAutomobile(new Automobile());

			Assert.AreEqual(1, aReturn);
		}
	}
}
