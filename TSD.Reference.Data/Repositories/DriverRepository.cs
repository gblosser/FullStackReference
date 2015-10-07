using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;
using TSD.Reference.Data.SQLite.DTO;

namespace TSD.Reference.Data.SQLite.Repositories
{
	public class DriverRepository : AbstractRepository, IDriverRepository
	{
		public DriverRepository()
		{
			var db = Connection;
			db.CreateTable<DriverDTO>();
		}

		public Driver GetDriver(int theDriverId)
		{
			var aDriver =  Connection.Table<DriverDTO>().FirstOrDefault(aItem => aItem.Id == theDriverId);
			return aDriver?.ToEntity();
		}

		public List<Driver> GetDriverByLastName(string theDriverName)
		{
			var aResult = Connection.Table<DriverDTO>().Where(aItem =>aItem.LastName.Contains(theDriverName));
			// if there are no results return an empty list
			if (aResult == null || !aResult.Any())
				return Enumerable.Empty<Driver>().ToList();

			return aResult.Select(aItem => aItem.ToEntity()).ToList();
		} 

		public int AddDriver(Driver theDriver)
		{
			return Connection.Insert(theDriver.ToDTO());
		}

		public void UpdateDriver(Driver theDriver)
		{
			Connection.Update(theDriver.ToDTO());
		}

		public void DeleteDriver(Driver theDriver)
		{
			Connection.Delete<DriverDTO>(theDriver.Id);
		}

		public async Task<Driver> GetDriverAsync(int theDriverId)
		{
			var aDriver = await ConnectionAsync.Table<DriverDTO>().Where(aItem => aItem.Id == theDriverId).FirstOrDefaultAsync();
			return aDriver?.ToEntity();
		}


		public async Task<List<Driver>> GetDriverByLastNameAsync(string theDriverName)
		{
			var aResult = await ConnectionAsync.Table<DriverDTO>().Where(aItem => aItem.LastName.Contains(theDriverName)).ToListAsync();
			// if there are no results return an empty list
			if (aResult == null || !aResult.Any())
				return Enumerable.Empty<Driver>().ToList();

			return aResult.Select(aItem => aItem.ToEntity()).ToList();
		}

		public async Task<int> AddDriverAsync(Driver theDriver)
		{
			return await ConnectionAsync.InsertAsync(theDriver.ToDTO());
		}

		public async Task UpdateDriverAsync(Driver theDriver)
		{
			await ConnectionAsync.UpdateAsync(theDriver.ToDTO());
		}

		public async Task DeleteDriverAsync(Driver theDriver)
		{
			await ConnectionAsync.DeleteAsync(theDriver.ToDTO());
		}
	}
}
