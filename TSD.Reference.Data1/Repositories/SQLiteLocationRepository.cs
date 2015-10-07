using System.Threading.Tasks;
using SQLite;
using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;
using TSD.Reference.Data.DTO;

namespace TSD.Reference.Data.Repositories
{
	public class SQLiteLocationRepository : ILocationRepository
	{
		private SQLiteConnection dbConnection => new SQLiteConnection(SQLiteConstants.ConnectionString);
		private SQLiteAsyncConnection dbConnectionAsync => new SQLiteAsyncConnection(SQLiteConstants.ConnectionString);

		public SQLiteLocationRepository()
		{
			var db = dbConnection;
			db.CreateTable<LocationDTO>();
		}
		public Location GetLocation(int theLocationId)
		{
			var aLocation = dbConnection.Table<LocationDTO>().FirstOrDefault(aItem => aItem.Id == theLocationId);
			return aLocation?.ToEntity();
		}

		public int AddLocation(Location theLocation)
		{
			return dbConnection.Insert(theLocation.ToDTO());
		}

		public void UpdateLocation(Location theLocation)
		{
			dbConnection.Update(theLocation.ToDTO());
		}

		public void DeleteLocation(Location theLocation)
		{
			dbConnection.Delete<LocationDTO>(theLocation.Id);
		}

		public async Task<Location> GetLocationAsync(int theLocationId)
		{
			var aLocation = await dbConnectionAsync.Table<LocationDTO>().Where(aItem => aItem.Id == theLocationId).FirstOrDefaultAsync();
			return aLocation?.ToEntity();
		}

		public async Task<int> AddLocationAsync(Location theLocation)
		{
			return await dbConnectionAsync.InsertAsync(theLocation.ToDTO());
		}

		public async Task UpdateLocationAsync(Location theLocation)
		{
			await dbConnectionAsync.UpdateAsync(theLocation.ToDTO());
		}

		public async Task DeleteLocationAsync(Location theLocation)
		{
			await dbConnectionAsync.DeleteAsync(theLocation.ToDTO());
		}
	}
}
