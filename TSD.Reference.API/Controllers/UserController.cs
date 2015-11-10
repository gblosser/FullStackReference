using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using TSD.Reference.API.Extensions;
using TSD.Reference.Core.Entities;
using TSD.Reference.Core.Services.Interfaces;
using WebApi.OutputCache.V2;

namespace TSD.Reference.API.Controllers
{
	[Authorize]
	[AutoInvalidateCacheOutput]
	public class UserController : ApiController
	{
		private readonly IUserService _userService;

		public UserController(IUserService theUserService)
		{
			_userService = theUserService;
		}

		// GET: api/User
		//[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 300)]
		public async Task<IEnumerable<User>> Get()
		{
			var aCustomerId = this.GetCustomerId();

			return await _userService.GetUsersForCustomerAsync(aCustomerId);
		}

		// GET: api/User/5
		[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 300)]
		public async Task<User> Get(int id)
		{
			var aCustomerId = this.GetCustomerId();

			return await _userService.GetUserAsync(aCustomerId, id);
		}

		// POST: api/User
		public async Task<int> Post(User user)
		{
			return await _userService.AddUserAsync(user);
		}

		// PUT: api/User/5
		public async Task Put(User user)
		{
			await _userService.UpdateUserAsync(user);
		}

		// DELETE: api/User/5
		public async Task Delete(User user)
		{
			await _userService.DeleteUserAsync(user);
		}
	}
}
