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
	public class RentalAgreementController : ApiController
	{
		private readonly IRentalAgreementService _rentalAgreementService;

		public RentalAgreementController(IRentalAgreementService theRentalAgreementService)
		{
			_rentalAgreementService = theRentalAgreementService;
		}

		// GET: api/RentalAgreement
		//[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 300)]
		public async Task<IEnumerable<RentalAgreement>> Get()
		{
			var aCustomerId = this.GetCustomerId();

			var aAgreements = await _rentalAgreementService.GetRentalAgreementsForCustomerAsync(aCustomerId);

			if (aAgreements == null)
				throw new HttpResponseException(HttpStatusCode.NotFound);
			return aAgreements;

		}

		// GET: api/RentalAgreement/5
		[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 300)]
		public async Task<RentalAgreement> Get(int id)
		{
			var aAgreement = await _rentalAgreementService.GetRentalAgreementAsync(id);

			if (aAgreement == null)
				throw new HttpResponseException(HttpStatusCode.NotFound);

			return aAgreement;
		}

		// POST: api/RentalAgreement
		public async Task<int> Post(RentalAgreement theRentalAgreement)
		{
			return await _rentalAgreementService.AddRentalAgreementAsync(theRentalAgreement);
		}

		// PUT: api/RentalAgreement/5
		public async Task Put(RentalAgreement theRentalAgreement)
		{
			await _rentalAgreementService.UpdateRentalAgreementAsync(theRentalAgreement);
		}
	}
}
