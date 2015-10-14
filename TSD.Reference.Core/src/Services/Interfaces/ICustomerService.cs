using System.Threading.Tasks;
using TSD.Reference.Core.Entities;

namespace TSD.Reference.Core.Services.Interfaces
{
	public interface ICustomerService
	{
		int AddCustomer(Customer theCustomer);
		Task<int> AddCustomerAsync(Customer theCustomer);
		void DeleteCustomer(Customer theCustomer);
		Task DeleteCustomerAsync(Customer theCustomer);
		Customer GetCustomerByName(string theCustomerName);
		Customer GetCustomerById(int theId);
		Task<Customer> GetCustomerByNameAsync(string theCustomerName);
		Task<Customer> GetCustomerByIdAsync(int theId);
		void UpdateCustomer(Customer theCustomer);
		Task UpdateCustomerAsync(Customer theCustomer);
	}
}