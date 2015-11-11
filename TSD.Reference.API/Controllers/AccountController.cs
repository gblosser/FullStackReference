using System.Threading.Tasks;
using System.Web.Http;
using TSD.Reference.API.Extensions;
using TSD.Reference.Core.Entities;
using TSD.Reference.Core.Services.Interfaces;

namespace TSD.Reference.API.Controllers
{
	/// <summary>
	/// Controller used to register a user and change their password
	/// </summary>
	[RoutePrefix("api/Account")]
	[Authorize]
	public class AccountController : ApiController
	{
		private readonly IUserService _userService;

		public AccountController(IUserService theUserService)
		{
			_userService = theUserService;
		}

		/// <summary>
		/// Call /api/token with the following FORM parameters in order to obtain a token:
		///   
		/// username	[your_username]
		/// password	[your_password]
		/// customer_id	[your_customer_id]
		/// grant_type password
		/// client_id poc
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		[AllowAnonymous]
		[Route("Register")]
		public async Task Register(User user)
		{
			await _userService.AddUserAsync(user);
		}

		[Route("ChangePassword")]
		public async Task ChangePassword(PasswordChange passwordChange)
		{
			var aUserId = this.GetUserId();
			var aCustomerId = this.GetCustomerId();

			await _userService.ChangePasswordAsync(passwordChange, aUserId, aCustomerId);
		}
	}
}
