using System.Threading.Tasks;
using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;
using TSD.Reference.Core.Services.Interfaces;

namespace TSD.Reference.Core.Services
{
	public class CustomerService : ICustomerService
	{
		private readonly ICustomerRepository _repository;

		public CustomerService(ICustomerRepository theCustomerRepository)
		{
			_repository = theCustomerRepository;
		}

		public async Task DeleteCustomerAsync(Customer theCustomer)
		{
			await _repository.DeleteCustomerAsync(theCustomer);
		}

		public Customer GetCustomerByName(string theCustomerName)
		{
			return _repository.GetCustomerByName(theCustomerName);
		}

		public Customer GetCustomerById(int theId)
		{
			return _repository.GetCustomer(theId);
		}

		public async Task<Customer> GetCustomerByNameAsync(string theCustomerName)
		{
			return await _repository.GetCustomerByNameAsync(theCustomerName);
		}

		public async Task<Customer> GetCustomerByIdAsync(int theId)
		{
			return await _repository.GetCustomerAsync(theId);
		}

		public int AddCustomer(Customer theCustomer)
		{
			return _repository.AddCustomer(theCustomer);
		}

		public async Task<int> AddCustomerAsync(Customer theCustomer)
		{
			return await _repository.AddCustomerAsync(theCustomer);
		}

		public void UpdateCustomer(Customer theCustomer)
		{
			_repository.UpdateCustomer(theCustomer);
		}

		public async Task UpdateCustomerAsync(Customer theCustomer)
		{
			await _repository.UpdateCustomerAsync(theCustomer);
		}

		public void DeleteCustomer(Customer theCustomer)
		{
			_repository.DeleteCustomer(theCustomer);
		}
	}
}
