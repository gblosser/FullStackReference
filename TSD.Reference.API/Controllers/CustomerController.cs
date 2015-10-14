using System.Threading.Tasks;
using System.Web.Http;
using TSD.Reference.Core.Entities;
using TSD.Reference.Core.Services;
using TSD.Reference.Core.Services.Interfaces;

namespace TSD.Reference.API.Controllers
{
	public class CustomerController : ApiController
	{
		private readonly ICustomerService _customerService;

		public CustomerController(ICustomerService theCustomerService)
		{
			_customerService = theCustomerService;
		}

		// GET: api/Customer
		public async Task<Customer> Get(string name)
		{
			return await _customerService.GetCustomerByNameAsync(name);
		}

		// GET: api/Customer/5
		public async Task<Customer> Get(int id)
		{
			return await _customerService.GetCustomerByIdAsync(id);
		}

		// POST: api/Customer
		public async Task<int> Post(Customer customer)
		{
			return await _customerService.AddCustomerAsync(customer);
		}

		// PUT: api/Customer
		public async Task Put(Customer customer)
		{
			await _customerService.UpdateCustomerAsync(customer);
		}

		// DELETE: api/Customer/5
		public async Task Delete(Customer customer)
		{
			await _customerService.DeleteCustomerAsync(customer);
		}
	}
}
