using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using TSD.Reference.API.Extensions;
using TSD.Reference.Core.Entities;
using TSD.Reference.Core.Services.Interfaces;

namespace TSD.Reference.API.Controllers
{
	public class RenterController : ApiController
	{
		private readonly IRenterService _renterService;

		public RenterController(IRenterService theRenterService)
		{
			_renterService = theRenterService;
		}
		// GET: api/Renter
		public async Task<IEnumerable<Renter>> Get()
		{
			var aCustomerId = this.GetCustomerId();
			return await _renterService.GetRentersAsync(aCustomerId);
		}

		// GET: api/Renter/5
		public async Task<Renter> Get(int id)
		{
			var aCustomerId = this.GetCustomerId();

			return await _renterService.GetRenterAsync(aCustomerId, id);
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
