using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using TSD.Reference.API.Extensions;
using TSD.Reference.Core.Entities;
using TSD.Reference.Core.Services.Interfaces;
using WebApi.OutputCache.V2;

namespace TSD.Reference.API.Controllers
{
	[AutoInvalidateCacheOutput]
	[Authorize]
	public class AutomobileController : ApiController
	{
		private readonly IAutomobileService _autoService;

		public AutomobileController(IAutomobileService theAutomobileService)
		{
			_autoService = theAutomobileService;
		}

		// GET: api/Automobile
		//[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 300)]
		public async Task<IEnumerable<Automobile>> Get()
		{
			var aCustomerId = this.GetCustomerId();
			
			return await _autoService.GetAutomobilesForCustomerAsync(Convert.ToInt32(aCustomerId));
		}


		// GET: api/Automobile/5
		[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 300)]
		public async Task<Automobile> Get(int id)
		{
			var aCustomerId = this.GetCustomerId();

			return await _autoService.GetAutomobileAsync(aCustomerId, id);
		}

		// POST: api/Automobile
		public async Task<int> Post(Automobile theAutomobile)
		{
			return await _autoService.AddAutomobileAsync(theAutomobile);
		}

		// PUT: api/Automobile/5
		public async Task Put(Automobile theAutomobile)
		{
			var aCustomerId = this.GetCustomerId();

			await _autoService.UpdateAutomobileAsync(aCustomerId, theAutomobile);
		}
	}
}
