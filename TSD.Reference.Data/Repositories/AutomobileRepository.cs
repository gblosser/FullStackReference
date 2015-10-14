using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;
using TSD.Reference.Data.SQLite.DTO;

namespace TSD.Reference.Data.SQLite.Repositories
{
	public class AutomobileRepository : AbstractRepository, IAutomobileRepository
	{

		public AutomobileRepository()
		{ 
			var db = Connection;
			db.CreateTable<AutomobileDTO>(CreateFlags.AutoIncPK);
		}

		#region Sync methods

		public Automobile GetAutomobile(int theAutomobileId)
		{
			var aDto = Connection.Table<AutomobileDTO>().FirstOrDefault(aItem => aItem.Id == theAutomobileId);
			return aDto?.ToEntity();
		}

		public IEnumerable<Automobile> GetAutomobiles(IEnumerable<int> theAutomobileIds)
		{
			var aResult = Connection.Table<AutomobileDTO>().Where(aItem => theAutomobileIds.Contains(aItem.Id));

			// if there are no results return an empty list
			if (aResult == null || !aResult.Any())
				return Enumerable.Empty<Automobile>().ToList();

			return aResult.Select(aItem => aItem.ToEntity()).ToList();
		}

		public async Task<IEnumerable<Automobile>> GetAutomobilesForLocationAsync(int theLocationId)
		{
			var aQueryResult = ConnectionAsync.Table<AutomobileDTO>().Where(aItem => aItem.LocationId == theLocationId);

			if (aQueryResult == null)
				return null;

			var aDtoList = await aQueryResult.ToListAsync();
			return aDtoList?.Select(aItem => aItem.ToEntity()).ToList();
		}

		public async Task<IEnumerable<Automobile>> GetAutomobilesForLocationsAsync(IEnumerable<int> theLocationIds)
		{
			var aQueryResult = await ConnectionAsync.Table<AutomobileDTO>().Where(aItem => theLocationIds.Contains(aItem.LocationId)).ToListAsync();

			return aQueryResult?.Select(aItem => aItem.ToEntity()).ToList();
		}

		public int AddAutomobile(Automobile theAutomobile)
		{
			return Connection.Insert(theAutomobile.ToDTO());
		}

		public void UpdateAutomobile(Automobile theAutomobile)
		{
			Connection.Update(theAutomobile.ToDTO());
		}

		public void DeleteAutomobile(Automobile theAutomobile)
		{
			Connection.Delete<AutomobileDTO>(theAutomobile.Id);
		}

		#endregion Sync methods

		#region Async methods

		public async Task<Automobile> GetAutomobileAsync(int theAutomobileId)
		{
			var aDto =
				await ConnectionAsync.Table<AutomobileDTO>().Where(aItem => aItem.Id == theAutomobileId).FirstOrDefaultAsync();
			return aDto?.ToEntity();
		}

		public async Task<IEnumerable<Automobile>> GetAutomobilesAsync(IEnumerable<int> theAutomobileIds)
		{
			var aResult =
				await ConnectionAsync.Table<AutomobileDTO>().Where(aItem => theAutomobileIds.Contains(aItem.Id)).ToListAsync();
			return aResult.Select(aItem => aItem.ToEntity()).ToList();
		}

		public async Task<int> AddAutomobileAsync(Automobile theAutomobile)
		{
			return await ConnectionAsync.InsertAsync(theAutomobile.ToDTO());
		}

		public async Task UpdateAutomobileAsync(Automobile theAutomobile)
		{
			await ConnectionAsync.UpdateAsync(theAutomobile.ToDTO());
		}

		public async Task DeleteAutomobileAsync(Automobile theAutomobile)
		{
			await ConnectionAsync.DeleteAsync(theAutomobile.ToDTO());
		}

		#endregion Async methods
	}
}
