using System;
using Newtonsoft.Json;
using TSD.Reference.Core.Crypto;
using TSD.Reference.Core.Entities;
using TSD.Reference.Core.Services.Interfaces;

namespace TSD.Reference.Core.Services
{
	/// <summary>
	/// Used for creating, retrieving and validating tokens that are created using UserCredentials objects
	/// </summary>
	public class LoginService : ILoginService
	{
		private const string _phrase = "p_o_c_p_w_d";
		private const string _bearer = "Bearer ";

		/// <summary>
		/// Returns an encrypted token for use with UserCredentials
		/// </summary>
		/// <param name="theUserCredentials">UserCredentials object</param>
		/// <param name="theLifetimeInMinutes">Lifetime that token is valid</param>
		/// <returns>Encrypted token</returns>
		public string GetToken(UserCredentials theUserCredentials, int theLifetimeInMinutes)
		{
			var aTemporaryUserCredentials = new TemporaryUserCredentials(theUserCredentials, theLifetimeInMinutes);

			var aJsonObject = JsonConvert.SerializeObject(aTemporaryUserCredentials);

			var aEncryptedJsonObject = StringCipher.Encrypt(aJsonObject, _phrase);

			return CreateBearerToken(aEncryptedJsonObject);
		}

		/// <summary>
		/// Validates a token against the UserCredentials and LifetimeInMinutes used to create it.
		/// </summary>
		/// <param name="theToken"></param>
		/// <returns></returns>
		public bool ValidateToken(string theToken)
		{

			try
			{
				var aUserCredentials = GetDecryptedUserCredentials(theToken);

				if (aUserCredentials == null)
					return false;

				if (DateTime.Now > aUserCredentials.Expiration)
					return false;

				return true;
			}
			catch (NotSupportedException ex)
			{
				return false;
			}
			catch (ArgumentOutOfRangeException ex)
			{
				return false;
			}
			catch (ArgumentException ex)
			{
				return false;
			}
			catch (System.Security.Cryptography.CryptographicException ex)
			{
				return false;
			}
			catch (System.FormatException ex)
			{
				return false;
			}
			catch (Newtonsoft.Json.JsonReaderException ex)
			{
				return false;
			}
		}

		/// <summary>
		/// Decodes a token and returns the UserCredentials with expiration
		/// </summary>
		/// <param name="theToken"></param>
		/// <returns></returns>
		public UserCredentials DecodeToken(string theToken)
		{
			return GetDecryptedUserCredentials(theToken).GetUserCredentials();
		}


		private static TemporaryUserCredentials GetDecryptedUserCredentials(string theToken)
		{
			var aTokenString = theToken.Replace(_bearer, string.Empty);

			var aDecryptedToken = StringCipher.Decrypt(aTokenString, _phrase);

			var aUserCredentials = JsonConvert.DeserializeObject<TemporaryUserCredentials>(aDecryptedToken);

			return aUserCredentials;
		}

		/// <summary>
		/// Creates the Bearer Token that will be placed on the authorization header
		/// </summary>
		/// <param name="theToken"></param>
		/// <returns></returns>
		private static string CreateBearerToken(string theToken)
		{
			return $"{_bearer}{theToken}";
		}

		/// <summary>
		/// Internal representation of expiring user credentials
		/// </summary>
		private class TemporaryUserCredentials
		{
			public TemporaryUserCredentials(){}

			public TemporaryUserCredentials(UserCredentials theUserCredentials, int theLifetimeInMinutes)
			{
				UserName = theUserCredentials.UserName;
				Password = theUserCredentials.Password;
				Expiration = DateTime.Now.AddMinutes(theLifetimeInMinutes);
			}

			internal UserCredentials GetUserCredentials()
			{
				return new UserCredentials {UserName = this.UserName, Password = this.Password};
			}

			public DateTime Expiration { get; set; }

			public string UserName{get; set; }

			public string Password { get; set; }
		}
	}
}
