using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;
using TSD.Reference.Core.Services.Interfaces;

namespace TSD.Reference.Core.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository theUserRepository)
		{
			_userRepository = theUserRepository;
		}

		public User GetUser(int theUserId)
		{
			return _userRepository.GetUser(theUserId);
		}

		public User GetUserByUserName(string theUserName)
		{
			return _userRepository.GetUserByUserName(theUserName);
		}

		public async Task<IEnumerable<User>> GetUsersForCustomerAsync(int theCustomerId)
		{
			return await _userRepository.GetUsersForCustomerAsync(theCustomerId);
		}

		public async Task<User> GetUserAsync(int aCustomerId, int id)
		{
			var aUser = await _userRepository.GetUserAsync(id);

			return aUser.CustomerId == aCustomerId ? aUser : null;
		}

		public async Task<int> AddUserAsync(User user)
		{
			return await _userRepository.AddUserAsync(user);
		}

		public async Task UpdateUserAsync(User user)
		{
			await _userRepository.UpdateUserAsync(user);
		}
		public async Task DeleteUserAsync(User user)
		{
			await _userRepository.DeleteUserAsync(user);
		}

		public async Task<bool> VerifyPasswordAsync(string theUserEmail, string thePassword)
		{
			return await _userRepository.VerifyPasswordAsync(theUserEmail, thePassword);
		}

		public async Task<bool> VerifyPasswordAsync(int theUserId, string thePassword)
		{
			return await _userRepository.VerifyPasswordAsync(theUserId, thePassword);
		}

		public async Task ChangePasswordAsync(PasswordChange thePasswordChange, int theUserId)
		{
			await _userRepository.ChangePasswordAsync(thePasswordChange.OldPassword, thePasswordChange.NewPassword, thePasswordChange.ConfirmNewPassword, theUserId);
		}
	}
}
