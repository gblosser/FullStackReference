using System.Threading.Tasks;
using SQLite;
using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;
using TSD.Reference.Data.DTO;

namespace TSD.Reference.Data.Repositories
{
	public class SQLiteCustomerRepository : ICustomerRepository
	{
		private SQLiteConnection dbConnection => new SQLiteConnection(SQLiteConstants.ConnectionString);
		private SQLiteAsyncConnection dbConnectionAsync => new SQLiteAsyncConnection(SQLiteConstants.ConnectionString);

		public SQLiteCustomerRepository()
		{
			var db = dbConnection;
			db.CreateTable<CustomerDTO>();
		}

		public Customer GetCustomer(int theCustomerId)
		{
			var aCustomer = dbConnection.Table<CustomerDTO>().FirstOrDefault(aItem => aItem.Id == theCustomerId);
			return aCustomer?.ToEntity();
		}

		public Customer GetCustomerByName(string theCustomerName)
		{
			var aCustomer = dbConnection.Table<CustomerDTO>().FirstOrDefault(aItem => aItem.Name == theCustomerName);
			return aCustomer?.ToEntity();
		}

		public int AddCustomer(Customer theCustomer)
		{
			return dbConnection.Insert(theCustomer.ToDTO());
		}

		public void UpdateCustomer(Customer theCustomer)
		{
			dbConnection.Update(theCustomer.ToDTO());
		}

		public void DeleteCustomer(Customer theCustomer)
		{
			dbConnection.Delete<CustomerDTO>(theCustomer.Id);
		}

		public async Task<Customer> GetCustomerAsync(int theCustomerId)
		{
			var aCustomer = await dbConnectionAsync.Table<CustomerDTO>().FirstOrDefaultAsync(aItem => aItem.Id == theCustomerId);
			return aCustomer?.ToEntity();
		}

		public async Task<Customer> GetCustomerByNameAsync(string theCustomerName)
		{
			var aCustomer = await dbConnectionAsync.Table<CustomerDTO>().FirstOrDefaultAsync(aItem => aItem.Name == theCustomerName);
			return aCustomer?.ToEntity();
		}

		public async Task<int> AddCustomerAsync(Customer theCustomer)
		{
			return await dbConnectionAsync.InsertAsync(theCustomer.ToDTO());
		}

		public async Task UpdateCustomerAsync(Customer theCustomer)
		{
			await dbConnectionAsync.UpdateAsync(theCustomer.ToDTO());
		}

		public async Task DeleteCustomerAsync(Customer theCustomer)
		{
			await dbConnectionAsync.DeleteAsync(theCustomer.ToDTO());
		}
	}
}
