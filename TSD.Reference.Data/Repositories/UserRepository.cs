using System;
using System.Threading.Tasks;
using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;
using TSD.Reference.Data.SQLite.DTO;

namespace TSD.Reference.Data.SQLite.Repositories
{
	public class UserRepository : AbstractRepository, IUserRepository
	{
		public UserRepository()
		{
			var db = Connection;
			db.CreateTable<UserDTO>();
		}
		public User GetUser(int theUserId)
		{
			var aUser = Connection.Table<UserDTO>().FirstOrDefault(aItem => aItem.Id == theUserId);
			return aUser?.ToEntity();
		}

		public User GetUserByEmail(string theEmail)
		{
			var aUser = Connection.Table<UserDTO>().FirstOrDefault(aItem => aItem.Email == theEmail);
			return aUser?.ToEntity();
		}

		public int AddUser(User theUser)
		{
			return Connection.Insert(theUser.ToDTO());
		}

		public void UpdateUser(User theUser)
		{
			Connection.Update(theUser.ToDTO());
		}

		public void DeleteUser(User theUser)
		{
			Connection.Delete<RenterDTO>(theUser.Id);
		}

		public async Task<User> GetUserAsync(int theUserId)
		{
			var aUser = await ConnectionAsync.Table<UserDTO>().Where(aItem => aItem.Id == theUserId).FirstOrDefaultAsync();
			return aUser?.ToEntity();
		}

		public async Task<User> GetUserByEmailAsync(string theEmail)
		{
			var aUser = await ConnectionAsync.Table<UserDTO>().Where(aItem => aItem.Email == theEmail).FirstOrDefaultAsync();
			return aUser?.ToEntity();
		}

		public async Task<int> AddUserAsync(User theUser)
		{
			return await ConnectionAsync.InsertAsync(theUser.ToDTO());
		}

		public async Task UpdateUserAsync(User theUser)
		{
			await ConnectionAsync.UpdateAsync(theUser.ToDTO());
		}

		public async Task DeleteUserAsync(User theUser)
		{
			await ConnectionAsync.DeleteAsync(theUser.ToDTO());
		}
	}
}
