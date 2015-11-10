using System.Collections.Generic;
using System.Net;
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
	public class RenterController : ApiController
	{
		private readonly IRenterService _renterService;

		public RenterController(IRenterService theRenterService)
		{
			_renterService = theRenterService;
		}

		// GET: api/Renter
		//[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 300)]
		public async Task<IEnumerable<Renter>> Get()
		{
			var aCustomerId = this.GetCustomerId();

			var aReturn = await _renterService.GetRentersAsync(aCustomerId);

			if(aReturn == null)
				throw new HttpResponseException(HttpStatusCode.NotFound);
			return aReturn;
		}

		// GET: api/Renter/5
		[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 300)]
		public async Task<Renter> Get(int id)
		{
			var aCustomerId = this.GetCustomerId();

			var aReturn = await _renterService.GetRenterAsync(aCustomerId, id);

			if(aReturn == null)
				throw new HttpResponseException(HttpStatusCode.NotFound);

			return aReturn;
		}

		// POST: api/Renter
		public async Task<int> Post(Renter renter)
		{
			return await _renterService.AddRenterAsync(renter);
		}

		// PUT: api/Renter/5
		public async Task Put(Renter renter)
		{
			await _renterService.UpdateRenterAsync(renter);
		}

		// DELETE: api/Renter/5
		public async Task Delete(Renter renter)
		{
			await _renterService.DeleteRenterAsync(renter);
		}
	}
}
