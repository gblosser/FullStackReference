using System;
using System.Threading.Tasks;
using TSD.Reference.Core.Entities;

namespace TSD.Reference.Core.Data
{
	public interface ICustomerRepository
	{
		Customer GetCustomer(int theCustomerId);

		Customer GetCustomerByName(string theCustomerName);
		int AddCustomer(Customer theCustomer);
		void UpdateCustomer(Customer theCustomer);
		void DeleteCustomer(Customer theCustomer);

		Task<Customer> GetCustomerAsync(int theCustomerId);
		Task<Customer> GetCustomerByNameAsync(string theCustomerName);
		Task<int> AddCustomerAsync(Customer theCustomer);
		Task UpdateCustomerAsync(Customer theCustomer);
		Task DeleteCustomerAsync(Customer theCustomer);
	}
}