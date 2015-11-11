using System.Collections.Generic;
using System.Threading.Tasks;
using TSD.Reference.Core.Entities;

namespace TSD.Reference.Core.Data
{
	/// <summary>
	/// Interface for handling application user data
	/// </summary>
	public interface IUserRepository
	{
		User GetUser(int theUserId);
		User GetUserByUserName(string theUserName);
		int AddUser(User theUser);
		void UpdateUser(User theUser);
		void DeleteUser(User theUser);
		bool VerifyPassword(string theUserName, string thePassword, int theCustomerId);
		bool VerifyPassword(int theUserId, string thePassword, int theCustomerId);
		void ChangePassword(string theOldPassword, string theNewPassword, string theNewPasswordConfirmed,
			int theUserId, int theCustomerId);


		Task<User> GetUserAsync(int theUserId);
		Task<User> GetUserByUserNameAsync(string theUserName);
		Task<int> AddUserAsync(User theUser);
		Task UpdateUserAsync(User theUser);
		Task DeleteUserAsync(User theUser);
		Task<IEnumerable<User>> GetUsersForCustomerAsync(int theCustomerId);
		Task<bool> VerifyPasswordAsync(string theUserName, string thePassword, int theCustomerId);
		Task<bool> VerifyPasswordAsync(int theUserId, string thePassword, int theCustomerId);
		Task ChangePasswordAsync(string theOldPassword, string theNewPassword, string theNewPasswordConfirmed,
			int theUserId, int theCustomerId);
	}
}
