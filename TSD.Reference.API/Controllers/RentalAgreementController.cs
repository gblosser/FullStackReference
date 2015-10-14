using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using TSD.Reference.API.Extensions;
using TSD.Reference.Core.Entities;
using TSD.Reference.Core.Services.Interfaces;

namespace TSD.Reference.API.Controllers
{
	public class RentalAgreementController : ApiController
	{
		private readonly IRentalAgreementService _rentalAgreementService;

		public RentalAgreementController(IRentalAgreementService theRentalAgreementService)
		{
			_rentalAgreementService = theRentalAgreementService;
		}
		// GET: api/RentalAgreement
		public async Task<IEnumerable<RentalAgreement>> Get()
		{
			var aCustomerId = this.GetCustomerId();

			return await _rentalAgreementService.GetRentalAgreementsForCustomerAsync(aCustomerId);
		}

		// GET: api/RentalAgreement/5
		public async Task<RentalAgreement> Get(int id)
		{
			return await _rentalAgreementService.GetRentalAgreementAsync(id);
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
