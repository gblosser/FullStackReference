using System.Threading.Tasks;
using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;
using TSD.Reference.Data.SQLite.DTO;

namespace TSD.Reference.Data.SQLite.Repositories
{
	public class LocationRepository : AbstractRepository, ILocationRepository
	{
		public LocationRepository()
		{
			var db = Connection;
			db.CreateTable<LocationDTO>();
		}
		public Location GetLocation(int theLocationId)
		{
			var aLocation = Connection.Table<LocationDTO>().FirstOrDefault(aItem => aItem.Id == theLocationId);
			return aLocation?.ToEntity();
		}

		public int AddLocation(Location theLocation)
		{
			return Connection.Insert(theLocation.ToDTO());
		}

		public void UpdateLocation(Location theLocation)
		{
			Connection.Update(theLocation.ToDTO());
		}

		public void DeleteLocation(Location theLocation)
		{
			Connection.Delete<LocationDTO>(theLocation.Id);
		}

		public async Task<Location> GetLocationAsync(int theLocationId)
		{
			var aLocation = await ConnectionAsync.Table<LocationDTO>().Where(aItem => aItem.Id == theLocationId).FirstOrDefaultAsync();
			return aLocation?.ToEntity();
		}

		public async Task<int> AddLocationAsync(Location theLocation)
		{
			return await ConnectionAsync.InsertAsync(theLocation.ToDTO());
		}

		public async Task UpdateLocationAsync(Location theLocation)
		{
			await ConnectionAsync.UpdateAsync(theLocation.ToDTO());
		}

		public async Task DeleteLocationAsync(Location theLocation)
		{
			await ConnectionAsync.DeleteAsync(theLocation.ToDTO());
		}
	}
}
