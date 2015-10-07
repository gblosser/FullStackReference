using System.Threading.Tasks;
using SQLite;
using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;
using TSD.Reference.Data.DTO;

namespace TSD.Reference.Data.Repositories
{
	public class SQLiteDriverRepository : IDriverRepository
	{
		private SQLiteConnection dbConnection => new SQLiteConnection(SQLiteConstants.ConnectionString);
		private SQLiteAsyncConnection dbConnectionAsync => new SQLiteAsyncConnection(SQLiteConstants.ConnectionString);

		public SQLiteDriverRepository()
		{
			var db = dbConnection;
			db.CreateTable<DriverDTO>();
		}

		public Driver GetDriver(int theDriverId)
		{
			var aDriver =  dbConnection.Table<DriverDTO>().Where(aItem => aItem.Id == theDriverId).FirstOrDefault();
			return aDriver?.ToEntity();
		}

		public int AddDriver(Driver theDriver)
		{
			return dbConnection.Insert(theDriver.ToDTO());
		}

		public void UpdateDriver(Driver theDriver)
		{
			dbConnection.Update(theDriver.ToDTO());
		}

		public void DeleteDriver(Driver theDriver)
		{
			dbConnection.Delete<DriverDTO>(theDriver.Id);
		}

		public async Task<Driver> GetDriverAsync(int theDriverId)
		{
			var aDriver = await dbConnectionAsync.Table<DriverDTO>().Where(aItem => aItem.Id == theDriverId).FirstOrDefaultAsync();
			return aDriver?.ToEntity();
		}

		public async Task<int> AddDriverAsync(Driver theDriver)
		{
			return await dbConnectionAsync.InsertAsync(theDriver.ToDTO());
		}

		public async Task UpdateDriverAsync(Driver theDriver)
		{
			await dbConnectionAsync.UpdateAsync(theDriver.ToDTO());
		}

		public async Task DeleteDriverAsync(Driver theDriver)
		{
			await dbConnectionAsync.DeleteAsync(theDriver.ToDTO());
		}
	}
}
