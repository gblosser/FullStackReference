using System.Collections.Generic;
using System.Threading.Tasks;
using TSD.Reference.Core.Entities;

namespace TSD.Reference.Core.Data
{
	public interface IUserRepository
	{
		User GetUser(int theUserId);
		User GetUserByEmail(string theEmail);
		int AddUser(User theUser);
		void UpdateUser(User theUser);
		void DeleteUser(User theUser);

		Task<User> GetUserAsync(int theUserId);
		Task<User> GetUserByEmailAsync(string theEmail);
		Task<int> AddUserAsync(User theUser);
		Task UpdateUserAsync(User theUser);
		Task DeleteUserAsync(User theUser);
		Task<IEnumerable<User>> GetUsersForCustomerAsync(int theCustomerId);
	}
}
