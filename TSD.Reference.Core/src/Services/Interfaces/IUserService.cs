using System.Collections.Generic;
using System.Threading.Tasks;
using TSD.Reference.Core.Entities;

namespace TSD.Reference.Core.Services.Interfaces
{
	public interface IUserService
	{
		User GetUser(int theUserId);
		User GetUserByEmail(string theUserEmail);
		Task<IEnumerable<User>> GetUsersForCustomerAsync(int theCustomerId);
		Task<User> GetUserAsync(int aCustomerId, int id);
		Task<int> AddUserAsync(User user);
		Task UpdateUserAsync(User user);
		Task DeleteUserAsync(User user);
	}
}