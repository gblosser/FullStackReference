using System.Threading.Tasks;
using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;
using TSD.Reference.Data.SQLite.DTO;

namespace TSD.Reference.Data.SQLite.Repositories
{
	public class CustomerRepository : AbstractRepository, ICustomerRepository
	{
		public CustomerRepository()
		{
			var db = Connection;
			db.CreateTable<CustomerDTO>();
		}

		public Customer GetCustomer(int theCustomerId)
		{
			var aCustomer = Connection.Table<CustomerDTO>().FirstOrDefault(aItem => aItem.Id == theCustomerId);
			return aCustomer?.ToEntity();
		}

		public Customer GetCustomerByName(string theCustomerName)
		{
			var aCustomer = Connection.Table<CustomerDTO>().FirstOrDefault(aItem => aItem.Name == theCustomerName);
			return aCustomer?.ToEntity();
		}

		public int AddCustomer(Customer theCustomer)
		{
			return Connection.Insert(theCustomer.ToDTO());
		}

		public void UpdateCustomer(Customer theCustomer)
		{
			Connection.Update(theCustomer.ToDTO());
		}

		public void DeleteCustomer(Customer theCustomer)
		{
			Connection.Delete<CustomerDTO>(theCustomer.Id);
		}

		public async Task<Customer> GetCustomerAsync(int theCustomerId)
		{
			var aCustomer = await ConnectionAsync.Table<CustomerDTO>().FirstOrDefaultAsync(aItem => aItem.Id == theCustomerId);
			return aCustomer?.ToEntity();
		}

		public async Task<Customer> GetCustomerByNameAsync(string theCustomerName)
		{
			var aCustomer = await ConnectionAsync.Table<CustomerDTO>().FirstOrDefaultAsync(aItem => aItem.Name == theCustomerName);
			return aCustomer?.ToEntity();
		}

		public async Task<int> AddCustomerAsync(Customer theCustomer)
		{
			return await ConnectionAsync.InsertAsync(theCustomer.ToDTO());
		}

		public async Task UpdateCustomerAsync(Customer theCustomer)
		{
			await ConnectionAsync.UpdateAsync(theCustomer.ToDTO());
		}

		public async Task DeleteCustomerAsync(Customer theCustomer)
		{
			await ConnectionAsync.DeleteAsync(theCustomer.ToDTO());
		}
	}
}
