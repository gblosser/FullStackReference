using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;

namespace TSD.Reference.Data.SQLite.Repositories
{
	public class UserRepository : AbstractRepository, IUserRepository
	{
		public UserRepository()
		{
			
		}
		public User GetUser(int theUserId)
		{
			throw new NotImplementedException();
		}

		public User GetUserByEmail(string theEmail)
		{
			throw new NotImplementedException();
		}

		public int AddUser(User theUser)
		{
			throw new NotImplementedException();
		}

		public void UpdateUser(User theUser)
		{
			throw new NotImplementedException();
		}

		public void DeleteUser(User theUser)
		{
			throw new NotImplementedException();
		}

		public Task<User> GetUserAsync(int theUserId)
		{
			throw new NotImplementedException();
		}

		public Task<User> GetUserByEmailAsync(string theEmail)
		{
			throw new NotImplementedException();
		}

		public Task<int> AddUserAsync(User theUser)
		{
			throw new NotImplementedException();
		}

		public Task UpdateUserAsync(User theUser)
		{
			throw new NotImplementedException();
		}

		public Task DeleteUserAsync(User theUser)
		{
			throw new NotImplementedException();
		}
	}
}
