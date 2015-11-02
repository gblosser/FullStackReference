using TSD.Reference.Core.Entities;
using TSD.Reference.Core.Services;
using TSD.Reference.Core.Services.Interfaces;
using Xunit;

namespace TSD.Reference.Core.test.Services
{
	public class LoginServiceTest
	{
		private readonly ILoginService _service;
		private readonly UserCredentials _userCredentials;
		private const int _expirationMinutes = 1;

		public LoginServiceTest()
		{
			
			_service = new LoginService();
			_userCredentials = new UserCredentials {UserName = "test", Password = "testPassword"};
		}

		[Fact]
		public void GetTokenTest()
		{
			var aToken = _service.GetToken(_userCredentials, _expirationMinutes);

			Assert.NotNull(aToken);
		}

		[Fact]
		public void ValidateTokenTest()
		{
			var aToken = _service.GetToken(_userCredentials, _expirationMinutes);

			Assert.NotNull(aToken);

			var aIsValid = _service.ValidateToken(aToken);

			Assert.True(aIsValid);
		}

		[Fact]
		public void DecodeTokenTest()
		{
			var aToken = _service.GetToken(_userCredentials, _expirationMinutes);

			Assert.NotNull(aToken);

			var aUserCredentials = _service.DecodeToken(aToken);

			Assert.Equal(_userCredentials.Password, aUserCredentials.Password);
		}

		[Fact]
		public void InvalidTokenTest()
		{
			var aToken = _service.GetToken(_userCredentials, -1);

			Assert.NotNull(aToken);

			var aIsValid = _service.ValidateToken(aToken);

			Assert.False(aIsValid);
		}

		[Fact]
		public void CorruptTokenTest()
		{
			var aToken = _service.GetToken(_userCredentials, -1);

			Assert.NotNull(aToken);

			var aNewToken = aToken.Replace(aToken[3], 'u');
			var aIsValid = _service.ValidateToken(aNewToken);

			Assert.False(aIsValid);
		}
	}
}
