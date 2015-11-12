using System.Threading.Tasks;
using Moq;
using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;
using TSD.Reference.Core.Services;
using TSD.Reference.Core.Services.Interfaces;
using Xunit;

namespace TSD.Reference.Core.test.Services
{
	public class UserServiceTest
	{
		private readonly IUserService _service;

		public UserServiceTest()
		{
			var aUserRepository = new Mock<IUserRepository>();

			var aEmployeeUser = new User
			{
				CustomerId = 1,
				Email = "email@email.com",
				FirstName = "Santa",
				Id = 1,
				IsEmployee = true,
				LastName = "Claus",
				Password = "xyzpdq",
				Username = "FatherChristmas"
			};

			aUserRepository.Setup(aItem => aItem.GetUser(It.IsAny<int>())).Returns(aEmployeeUser);
			aUserRepository.Setup(aItem => aItem.GetUserAsync(It.IsAny<int>())).Returns(Task.FromResult(aEmployeeUser));

			_service = new UserService(aUserRepository.Object);
		}

		[Fact]
		public void GetUserTest()
		{
			var aUser = _service.GetUser(1, 1);

			Assert.NotNull(aUser);
		}

		[Fact]
		public async Task GetUserAsyncTest()
		{
			var aUser = await _service.GetUserAsync(1, 1);

			Assert.NotNull(aUser);
		}
	}
}
