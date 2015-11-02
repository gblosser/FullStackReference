using System.Web.Http;
using TSD.Reference.Core.Entities;
using TSD.Reference.Core.Services.Interfaces;

namespace TSD.Reference.API.Controllers
{
	public class LogInController : ApiController
	{
		private readonly ILoginService _loginService;

		public LogInController(ILoginService theLoginService)
		{
			_loginService = theLoginService;
		}


		// POST: api/LogIn
		public string Post(UserCredentials userCredentials)
		{
			return _loginService.GetToken(userCredentials, 15);
		}

	}
}
