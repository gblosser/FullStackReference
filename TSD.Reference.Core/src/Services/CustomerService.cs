using System.Threading.Tasks;
using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;

namespace TSD.Reference.Core.Services
{
	public class CustomerService
	{
		private readonly ICustomerRepository _repository;

		public CustomerService(ICustomerRepository theCustomerRepository)
		{
			_repository = theCustomerRepository;
		}

		public Customer GetCustomerByName(string theCustomerName)
		{
			return _repository.GetCustomerByName(theCustomerName);
		}

		public async Task<Customer> GetCustomerByNameAsync(string theCustomerName)
		{
			return await _repository.GetCustomerByNameAsync(theCustomerName);
		}

		public int AddCustomer(Customer theCustomer)
		{
			return _repository.AddCustomer(theCustomer);
		}

		public void UpdateCustomer(Customer theCustomer)
		{
			_repository.UpdateCustomer(theCustomer);
		}

		public void DeleteCustomer(Customer theCustomer)
		{
			_repository.DeleteCustomer(theCustomer);
		}
	}
}
