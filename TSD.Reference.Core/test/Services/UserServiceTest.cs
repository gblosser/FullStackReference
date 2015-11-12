using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;
using TSD.Reference.Core.Exceptions;
using TSD.Reference.Core.Services;
using TSD.Reference.Core.Services.Interfaces;
using Xunit;

namespace TSD.Reference.Core.test.Services
{
	public class UserServiceTest
	{
		private readonly IUserService _service;
		const string _santaUserName = "FatherChristmas";
		const string _newPwd = "qfqfqrexfgrqfe";

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
				Username = _santaUserName
			};

			aUserRepository.Setup(aItem => aItem.GetUser(It.IsAny<int>())).Returns(aEmployeeUser);
			aUserRepository.Setup(aItem => aItem.GetUserAsync(It.IsAny<int>())).Returns(Task.FromResult(aEmployeeUser));
			aUserRepository.Setup(aItem => aItem.GetUserByUserName(_santaUserName)).Returns(aEmployeeUser);
			aUserRepository.Setup(aItem => aItem.GetUsersForCustomerAsync(It.IsAny<int>()))
				.Returns(Task.FromResult(new List<User> {aEmployeeUser}.AsEnumerable()));
			aUserRepository.Setup(aItem => aItem.AddUserAsync(It.IsAny<User>())).Returns(Task.FromResult(2));
			aUserRepository.Setup(aItem => aItem.UpdateUserAsync(It.IsAny<User>())).Returns(Task.Run(() => { }));
			aUserRepository.Setup(aItem => aItem.DeleteUserAsync(It.IsAny<User>())).Returns(Task.Run(() => { }));
			aUserRepository.Setup(aItem => aItem.VerifyPasswordAsync(_santaUserName, "xyzpdq", 1)).Returns(Task.FromResult(true));
			aUserRepository.Setup(aItem => aItem.ChangePasswordAsync("xyzpdq", _newPwd, _newPwd, 1, 1))
				.Returns(Task.Run(() => { }));

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

		[Fact]
		public void GetUserByUserNameTest()
		{
			var aUser = _service.GetUserByUserName(_santaUserName);

			Assert.NotNull(aUser);
			Assert.Equal("Santa", aUser.FirstName);
		}

		[Fact]
		public async Task GetUsersForCustomerAsyncTest()
		{
			var aUsers = await _service.GetUsersForCustomerAsync(1);

			Assert.NotEmpty(aUsers);
		}

		[Fact]
		public async Task AddUserAsyncTest()
		{
			var aNewUser = new User
			{
				CustomerId = 1,
				Email = "email@email.com",
				FirstName = "Santa",
				Id = 2,
				IsEmployee = true,
				LastName = "Baby",
				Password = "w54c h",
				Username = "JellyBelly"
			};

			var aId = await _service.AddUserAsync(aNewUser);

			Assert.Equal(2, aId);
		}

		[Fact]
		public async Task UpdateUserAsyncTest()
		{
			var aUser = await _service.GetUserAsync(1, 1);

			Assert.NotNull(aUser);

			aUser.Username = "FatRedMan";

			await _service.UpdateUserAsync(aUser);
		}

		[Fact]
		public async Task DeleteUserAsyncTest()
		{
			var aUser = await _service.GetUserAsync(1, 1);

			Assert.NotNull(aUser);

			await _service.DeleteUserAsync(aUser);
		}

		[Fact]
		public async Task VerifyPasswordAsyncTest()
		{
			var aResult = await _service.VerifyPasswordAsync(_santaUserName, "xyzpdq", 1);

			Assert.True(aResult);
		}

		[Fact]
		public async Task ChangePasswordAsyncTest()
		{
			await
				_service.ChangePasswordAsync(
					new PasswordChange {ConfirmNewPassword = _newPwd, NewPassword = _newPwd, OldPassword = "xyzpdq"}, 1, 1);
		}
    }
}
