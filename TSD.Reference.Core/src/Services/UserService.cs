using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;

namespace TSD.Reference.Core.Services
{
	public class UserService
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

		public User GetUserByEmail(string theUserEmail)
		{
			return _userRepository.GetUserByEmail(theUserEmail);
		}
	}
}
