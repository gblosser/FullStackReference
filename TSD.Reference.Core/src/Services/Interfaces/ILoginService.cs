using TSD.Reference.Core.Entities;

namespace TSD.Reference.Core.Services.Interfaces
{
	/// <summary>
	/// Used to get tokens on login and validate tokens on requests
	/// </summary>
	public interface ILoginService
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="theUserCredentials"></param>
		/// <param name="theLifetimeInMinutes"></param>
		/// <returns></returns>
		string GetToken(UserCredentials theUserCredentials, int theLifetimeInMinutes);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="theToken"></param>
		/// <returns></returns>
		bool ValidateToken(string theToken);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="theToken"></param>
		/// <returns></returns>
		UserCredentials DecodeToken(string theToken);
	}
}
